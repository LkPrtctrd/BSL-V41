using BSL.v41.General.Cloud;
using BSL.v41.General.NetIsland.LaserBattle;
using BSL.v41.General.Networking;
using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Dynamic;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_4;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Player;
using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Titan.Utilities;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.General.NetIsland;

public class MatchmakingManager
{
    private int _defaultSecondsToStartGame;
    private int _maxPlayersCount;
    private int _nowPlayersFoundedCount;
    private int _nowRemainedSecondsToStartGame;

    private Dictionary<long, ServerConnection> _players = null!;
    private int _slot;

    public void Initialize(int slot, int maxPlayersCount)
    {
        _slot = slot;
        _maxPlayersCount = maxPlayersCount;
        _nowPlayersFoundedCount = 0;
        _defaultSecondsToStartGame = maxPlayersCount * Saver.SearchTimeFactor;
        _nowRemainedSecondsToStartGame = _defaultSecondsToStartGame;
        _players = new Dictionary<long, ServerConnection>();

        PreUpdate();
        Update();
    }

    public bool AddPlayerToMassive(long accountId, ServerConnection serverConnection)
    {
        Thread.Sleep(200);

        if (_nowPlayersFoundedCount >= _maxPlayersCount) return false;
        _players.TryAdd(accountId, serverConnection);
        _nowPlayersFoundedCount++;
        return true;
    }

    public void RemovePlayerFromMassive(long accountId, bool error = false)
    {
        Thread.Sleep(200);

        try
        {
            var message = new MatchmakeFailedMessage();
            {
                message.SetErrorCode(20);
            }

            if (error) _players[accountId].GetMessaging()!.Send(message);

            _players.Remove(accountId);
            _nowPlayersFoundedCount--;
        }
        catch (Exception)
        {
            // ignored.
        }
    }

    public ServerConnection GetPlayerFromMassive(long accountId)
    {
        return _players.GetValueOrDefault(accountId)!;
    }

    private void PreUpdate()
    {
        Task.Run(() =>
        {
            while (_defaultSecondsToStartGame > 1)
            {
                Thread.Sleep(200);
                if (_players.Count < 1) _nowRemainedSecondsToStartGame = _defaultSecondsToStartGame;
            }
        });
        
        Task.Run(() =>
        {
            while (_defaultSecondsToStartGame > 1)
            {
                Thread.Sleep(1000);

                if (_players.Count >= 1)
                    _nowRemainedSecondsToStartGame = _nowRemainedSecondsToStartGame <= 0
                        ? _defaultSecondsToStartGame
                        : _nowRemainedSecondsToStartGame - 1;
                else _nowRemainedSecondsToStartGame = _defaultSecondsToStartGame;
            }
        });
    }

    private void Update()
    {
        Task.Run(() =>
        {
            while (_defaultSecondsToStartGame >= 1)
            {
                Thread.Sleep(200);

                if (!DynamicServerParameters.Event1DataMassive[_slot - 1].GetTimeFinished())
                {
                    try
                    {
                        var message = new MatchMakingStatusMessage();
                        {
                            message.SetSeconds(_nowRemainedSecondsToStartGame);
                            message.SetFoundPlayers(_nowPlayersFoundedCount);
                            message.SetMaxFounds(_maxPlayersCount);
                            message.SetShowTips(_maxPlayersCount > 0);
                        }

                        foreach (var player in _players) player.Value.GetMessaging()!.Send(message);
                    }
                    catch
                    {
                        // ignored.
                    }

                    if (_nowRemainedSecondsToStartGame <= 1 && _players.Count > 0) StartGame(true);

                    if (_players.Count >= _maxPlayersCount) StartGame(false);
                }
                else
                {
                    var message = new MatchmakeFailedMessage();
                    {
                        message.SetErrorCode(5);
                    }

                    foreach (var player in _players) player.Value.GetMessaging()!.Send(message);

                    _nowPlayersFoundedCount = 0;
                    _nowRemainedSecondsToStartGame = _defaultSecondsToStartGame;
                    _players.Clear();
                }

                Thread.Sleep(200);
            }
        });
    }

    private void StartGame(bool timeFinished)
    {
        _nowPlayersFoundedCount = 0;
        _nowRemainedSecondsToStartGame = _defaultSecondsToStartGame;

        if (_slot > 0)
        {
            var randomBrawlerList = new List<int> { 0, 1, 12, 13, 16, 17 };
            var gameModeVariation = LogicDataTables.GetGameModeVariationByName
            (((LogicLocationData)LogicDataTables.GetDataById(DynamicServerParameters.Event1DataMassive[_slot - 1]
                .GetLocation())).GetGameModeVariation()).GetVariation();

            var logicPlayerMap = new List<LogicPlayer>();
            var logicPlayerMapWithBots = new List<LogicPlayer>();
            var logicPlayerDictionary = new Dictionary<LogicPlayer, ServerConnection>();

            var logicBattleModeServer =
                new LogicBattleModeServer(Saver.UdpInfoPorts[new Random().Next(0, Saver.UdpInfoPorts.Count - 1)],
                    true);
            {
                logicBattleModeServer.SetLocation(DynamicServerParameters.Event1DataMassive[_slot - 1].GetLocation());
                logicBattleModeServer.GenerateTileMap();
                logicBattleModeServer.SetUpdateTick(20);
                logicBattleModeServer.UpdateTime();
                logicBattleModeServer.TickUpdate();
            }

            var i = 0;
            foreach (var player in _players) // player-part.
            {
                if (!InteractiveModule.UdpSessionIdsMassive.ContainsKey(player.Value.GetAccountModel().GetAccountId()))
                    InteractiveModule.UdpSessionIdsMassive.Add(player.Value.GetAccountModel().GetAccountId(),
                        ++Saver.LastUdpSessionId);
                else
                    InteractiveModule.UdpSessionIdsMassive[player.Value.GetAccountModel().GetAccountId()] =
                        ++Saver.LastUdpSessionId;

                var playerDisplayData = new PlayerDisplayData();
                {
                    playerDisplayData.SetAvatarName(player.Value.GetAccountModel()
                        .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AvatarName)
                        .ToString()!);
                    playerDisplayData.SetPlayerThumbnail(Convert.ToInt32(player.Value.GetAccountModel()
                        .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure
                            .PlayerThumbnailGlobalId)));
                    playerDisplayData.SetNameColor(Convert.ToInt32(player.Value.GetAccountModel()
                        .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.NameColorGlobalId)));
                }

                var logicPlayer = new LogicPlayer();
                {
                    logicPlayer.SetPlayerIndex(i);
                    logicPlayer.SetTeamIndex(i % 2);
                    logicPlayer.SetBounty(gameModeVariation == 3);
                    logicPlayer.SetAccountModel(player.Value.GetAccountModel());
                    logicPlayer.SetPlayerDisplayData(playerDisplayData);
                    logicPlayer.SessionId =
                        InteractiveModule.UdpSessionIdsMassive[player.Value.GetAccountModel().GetAccountId()] - 0;

                    logicPlayer.HeroInfo.Add(0); // hero instance id.
                    logicPlayer.HeroInfo.Add(1); // hero level.
                    logicPlayer.HeroInfo.Add(0); // hero trophies.
                    logicPlayer.HeroInfo.Add(0); // skin instance id.

                    /*logicPlayer.CardInfo.Add((LogicCardData)LogicDataTables.GetDataById(
                        GlobalId.CreateGlobalId(CsvHelperTable.Cards.GetId(), 79 - 3)));
                    logicPlayer.CardInfo.Add((LogicCardData)LogicDataTables.GetDataById(
                        GlobalId.CreateGlobalId(CsvHelperTable.Cards.GetId(), 408 - 3)));*/
                }

                logicPlayerMapWithBots.Add(logicPlayer);
                logicPlayerMap.Add(logicPlayer);
                logicPlayerDictionary.Add(logicPlayer, player.Value);
                logicBattleModeServer.AddPlayer(logicPlayer, player.Value);
                InteractiveModule.LogicBattleModeServersMassive!.Add(logicPlayer.SessionId, logicBattleModeServer);
                i++;
            }

            foreach (var player in logicPlayerMap)
            {
                var startLoading = new StartLoadingMessage();
                {
                    startLoading.SetLocation(DynamicServerParameters.Event1DataMassive[_slot - 1].GetLocation());
                    startLoading.SetGameModeVariation(LogicGameModeUtil.HasTwoTeams(LogicDataTables
                        .GetGameModeVariationByName
                        (((LogicLocationData)LogicDataTables.GetDataById(DynamicServerParameters
                            .Event1DataMassive[_slot - 1].GetLocation())).GetGameModeVariation()).GetVariation())
                        ? 1
                        : 6);
                    startLoading.SetLogicPlayer(player);
                    startLoading.SetLogicPlayerMap(logicPlayerMapWithBots);
                    startLoading.SetIsSpectate(false);
                    startLoading.SetIsFriendlyMatch(false);
                    startLoading.SetIsUnderdog(false);
                }

                logicPlayerDictionary[player].GetMessaging()!.Send(startLoading);
            }
        }

        _players.Clear();
    }
}