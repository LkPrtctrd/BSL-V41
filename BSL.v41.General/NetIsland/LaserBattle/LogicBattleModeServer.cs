using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using BSL.v41.General.NetIsland.LaserBattle.Manager;
using BSL.v41.General.NetIsland.LaserBattle.Objects;
using BSL.v41.General.NetIsland.LaserBattle.Objects.Laser;
using BSL.v41.General.NetIsland.LaserBattle.Render.Help;
using BSL.v41.General.NetIsland.LaserBattle.Render.Tile.Metadata;
using BSL.v41.General.NetIsland.LaserBattle.Significant;
using BSL.v41.General.Networking;
using BSL.v41.General.Networking.LaserUdp;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_4;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Input;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Player;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.Graphic;
using BSL.v41.Titan.Mathematical;
using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Titan.Utilities;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.General.NetIsland.LaserBattle;

public class LogicBattleModeServer
{
    public const int IntroTicks = 120;
    public const int NormalTicks = 3120;
    public const int RoboWarsRoboSpawnSeconds1 = 25;
    public const int RoboWarsRoboSpawnSeconds2 = 75;
    public const int RoboWarsRoboSpawnSeconds3 = 125;

    private readonly int _battlePort;
    private readonly bool _isPolygon;
    private readonly LogicGameObjectManagerServer _logicGameObjectManagerServer;
    
    private readonly List<int> _usedPlayerSpawnCoordinates = [];

    public readonly Dictionary<int, LogicPetrolServer> LogicPetrolServers;
    public readonly List<LogicPlayer> LogicPlayers;

    public readonly Dictionary<int, LogicPlayer> LogicPlayersByObjectGlobalId;
    public readonly Dictionary<long, int> LogicPlayersBySessionIdToObjectGlobalId;
    public readonly Dictionary<LogicPlayer, ServerConnection> LogicPlayersConnection;

    private Stopwatch _stopwatch;
    private int _gameModeVariation;
    private int _location;
    private LogicTileMetadata _logicTileMetadata = null!;
    private int _tick;
    private int _updateTick;

    public LogicBattleModeServer(int battlePort, bool isPolygon)
    {
        _battlePort = battlePort;
        _isPolygon = isPolygon;

        _logicGameObjectManagerServer = new LogicGameObjectManagerServer();
        LogicPlayers = new List<LogicPlayer>();
        LogicPlayersByObjectGlobalId = new Dictionary<int, LogicPlayer>();
        LogicPlayersConnection = new Dictionary<LogicPlayer, ServerConnection>();
        LogicPlayersBySessionIdToObjectGlobalId = new Dictionary<long, int>();

        _stopwatch = new Stopwatch();
        _tick = 0;
        _updateTick = 1;

        LogicPetrolServers = new Dictionary<int, LogicPetrolServer>();

        _logicGameObjectManagerServer.SetLogicBattleModeServer(this);

        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Cmd,
            $"New battle spawned. Battle time to unix: {LogicTimeUtil.GetTimestamp()} / " +
            $"Battle port: {battlePort} / Battle polygon type: {isPolygon}. Good luck!");
    }

    public void UpdateTime()
    {
        _updateTick = LogicMath.Min(_updateTick, 20);
    }

    public void TickUpdate()
    {
        Task.Run(() =>
        {
            while (GetTicksGone() < 20000 && _stopwatch != null!)
            {
                _stopwatch.Restart();
                {
                    ExecuteOneTick(null!);
                }

                while (_stopwatch.ElapsedTicks < TimeSpan.FromMilliseconds(1000f / _updateTick).Ticks)
                    Thread.SpinWait(1);
            }

            _stopwatch!.Stop();
            _stopwatch = null!;
        });
    }

    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH",
        MessageId = "type: System.Byte[]; size: 433MB")]
    [SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH",
        MessageId = "type: System.Byte[]; size: 115MB")]
    [SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH",
        MessageId = "type: System.Byte[]; size: 124MB")]
    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH",
        MessageId = "type: System.Byte[]; size: 4372MB")]
    private void ExecuteOneTick(object stateInfo)
    {
        _tick++;
        {
            PreTick();
            Tick();
        }

        if (LogicPlayers.Count > 0)
            if (_tick < 3 * 2)
                return;
        {
            try
            {
                if (_tick <= 3) return;

                var tasks = LogicPlayers.Where(player => player != null!).Where(player => player.OwnObjectId > 0)
                    .Where(player => !player.IsBot())
                    .Select(player =>
                    {
                        if (player.GetPlayerIndex() < 0) return Task.CompletedTask;

                        var bitStream = new BitStream(1024 / 2);

                        _logicGameObjectManagerServer.Encode(bitStream, player.OwnObjectId, player.GetPlayerIndex(),
                            player.GetTeamIndex());

                        var visionUpdateMessage = new VisionUpdateMessage();
                        {
                            visionUpdateMessage.Tick = _tick;
                            visionUpdateMessage.Spectators = 0;
                            visionUpdateMessage.InputCounter = player.InputCounter;
                            visionUpdateMessage.SkipIntro = false;
                            visionUpdateMessage.BrawlTvMode = false;
                        }

                        visionUpdateMessage.SetVisionBitStream(bitStream);

                        var udpMessage = new UdpMessage();
                        {
                            udpMessage.ByteStream = visionUpdateMessage.ByteStream;
                            udpMessage.PiranhaMessage = visionUpdateMessage;
                        }

                        if (_logicGameObjectManagerServer.GetNumGameObjects().Count > 50 || player.TcpModeRefract)
                        {
                            LogicPlayersConnection[player].GetMessaging()!.Send(visionUpdateMessage);
                            _updateTick = 22;
                        }
                        else
                        {
                            _updateTick = 20;
                        }

                        udpMessage.Send(player.SessionId, _battlePort);
                        return Task.CompletedTask;
                    });

                Task.WhenAll(tasks);
            }
            catch (Exception exception)
            {
                ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Error,
                    $"{GetType().Name} -> {exception.Source} \n => {exception.Message} \n => Error{exception.StackTrace}.");

                foreach (var logicPlayer in LogicPlayers.Where(logicPlayer => !logicPlayer.IsBot()))
                    LogicPlayersConnection[logicPlayer].GetMessaging()!.Send(new ServerErrorMessage(43));

                if (_stopwatch.IsRunning) _stopwatch.Stop();
                _stopwatch = null!;
            }
        }
    }

    private void PreTick()
    {
        _logicGameObjectManagerServer.PreTick();
    }

    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH",
        MessageId = "type: System.Byte[]; size: 203MB")]
    private void Tick()
    {
        try
        {
            _logicGameObjectManagerServer.Tick();

            TickSpawnHeroes();
            AddGameObjects();
        }
        catch (Exception exception)
        {
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Error,
                $"{GetType().Name} -> {exception.Source} \n => {exception.Message} \n => Error{exception.StackTrace}.");

            foreach (var logicPlayer in LogicPlayers.Where(logicPlayer => !logicPlayer.IsBot()))
                LogicPlayersConnection[logicPlayer].GetMessaging()!.Send(new ServerErrorMessage(41));

            if (_stopwatch.IsRunning) _stopwatch.Stop();
            _stopwatch = null!;
        }
    }

    private void TickSpawnHeroes()
    {
        if (_tick > 3)
        {
            if (_tick == 5)
                foreach (var logicPlayer in LogicPlayers)
                {
                    logicPlayer.SetPlayerIndex(ObjectIndexHelperTableExtensions.GetIndex(logicPlayer.GetTeamIndex(),
                        logicPlayer.GetPlayerIndex()));
                    {
                        SpawnHero(logicPlayer);
                    }
                }
            else
                Helper.Skip();
            // todo.
        }
    }

    private void TickPetrols()
    {
        foreach (var logicPetrolServer in LogicPetrolServers)
            if (logicPetrolServer.Value.PetrolType == 0) LogicPetrolServers.Remove(logicPetrolServer.Key);
            else logicPetrolServer.Value.Tick(this);
    }

    public void SpawnHero(LogicPlayer logicPlayer, bool respawn = false)
    {
        var logicCharacterServer = new LogicCharacterServer(this,
            CsvHelperTable.Characters.GetId(),
            logicPlayer.HeroInfo[0], logicPlayer.GetPlayerIndex());
        {
            logicCharacterServer.InitializeMembers();
        }

        if (!respawn)
            for (var i = 0; i < _logicTileMetadata.RenderSystem.GetTilemapWidth(); i++)
            for (var j = 0; j < _logicTileMetadata.RenderSystem.GetTilemapHeight(); j++)
            {
                var globalCord = i * j - i;
                var logicTile = _logicTileMetadata.LogicTileMap.GetTile(i, j, true);
                if (logicTile.TileCode != $"{logicPlayer.GetTeamIndex() + 1}"[0] ||
                    _usedPlayerSpawnCoordinates.Contains(globalCord)) continue;

                logicCharacterServer.SetStartPosition(new LogicVector2(logicTile.LogicX, logicTile.LogicY));
                {
                    _usedPlayerSpawnCoordinates.Add(globalCord);
                    {
                        _logicGameObjectManagerServer.AddLogicGameObject(logicCharacterServer);
                        {
                            logicPlayer.OwnObjectId = logicCharacterServer.GetObjectGlobalId();
                        }

                        LogicPlayersByObjectGlobalId.Add(logicPlayer.OwnObjectId, logicPlayer);
                        {
                            if (logicPlayer.SessionId > -1)
                                LogicPlayersBySessionIdToObjectGlobalId.Add(logicPlayer.SessionId,
                                    logicPlayer.OwnObjectId);
                        }

                        if (LogicGameModeUtil.HasSpawnProtectionInTheStart(_gameModeVariation))
                            Helper.Skip(); // todo.
                    }

                    logicCharacterServer.InitializeMembers(true);
                    return;
                }
            }
        else
            Helper.Skip();
        // todo.
    }

    public void AddGameObjects()
    {
        if (_tick != 5) return;
        // todo.
    }

    public void AddPetrol(LogicGameObjectServer creator, int x, int y)
    {
        var logicPetrolServer = new LogicPetrolServer(0);
        {
            if (creator.GetPlayer() == null) logicPetrolServer.Ignite(creator, x, y, -1);
            else logicPetrolServer.Ignite(creator, x, y, creator.GetPlayer()!.GetPlayerIndex());
        }

        LogicPetrolServers.Add(LogicPetrolServers.Count, logicPetrolServer);
    }

    public void HandleClientInput(ClientInput clientInput)
    {
        var logicPlayer = LogicPlayers.Find(player => player.SessionId == clientInput.OwnSessionId)!;
        {
            if (logicPlayer.InputCounter >= clientInput.Index)
                return;
            logicPlayer.InputCounter = clientInput.Index;
        }

        var logicCharacterServer = (LogicCharacterServer)
            _logicGameObjectManagerServer.GetGameObjectById(
                LogicPlayersBySessionIdToObjectGlobalId[clientInput.OwnSessionId]);

        switch (clientInput.Type)
        {
            case 0 or 1:
            {
                logicCharacterServer.ActivateSkill(clientInput.X, clientInput.Y, clientInput.Type);
                logicCharacterServer.ResetAfkTicks();
                break;
            }

            case 2:
            {
                var v12 = logicCharacterServer.GetX();
                var v13 = logicCharacterServer.GetY();
                var v7 = clientInput.X;
                var v90 = clientInput.Y;
                var v14 = v90;
                var v15 = LogicGamePlayUtil.GetDistanceSquaredBetween(v12, v13, v7, v90);

                if (v15 > 810001)
                {
                    var v16 = LogicMath.Sqrt(v15);
                    var v17 = logicCharacterServer.GetX();
                    var v18 = 900 * (v7 - logicCharacterServer.GetX()) / v16;
                    var v19 = logicCharacterServer.GetY();

                    v7 = v18 + v17;
                    v14 = 900 * (v90 - logicCharacterServer.GetY()) / v16 + v19;
                }

                logicCharacterServer.MoveTo(clientInput.X, clientInput.Y, v7, v14);
                logicCharacterServer.ResetAfkTicks();
                break;
            }

            case 5:
            {
                logicCharacterServer.UltiEnabled();
                break;
            }

            case 6:
            {
                logicCharacterServer.UltiDisabled();
                break;
            }

            case 8:
            {
                logicCharacterServer.LogicAccessory?.TriggerAccessory();
                logicCharacterServer.ResetAfkTicks();
                break;
            }

            case 9:
            {
                var v1 = (LogicGlobalData)LogicDataTables.GetDataFromTable(CsvHelperTable.Globals.GetId())
                    .GetDataByName("EMOTE_COOLDOWN_TICKS");

                if (logicPlayer.PinInfo.Count > 0)
                {
                    if (logicPlayer.PinInfo[1] < GetTicksGone())
                    {
                        logicPlayer.PinInfo[0] = clientInput.PinIndex;
                        logicPlayer.PinInfo[1] = GetTicksGone() + v1.GetNumberValue();
                        logicPlayer.PinInfo[2] = GetTicksGone();
                        logicPlayer.PinInfo[3]++;
                    }
                }
                else
                {
                    logicPlayer.PinInfo.Add(clientInput.PinIndex);
                    logicPlayer.PinInfo.Add(GetTicksGone() + +v1.GetNumberValue());
                    logicPlayer.PinInfo.Add(GetTicksGone());
                    logicPlayer.PinInfo.Add(1);
                }

                break;
            }
        }
    }

    public void GenerateTileMap()
    {
        _logicTileMetadata = new LogicTileMetadata(_location);
        _logicTileMetadata.LogicTileMap.LoadMap();
        _logicTileMetadata.LogicTileMap.GenerateTileMap(_logicTileMetadata.LogicGlobalStringData);
        _gameModeVariation = LogicDataTables
            .GetGameModeVariationByName(((LogicLocationData)LogicDataTables.GetDataById(GetLocation() < 1000000
                ? GlobalId.CreateGlobalId(CsvHelperTable.Locations.GetId(), GetLocation())
                : GetLocation())).GetGameModeVariation()).GetVariation();

        if (_isPolygon) TileHelper.DestroyAllBlocksInMap(_logicTileMetadata);
    }

    public void AddPlayer(LogicPlayer logicPlayer, ServerConnection serverConnection)
    {
        LogicPlayers.Add(logicPlayer);
        LogicPlayersConnection.Add(logicPlayer, serverConnection);
    }

    public int GetPlayerCount()
    {
        return LogicPlayers.Count;
    }

    public bool IsPlayFieldMirroredForPlayer(int teamIndex)
    {
        return teamIndex > 0 && LogicGameModeUtil.HasTwoTeams(_gameModeVariation);
    }

    public bool IsPlayerAfk(int afkTicks, LogicCharacterServer logicCharacterServer)
    {
        var v1 = (LogicGlobalData)LogicDataTables.GetDataFromTable(CsvHelperTable.Globals.GetId())
            .GetDataByName("AFK_TIMER_SECONDS");

        if (!logicCharacterServer.GetCharacterData().IsHero()) return false;
        return afkTicks > (v1.GetNumberValue() + 1) * GetTick() && !logicCharacterServer.GetPlayer()!.IsBot();
    }

    public int GetMovementSpeedFactor(int a1, int a2, int a3, int a4, int a5)
    {
        return 1;

        const float v5 = 1.0f;
        var v6 = a1 == 0;

        if (a1 != 0)
        {
            a4 -= a2;
            a2 = a5 - a3;
            v6 = ((a5 - a3) | a4) == 0;
        }

        if (v6) return (int)v5;

        var v7 = 1;
        {
            if (a1 < 0) v7 = -1;
        }

        var v8 = a4 * v7 / (float)Math.Sqrt(a4 * a4 + a2 * a2);
        {
            if (v8 < 0.0f) v8 *= 0.5f;
        }

        return (int)(v8 * 0.5f + 1.0f);
    }

    public LogicTileMetadata GetTileMap()
    {
        return _logicTileMetadata;
    }

    public int GetInitialRandomSeed()
    {
        return _battlePort;
    }

    public int GetTick()
    {
        return _updateTick < 20 ? 20 : _updateTick;
    }

    public int GetTicksGone()
    {
        return _tick;
    }

    public int GetLocation()
    {
        return _location;
    }

    public LogicPlayer? GetPlayer(int findId, bool bySessionId)
    {
        return LogicPlayersByObjectGlobalId.GetValueOrDefault(bySessionId
            ? LogicPlayersBySessionIdToObjectGlobalId.GetValueOrDefault(findId, 0)
            : findId);
    }

    public void SetLocation(int location)
    {
        _location = location;
    }

    public void SetUpdateTick(int value)
    {
        _updateTick = value;
    }
}
