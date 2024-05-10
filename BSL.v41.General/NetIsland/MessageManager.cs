using System.Net.Sockets;
using BSL.v41.General.Cloud;
using BSL.v41.General.GHelp;
using BSL.v41.General.NetIsland.LaserBattle;
using BSL.v41.General.Networking;
using BSL.v41.Logic.Database;
using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Database.Alliance;
using BSL.v41.Logic.Environment.LaserCommand.Laser;
using BSL.v41.Logic.Environment.LaserListener;
using BSL.v41.Logic.Environment.LaserMessage;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_1;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_11;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_27;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_4;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Mode;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Player;
using BSL.v41.Logic.Environment.LaserNotification;
using BSL.v41.Logic.Environment.LaserNotification.Laser;
using BSL.v41.StaticService.Laser;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Titan.Utilities;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;
using BSL.v41.Tools.LaserData;
using BSL.v41.Tools.LaserFactory;

namespace BSL.v41.General.NetIsland;

public class MessageManager(
    Socket? listener,
    Socket? userListener,
    Connection? connection,
    ServerConnection? serverConnection)
{
    private readonly LogicHomeMode _logicHomeMode = new();
    public readonly Dictionary<int, PiranhaMessage> ToProxyMessageDictionary = new();
    private int _joinedTime;
    private LanguageFactory _languageFactory = LanguageFactory.English;
    private long _lastKeepAliveReceivedTime = LogicTimeUtil.GetTimestamp();
    private Socket? _listener = listener;
    private int _ping = 100;
    private int _playerGeoEventSlot;
    private int _playerStatus;
    public int ToProxyMessageSeed = 300;

    public void SendMessage(PiranhaMessage? piranhaMessage)
    {
        if (piranhaMessage!.IsServerToClientMessage()) serverConnection!.GetMessaging()!.Send(piranhaMessage);
    }

    public bool IsActive()
    {
        if (_playerGeoEventSlot < 0 || LogicTimeUtil.GetTimestamp() - _lastKeepAliveReceivedTime < 6 * 5)
            return LogicTimeUtil.GetTimestamp() - _lastKeepAliveReceivedTime < 6 * 5;

        IdentifierListener.RemoveLogicGameListenerByAccountId(serverConnection!.GetAccountModel().GetAccountId());

        Saver.OwnMatchmakingManager.MatchmakingManagers[_playerGeoEventSlot]
            .RemovePlayerFromMassive(serverConnection!.GetAccountModel().GetAccountId(), true);
        {
            _playerGeoEventSlot = -1;
        }

        serverConnection!.GetAccountModel()
            .SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.PlayerStatus, 0);

        return LogicTimeUtil.GetTimestamp() - _lastKeepAliveReceivedTime < 6 * 5;
    }

    public int GetPing()
    {
        return _ping;
    }

    public int ConstructInstance(PiranhaMessage piranhaMessage)
    {
        ToProxyMessageSeed++;
        ToProxyMessageDictionary.TryAdd(ToProxyMessageSeed, piranhaMessage);

        return ToProxyMessageSeed;
    }

    public int ReceiveMessage(PiranhaMessage? piranhaMessage)
    {
        
        if (!piranhaMessage!.IsClientToServerMessage()) return 0;

        if (serverConnection!.GetAccountModel() == null!)
            return piranhaMessage.GetMessageType() switch
            {
                10101 => LoginMessageReceived((LoginMessage)piranhaMessage),
                10108 => KeepAliveMessageReceived((KeepAliveMessage)piranhaMessage),
                _ => 0
            };

        if (IsActive())
            switch (piranhaMessage.GetMessageType())
            {
                case 30000:
                    return AttributionMessageReceived((AttributionMessage)piranhaMessage);
            }

        KeepAliveMessageReceived(null!);

        return piranhaMessage.GetMessageType() switch
        {
            10101 => LoginMessageReceived((LoginMessage)piranhaMessage),
            10108 => KeepAliveMessageReceived((KeepAliveMessage)piranhaMessage),
            14103 => MatchmakeRequestMessageReceived((MatchmakeRequestMessage)piranhaMessage),
            10177 => ClientInfoMessageReceived((ClientInfoMessage)piranhaMessage),
            10111 => AccountIdentifiersMessageReceived((AccountIdentifiersMessage)piranhaMessage),
            10107 => ClientCapabilitiesMessageReceived((ClientCapabilitiesMessage)piranhaMessage),
            10110 => AnalyticEventMessageReceived((AnalyticEventMessage)piranhaMessage),
            14106 => CancelMatchmakingMessageReceived((CancelMatchmakingMessage)piranhaMessage),
            14166 => ChronosEventSeenMessageReceived((ChronosEventSeenMessage)piranhaMessage),
            14102 => EndClientTurnMessageReceived((EndClientTurnMessage)piranhaMessage),
            14109 => GoHomeFromOfflinePractiseMessageReceived((GoHomeFromOfflinePractiseMessage)piranhaMessage),
            14101 => GoHomeMessageReceived((GoHomeMessage)piranhaMessage),
            14366 => PlayerStatusMessageReceived((PlayerStatusMessage)piranhaMessage),
            10555 => ClientInputMessageReceived((ClientInputMessage)piranhaMessage),
            10212 => ChangeAvatarNameMessageReceived((ChangeAvatarNameMessage)piranhaMessage),
            14301 => CreateAllianceMessageReceived((CreateAllianceMessage)piranhaMessage),
            14302 => AskForAllianceDataMessageReceived((AskForAllianceDataMessage)piranhaMessage),
            14316 => ChangeAllianceSettingsMessageReceived((ChangeAllianceSettingsMessage)piranhaMessage),
            14308 => LeaveAllianceMessageReceived((LeaveAllianceMessage)piranhaMessage),
            30000 => AttributionMessageReceived((AttributionMessage)piranhaMessage),
            14118 => SinglePlayerMatchRequestMessageReceived((SinglePlayerMatchRequestMessage)piranhaMessage),
            _ => 0
        };
    }

    private int SinglePlayerMatchRequestMessageReceived(SinglePlayerMatchRequestMessage piranhaMessage)
    {
        var location = 15000000 + (70 - 3);

        var players = new Dictionary<long, ServerConnection>
            { { serverConnection!.GetAccountModel().GetAccountId(), serverConnection } };

        var gameModeVariation = LogicDataTables.GetGameModeVariationByName
            (((LogicLocationData)LogicDataTables.GetDataById(location)).GetGameModeVariation()).GetVariation();

        var logicPlayerMap = new List<LogicPlayer>();
        var logicPlayerMapWithBots = new List<LogicPlayer>();
        var logicPlayerDictionary = new Dictionary<LogicPlayer, ServerConnection>();

        var logicBattleModeServer =
            new LogicBattleModeServer(Saver.UdpInfoPorts[new Random().Next(0, Saver.UdpInfoPorts.Count - 1)],
                true);
        {
            logicBattleModeServer.SetLocation(location);
            logicBattleModeServer.GenerateTileMap();
            logicBattleModeServer.SetUpdateTick(20);
            logicBattleModeServer.UpdateTime();
            logicBattleModeServer.TickUpdate();
        }

        var i = 0;
        foreach (var player in players) // player-part.
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

                logicPlayer.CardInfo.Add((LogicCardData)LogicDataTables.GetDataById(
                    GlobalId.CreateGlobalId(CsvHelperTable.Cards.GetId(), 79 - 3)));
                logicPlayer.CardInfo.Add((LogicCardData)LogicDataTables.GetDataById(
                    GlobalId.CreateGlobalId(CsvHelperTable.Cards.GetId(), 408 - 3)));
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
                startLoading.SetLocation(location);
                startLoading.SetGameModeVariation(LogicGameModeUtil.HasTwoTeams(LogicDataTables
                    .GetGameModeVariationByName
                        (((LogicLocationData)LogicDataTables.GetDataById(location)).GetGameModeVariation())
                    .GetVariation())
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

        return 1;
    }

    private int AttributionMessageReceived(AttributionMessage piranhaMessage)
    {
        if (!piranhaMessage.StringReferenceOpenList[9].Equals("utm_source=google-play&utm_medium=organic")) return 0;
        if (!piranhaMessage.StringReferenceOpenList[3]
                .Equals($"{Fingerprint.GetMajorVersion()}.{Fingerprint.GetMinorVersion()}")) return 0;
        if (!piranhaMessage.StringReferenceOpenList[0].Contains("Mozilla")) return 0;
        if (!piranhaMessage.StringReferenceOpenList[0].Contains("Linux")) return 0;
        if (!piranhaMessage.StringReferenceOpenList[0].Contains("Android")) return 0;

        serverConnection!.GetAccountModel().SetFieldValueByAccountStructureParameterFromAccountModel(
            AccountStructure.UserIsAuthed, true); 

        return 1;
    }

    private int LeaveAllianceMessageReceived(LeaveAllianceMessage piranhaMessage)
    {
        AllianceResponseMessage? allianceResponseMessage;

        var v1 = (long)serverConnection!.GetAccountModel()
            .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId);

        if (v1 < 1)
        {
            allianceResponseMessage = new AllianceResponseMessage();
            {
                allianceResponseMessage.IsResponse(94);
            }

            SendMessage(allianceResponseMessage);
            return 0;
        }

        var v2 = Databases.AllianceDatabase.LoadAlliance(v1);
        {
            var d1 =
                (Dictionary<long, AllianceMemberEntry>)v2.GetFieldValueByAllianceStructureParameterFromAllianceModel(
                    AllianceStructure.AllianceMemberEntryList);
            {
                d1.Remove(serverConnection!.GetAccountModel().GetAccountId());
            }

            v2.SetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure.AllianceMemberEntryList,
                d1);
        }

        serverConnection.GetAccountModel()
            .SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId, (long)0);

        allianceResponseMessage = new AllianceResponseMessage();
        {
            allianceResponseMessage.IsResponse(80);
        }

        SendMessage(allianceResponseMessage);
        SendMessage(new MyAllianceMessage());

        var allianceModel = Databases.AllianceDatabase.LoadAlliance(97);
        var allianceMemberEntry = new AllianceMemberEntry
        {
            AccountId = serverConnection.GetAccountModel().GetAccountId(),
            AllianceRoleHelperTable = AllianceRoleHelperTable.Member
        };

        serverConnection!.GetAccountModel()
            .SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId,
                allianceModel.GetAllianceId());
        serverConnection.GetAccountModel()
            .SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceMemberEntry,
                allianceMemberEntry);

        var c = (Dictionary<long, AllianceMemberEntry>)
            allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(
                AllianceStructure.AllianceMemberEntryList);
        c.Add(serverConnection.GetAccountModel().GetAccountId(), allianceMemberEntry);

        allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
            AllianceStructure.AllianceMemberEntryList, c);

        ToSendAutomizeHelper.SendAllianceStatus(
            Databases.AllianceDatabase.LoadAlliance(
                Convert.ToInt64(serverConnection!.GetAccountModel()
                    .GetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.AllianceId))), serverConnection!.GetAccountModel().GetAccountId());
        return 1;
    }

    private int ChangeAllianceSettingsMessageReceived(ChangeAllianceSettingsMessage piranhaMessage)
    {
        AllianceResponseMessage? allianceResponseMessage;

        if (Databases.AllianceDatabase.LoadAlliance((long)serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId)) ==
            null!)
        {
            allianceResponseMessage = new AllianceResponseMessage();
            {
                allianceResponseMessage.IsResponse(93);
            }

            SendMessage(allianceResponseMessage);
            return 0;
        }

        if (!LogicDataTables.GetAllianceRoleByName(((AllianceMemberEntry)serverConnection!.GetAccountModel()
                    .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceMemberEntry))
                .AllianceRoleHelperTable.GetCsvName()).GetCanChangeAllianceSettings())
        {
            allianceResponseMessage = new AllianceResponseMessage();
            {
                allianceResponseMessage.IsResponse(95);
            }

            SendMessage(allianceResponseMessage);
            return 0;
        }

        if (!Helper.GetIsAdequateString(piranhaMessage.GetAllianceDescription()))
        {
            allianceResponseMessage = new AllianceResponseMessage();
            {
                allianceResponseMessage.IsResponse(23);
            }

            SendMessage(allianceResponseMessage);
            return 1;
        }

        var allianceModel = Databases.AllianceDatabase.LoadAlliance((long)serverConnection!.GetAccountModel()
            .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId));

        allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
            AllianceStructure.AllianceDescription, piranhaMessage.GetAllianceDescription());
        allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
            AllianceStructure.AllianceBadgeData, piranhaMessage.GetAllianceBadgeData());
        allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
            AllianceStructure.AllianceRegion, piranhaMessage.GetAllianceRegion());
        allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
            AllianceStructure.AllianceType, piranhaMessage.GetAllianceType());
        allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
            AllianceStructure.AllianceRequiredScore, piranhaMessage.GetRequiredScore());
        allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
            AllianceStructure.AllianceFamilyType, piranhaMessage.GetIsFamilyType());

        allianceResponseMessage = new AllianceResponseMessage();
        {
            allianceResponseMessage.IsResponse(10);
        }

        SendMessage(allianceResponseMessage);

        ToSendAutomizeHelper.SendMyAlliance(this,
            Databases.AllianceDatabase.LoadAlliance((long)serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId)),
            serverConnection!.GetAccountModel(), true);

        ToSendAutomizeHelper.SendAllianceStatus(
            Databases.AllianceDatabase.LoadAlliance(
                Convert.ToInt64(serverConnection!.GetAccountModel()
                    .GetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.AllianceId))), serverConnection!.GetAccountModel().GetAccountId());

        return 1;
    }

    private int AskForAllianceDataMessageReceived(AskForAllianceDataMessage piranhaMessage)
    {
        if (Databases.AllianceDatabase.LoadAlliance(piranhaMessage.GetAllianceId()) == null!)
            return 0;

        ToSendAutomizeHelper.SendAllianceStatus(
            Databases.AllianceDatabase.LoadAlliance(
                Convert.ToInt64(serverConnection!.GetAccountModel()
                    .GetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.AllianceId))), serverConnection!.GetAccountModel().GetAccountId());

        ToSendAutomizeHelper.SendAllianceData(this,
            Databases.AllianceDatabase.LoadAlliance(piranhaMessage.GetAllianceId()),
            (long)serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId),
            myAccountId: serverConnection.GetAccountModel().GetAccountId());

        ToSendAutomizeHelper.SendAllianceStatus(
            Databases.AllianceDatabase.LoadAlliance(
                Convert.ToInt64(serverConnection!.GetAccountModel()
                    .GetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.AllianceId))), serverConnection!.GetAccountModel().GetAccountId());

        return 1;
    }

    private int CreateAllianceMessageReceived(CreateAllianceMessage piranhaMessage)
    {
        AllianceResponseMessage? allianceResponseMessage;

        if (Convert.ToInt32(serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId)) > 0)
        {
            allianceResponseMessage = new AllianceResponseMessage();
            {
                allianceResponseMessage.IsResponse(21);
            }

            SendMessage(allianceResponseMessage);
            return 0;
        }

        if (Convert.ToInt32(serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(
                    AccountStructure.LastCreateAllianceTime)) > 1 * 60 * 60 &&
            LogicTimeUtil.GetTimestamp() - Convert.ToInt32(serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(
                    AccountStructure.LastCreateAllianceTime)) < 1 * 60 * 60)
        {
            allianceResponseMessage = new AllianceResponseMessage();
            {
                allianceResponseMessage.IsResponse(21);
            }

            SendMessage(allianceResponseMessage);
            return 0;
        }

        if (Helper.GetIsAdequateString(piranhaMessage.AllianceName) &&
            Helper.GetIsCorrectName(piranhaMessage.AllianceName))
        {
            if (Helper.GetIsAdequateString(piranhaMessage.AllianceDescription))
            {
                var allianceMemberEntry = new AllianceMemberEntry
                {
                    AccountId = serverConnection.GetAccountModel().GetAccountId(),
                    AllianceRoleHelperTable = AllianceRoleHelperTable.Leader
                };

                var allianceMemberList = new Dictionary<long, AllianceMemberEntry>();
                {
                    allianceMemberList.Add(serverConnection.GetAccountModel().GetAccountId(), allianceMemberEntry);
                }

                var allianceModel = Databases.AllianceDatabase.CreateAlliance(new AllianceModel());
                {
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceCreatedDate,
                        $"{DateTime.Now:yyyy/MM/dd-(dddd) HH:mm:ss}");
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceName, piranhaMessage.AllianceName);
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceDescription, piranhaMessage.AllianceDescription);
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceBadgeData, piranhaMessage.GetAllianceBadgeData());
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceRegion, piranhaMessage.AllianceRegion);
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceType, piranhaMessage.GetAllianceType());
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceRequiredScore, piranhaMessage.GetRequiredScore());
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceFamilyType, piranhaMessage.IsFamilyType);
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceMemberEntryList, allianceMemberList);
                    allianceModel.SetFieldValueByAllianceStructureParameterFromAllianceModel(
                        AllianceStructure.AllianceLanguage, 1000000);
                }

                serverConnection!.GetAccountModel()
                    .SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId,
                        allianceModel.GetAllianceId());
                serverConnection.GetAccountModel()
                    .SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceMemberEntry,
                        allianceMemberEntry);
                serverConnection.GetAccountModel()
                    .SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.LastCreateAllianceTime,
                        LogicTimeUtil.GetTimestamp());

                var allianceHeaderEntry = new AllianceHeaderEntry();
                {
                    allianceHeaderEntry.SetAllianceId(allianceModel.GetAllianceId());
                    allianceHeaderEntry.SetAllianceName(allianceModel
                        .GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure.AllianceName)
                        .ToString()!);
                    allianceHeaderEntry.SetScore(allianceMemberList.Sum(member =>
                        Convert.ToInt32(Databases.AccountDatabase.LoadAccount(member.Value.AccountId)
                            .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.Trophies))));
                    allianceHeaderEntry.SetAllianceType(Convert.ToInt32(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceType)));
                    allianceHeaderEntry.SetAllianceBadgeData(Convert.ToInt32(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceBadgeData)));
                    allianceHeaderEntry.SetRequiredScore(Convert.ToInt32(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceRequiredScore)));
                    allianceHeaderEntry.SetPreferredLanguage(Convert.ToInt32(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceLanguage)));
                    allianceHeaderEntry.SetAllianceRegionData(Convert.ToInt32(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceRegion)));
                    allianceHeaderEntry.SetNumberOfMembers(allianceMemberList.Count);
                    allianceHeaderEntry.SetIsFamilyType(
                        (bool)allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceFamilyType));
                }

                var allianceFullEntry = new AllianceFullEntry();
                {
                    allianceFullEntry.SetAllianceDescription(allianceModel
                        .GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceDescription).ToString()!);
                    allianceFullEntry.SetAllianceHeaderEntry(allianceHeaderEntry);
                    allianceFullEntry.SetAllianceMembers(allianceMemberList);
                }

                var allianceDataMessage = new AllianceDataMessage();
                {
                    allianceDataMessage.IsMyAlliance = true;
                    allianceDataMessage.SetAllianceFullEntry(allianceFullEntry);
                }

                var myAllianceMessage = new MyAllianceMessage();
                {
                    myAllianceMessage.OnlineMembers = allianceMemberList.Count(keyValuePair =>
                        IdentifierListener.GetV2LogicGameListenerByAccountId(keyValuePair.Key));
                    myAllianceMessage.AllianceRole = allianceMemberEntry.AllianceRoleHelperTable;
                    myAllianceMessage.AllianceHeaderEntry = allianceHeaderEntry;
                }

                allianceResponseMessage = new AllianceResponseMessage();
                {
                    allianceResponseMessage.IsResponse(20);
                }

                SendMessage(allianceResponseMessage);
                SendMessage(myAllianceMessage);
                SendMessage(allianceDataMessage);
                return 1;
            }

            allianceResponseMessage = new AllianceResponseMessage();
            {
                allianceResponseMessage.IsResponse(23);
            }

            SendMessage(allianceResponseMessage);
            return 1;
        }

        allianceResponseMessage = new AllianceResponseMessage();
        {
            allianceResponseMessage.IsResponse(piranhaMessage.AllianceName.Length < 3 ? 24 : 22);
        }

        SendMessage(allianceResponseMessage);
        return 1;
    }

    private int ChangeAvatarNameMessageReceived(ChangeAvatarNameMessage piranhaMessage)
    {
        if (piranhaMessage.GetNameSetByUser()) return 0;
        
        if (Helper.GetIsAdequateString(piranhaMessage.GetName()) &&
            Helper.GetIsCorrectName(piranhaMessage.GetName()))
        {
            serverConnection!.GetAccountModel().SetFieldValueByAccountStructureParameterFromAccountModel(
                AccountStructure.NameSetByUser,
                !piranhaMessage.GetNameSetByUser());

            serverConnection!.GetAccountModel().SetFieldValueByAccountStructureParameterFromAccountModel(
                AccountStructure.AvatarName,
                piranhaMessage.GetName());

            var availableServerCommandMessage = new AvailableServerCommandMessage();
            {
                availableServerCommandMessage.SetServerCommand(new LogicChangeAvatarNameCommand(
                    piranhaMessage.GetName(), 0));
            }

            SendMessage(availableServerCommandMessage);
        }
        else
        {
            var avatarNameChangeFailed = new AvatarNameChangeFailedMessage();
            {
                avatarNameChangeFailed.SetReason(piranhaMessage.GetName().Length < 3 ? 2 :
                    piranhaMessage.GetName().Length >= 15 ? 1 : 0);
            }

            SendMessage(avatarNameChangeFailed);
        }

        return 1;
    }

    private int EndClientTurnMessageReceived(EndClientTurnMessage piranhaMessage)
    {
        _logicHomeMode.EndClientTurnReceived(piranhaMessage.GetTick(), piranhaMessage.GetChecksum(),
            piranhaMessage.GetCommands());

        return 1;
    }

    private int ChronosEventSeenMessageReceived(ChronosEventSeenMessage piranhaMessage)
    {
        // ignored.
        return 0;
    }

    private int GoHomeFromOfflinePractiseMessageReceived(GoHomeFromOfflinePractiseMessage piranhaMessage)
    {
        ConstructInstance(new OwnHomeDataMessage(serverConnection!.GetAccountModel()));
        return 1;
    }

    private int GoHomeMessageReceived(GoHomeMessage piranhaMessage)
    {
        ConstructInstance(new OwnHomeDataMessage(serverConnection!.GetAccountModel()));
        return 1;
    }

    private int PlayerStatusMessageReceived(PlayerStatusMessage piranhaMessage)
    {
        _playerStatus = piranhaMessage.GetPlayerStatus();
        
        return 1;
    }

    private int ClientInputMessageReceived(ClientInputMessage piranhaMessage)
    {
        serverConnection!.Disconnect();
        return 0;
    }

    private int CancelMatchmakingMessageReceived(CancelMatchmakingMessage piranhaMessage)
    {
        SendMessage(new MatchMakingCancelledMessage());

        if (_playerGeoEventSlot < 0) return 0;

        Saver.OwnMatchmakingManager.MatchmakingManagers[_playerGeoEventSlot]
            .RemovePlayerFromMassive(serverConnection!.GetAccountModel().GetAccountId());
        {
            _playerGeoEventSlot = -1;
        }

        return 1;
    }

    private int AnalyticEventMessageReceived(AnalyticEventMessage piranhaMessage)
    {
        if (true)
        {
            var c2 = new AvailableServerCommandMessage();
            {
                c2.SetServerCommand(new LogicAddNotificationCommand(new FloaterTextNotification(
                    piranhaMessage.GetAnalyticEvent().GetEvent() + $" ({LogicTimeUtil.GetTimestamp()}): " +
                    piranhaMessage.GetAnalyticEvent().GetEventInfo())));
            }
        }

        return 1;
    }

    private int ClientCapabilitiesMessageReceived(ClientCapabilitiesMessage piranhaMessage)
    {
        _ping = piranhaMessage.GetPing();
        return 1;
    }

    private int AccountIdentifiersMessageReceived(AccountIdentifiersMessage piranhaMessage)
    {
        return 0;
    }

    private int ClientInfoMessageReceived(ClientInfoMessage piranhaMessage)
    {
        if (!piranhaMessage.GetType().Equals("wlan0") && !piranhaMessage.GetType().Equals("tun0")) return 0;

        SendMessage(new UdpConnectionInfoMessage(InteractiveModule.LogicBattleModeServersMassive!
                [InteractiveModule.UdpSessionIdsMassive[serverConnection!.GetAccountModel().GetAccountId()]]
                .GetInitialRandomSeed(),
            Saver.UdpInfoDomain,
            InteractiveModule.UdpSessionIdsMassive[serverConnection.GetAccountModel().GetAccountId()]));

        return 1;
    }

    private int MatchmakeRequestMessageReceived(MatchmakeRequestMessage piranhaMessage)
    {
        var v1 = Saver.OwnMatchmakingManager.MatchmakingManagers[piranhaMessage.GetEventSlot()]
            .AddPlayerToMassive(serverConnection!.GetAccountModel().GetAccountId(), serverConnection); 
        {
            if (v1)
            {
                _playerGeoEventSlot = piranhaMessage.GetEventSlot();
                return 1;
            }

            var message = new MatchmakeFailedMessage();
            {
                message.SetErrorCode(19);
            }

            SendMessage(message);
        }

        return 1;
    }

    private int KeepAliveMessageReceived(KeepAliveMessage piranhaMessage)
    {
        if (piranhaMessage != null!) _lastKeepAliveReceivedTime = LogicTimeUtil.GetTimestamp();
        else _lastKeepAliveReceivedTime = LogicTimeUtil.GetTimestamp() - 5;

        if (piranhaMessage == null!) return 1;

        SendMessage(new KeepAliveServerMessage());

        serverConnection!.GetAccountModel().SetFieldValueByAccountStructureParameterFromAccountModel(
            AccountStructure.LastOnlineTime,
            LogicTimeUtil.GetTimestamp());

        serverConnection!.GetAccountModel().SetFieldValueByAccountStructureParameterFromAccountModel(
            AccountStructure.PlayTimeSeconds,
            Convert.ToInt32(serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.PlayTimeSeconds)) +
            6 * 4);

        return 1;
    }

    private int LoginMessageReceived(LoginMessage piranhaMessage)
    {
        var accountModel = new AccountModel();
        var plIns = GlobalId.GetInstanceId(piranhaMessage.GetPreferredLanguage());

        int accountLvl;
        {
            accountLvl = piranhaMessage.GetAccountId() == 0
                ? 1 // need to create
                : 2; // account created (local usage)

            if (accountLvl == 2)
                try
                {
                    if (Databases.AccountDatabase.LoadAccount(piranhaMessage.GetAccountId()) == null!)
                        accountLvl = 3; // clear app data
                    else
                        accountLvl = piranhaMessage.GetPassToken() ==
                                     Databases.AccountDatabase.LoadAccount(piranhaMessage.GetAccountId())
                                         .GetFieldValueByAccountStructureParameterFromAccountModel(
                                             AccountStructure.PassToken).ToString()
                            ? 4
                            : 3;
                }
                catch (Exception)
                {
                    accountLvl = 3; // clear app data
                }
        }

        switch (accountLvl)
        {
            case 1:
            {
                var notificationFactory = new NotificationFactory();
                {
                    notificationFactory.AccountModel = accountModel;
                }

                accountModel = Databases.AccountDatabase.CreateAccount(accountModel);
                {
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.HomeId,
                        Helper.GenerateRandomIntForBetween(1, 10));
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.PassToken,
                        Helper.GenerateToken(accountModel.GetAccountId()));
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AvatarName,
                        "Brawler");
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.TrophyRoadProgress,
                        1);
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.PlayerThumbnailGlobalId,
                        GlobalId.CreateGlobalId(CsvHelperTable.PlayerThumbnails.GetId(), 0));
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.NameColorGlobalId,
                        GlobalId.CreateGlobalId(CsvHelperTable.NameColors.GetId(), 0));
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.SessionCount,
                        Convert.ToInt32(accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
                            AccountStructure
                                .SessionCount)) + 1);
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.LogicOfferBundleList,
                        new List<LogicOfferBundle>());
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.AccountCreatedDate,
                        $"{DateTime.Now:yyyy/MM/dd-(dddd) HH:mm:ss}");
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.StartPlayTimeSeconds,
                        LogicTimeUtil.GetTimestamp());
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.PlayTimeSeconds, 0);
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.HomeBrawlerGlobalId, GlobalId.CreateGlobalId(
                            CsvHelperTable.Characters.GetId(), 0));
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.SupportedCreator,
                        "BSL-V41");
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.NotificationFactoryIntraSignedId,
                        accountModel.GetAccountId() * 10);
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.IntValueEntryX, 
                        new Dictionary<int, IntValueEntry> { { 15, new IntValueEntry(15, 1) } });
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.IntValueEntryY,
                        new Dictionary<int, IntValueEntry>
                        {
                            {
                                1, new IntValueEntry(1,
                                    GlobalId.CreateGlobalId(CsvHelperTable.Themes.GetId(), 38)) 
                            }
                        });
                }

                Databases.NotificationIntraSignedDatabase.AddAppendix(Convert.ToInt64(
                    accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.NotificationFactoryIntraSignedId)), notificationFactory);
                break;
            }
            case 3:
            {
                try
                {
                    if (piranhaMessage.GetAccountId() < 0)
                    {
                        userListener!.Close();
                    }
                    else
                    {
                        var loginFailedMessage = new LoginFailedMessage();
                        {
                            loginFailedMessage.SetErrorCode(Convert.ToInt32(ErrorCodeHelperTable.CustomMessage));
                            loginFailedMessage.SetContentUrl("");
                            loginFailedMessage.SetRedirectDomain("");
                            loginFailedMessage.SetUpdateUrl("");
                            loginFailedMessage.SetRemoveResourceFingerprintData("");
                            loginFailedMessage.SetSecondsUntilMaintenanceEnd(-1);
                            loginFailedMessage.SetShowContactSupportForBan(false);
                            loginFailedMessage.SetReason(
                                _languageFactory.TranslateTo("Проведите очистку данных приложения, пожалуйста!"));
                        }

                        SendMessage(loginFailedMessage);
                    }
                }
                catch
                {
                    // ignored.
                }

                break;
            }
            case 4:
            {
                accountModel = Databases.AccountDatabase.LoadAccount(piranhaMessage.GetAccountId());
                {
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.PlayTimeSeconds,
                        Convert.ToInt32(
                            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure
                                .PlayTimeSeconds)) + 10);
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.SessionCount,
                        Convert.ToInt32(accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
                            AccountStructure
                                .SessionCount)) + 1);
                }
                break;
            }
        }

        serverConnection!.SetAccountModel(accountModel);
        var allianceModel = Databases.AllianceDatabase.LoadAlliance(
            (long)accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
                AccountStructure.AllianceId));

        var loginOkMessage = new LoginOkMessage();
        {
            loginOkMessage.SetAccountId(Convert.ToInt64(accountModel
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AccountId)));
            loginOkMessage.SetHomeId(Convert.ToInt64(accountModel
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.HomeId)));
            loginOkMessage.SetPassToken(accountModel
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.PassToken).ToString()!);
            loginOkMessage.SetFacebookId("EE0");
            loginOkMessage.SetGamecenterId("Z");
            loginOkMessage.SetServerMajorVersion(Fingerprint.GetMinorVersion());
            loginOkMessage.SetServerBuild(Fingerprint.GetBuildVersion());
            loginOkMessage.SetContentVersion(Fingerprint.GetMajorVersion());
            loginOkMessage.SetServerEnvironment(GlobalStaticCloud.PragmaAndServerEnvironment!);
            loginOkMessage.SetSessionCount(Convert.ToInt32(
                accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.SessionCount)));
            loginOkMessage.SetPlayTimeSeconds(Convert.ToInt32(
                accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure
                    .PlayTimeSeconds)));
            loginOkMessage.SetDaysSinceStartedPlaying(
                Convert.ToInt32(
                    accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure
                        .PlayTimeSeconds)) / 60 / 60 / 24);
            loginOkMessage.SetServerTime($"{DateTime.Now:yyyy/MM/dd-(dddd) HH:mm:ss}");
            loginOkMessage.SetAccountCreatedDate(accountModel
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AccountCreatedDate)
                .ToString()!);
            loginOkMessage.SetStartupCooldownSeconds(60);
            loginOkMessage.SetLoginCountry(_languageFactory.GetTextCodeByLanguage().ToUpper());
            loginOkMessage.SetKunlunId("0f");
            loginOkMessage.SetTier(0);
            loginOkMessage.SetSecondsUntilAccountDeletion(0);
        }

        var myAllianceMessage = new MyAllianceMessage();
        {
            myAllianceMessage.OnlineMembers = allianceModel == null!
                ? 0
                : ToOpenDictionaryHelper.ToAllianceMemberEntryDictionary(
                    allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                        .AllianceMemberEntryList)).Count(keyValuePair =>
                    IdentifierListener.GetV2LogicGameListenerByAccountId(keyValuePair.Key));

            myAllianceMessage.AllianceRole = allianceModel == null!
                ? 0
                : ((AllianceMemberEntry)accountModel
                    .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceMemberEntry))
                .AllianceRoleHelperTable;

            if (allianceModel == null!)
            {
                myAllianceMessage.AllianceHeaderEntry = null!;
            }
            else
            {
                var allianceHeaderEntry = new AllianceHeaderEntry();
                {
                    allianceHeaderEntry.SetAllianceId(allianceModel.GetAllianceId());
                    allianceHeaderEntry.SetAllianceName(allianceModel
                        .GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure.AllianceName)
                        .ToString()!);
                    allianceHeaderEntry.SetScore(ToOpenDictionaryHelper.ToAllianceMemberEntryDictionary(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceMemberEntryList)).Sum(member =>
                        Convert.ToInt32(Databases.AccountDatabase.LoadAccount(member.Value!.AccountId)
                            .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.Trophies))));
                    allianceHeaderEntry.SetAllianceType(Convert.ToInt32(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceType)));
                    allianceHeaderEntry.SetAllianceBadgeData(Convert.ToInt32(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceBadgeData)));
                    allianceHeaderEntry.SetRequiredScore(Convert.ToInt32(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceRequiredScore)));
                    allianceHeaderEntry.SetPreferredLanguage(Convert.ToInt32(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceLanguage)));
                    allianceHeaderEntry.SetAllianceRegionData(Convert.ToInt32(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceRegion)));
                    allianceHeaderEntry.SetNumberOfMembers(ToOpenDictionaryHelper.ToAllianceMemberEntryDictionary(
                        allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceMemberEntryList)).Count);
                    allianceHeaderEntry.SetIsFamilyType(
                        (bool)allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                            .AllianceFamilyType));
                }

                myAllianceMessage.AllianceHeaderEntry = allianceHeaderEntry;
            }
        }

        SendMessage(loginOkMessage);
        SendMessage(new OwnHomeDataMessage(serverConnection!.GetAccountModel()));
        SendMessage(myAllianceMessage);

        // to set;

        _joinedTime = LogicTimeUtil.GetTimestamp();

        _logicHomeMode.AccountModel = serverConnection!.GetAccountModel();

        _logicHomeMode.SetGameListener(new LogicGameListener(connection!.GetUserListener()!, _logicHomeMode));
        _logicHomeMode.SetHome(null!);
        _logicHomeMode.SetHomeOwnerAvatar(null!);

        IdentifierListener.RemoveLogicGameListenerByAccountId(serverConnection!.GetAccountModel().GetAccountId());
        IdentifierListener.SetLogicGameListenerByAccountId(serverConnection!.GetAccountModel().GetAccountId(),
            _logicHomeMode.GetGameListener()!);

        _languageFactory = plIns switch
        {
            17 => LanguageFactory.Russian,
            0 => LanguageFactory.English,
            9 => LanguageFactory.German,
            13 => LanguageFactory.Chinese,
            _ => LanguageFactory.English
        };

        serverConnection!.GetAccountModel().SetFieldValueByAccountStructureParameterFromAccountModel(
            AccountStructure.LanguageLaserFactory, _languageFactory);

        if ((long)serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId) <= 0) return 1;

        ToSendAutomizeHelper.SendMyAlliance(this,
            Databases.AllianceDatabase.LoadAlliance((long)serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId)),
            serverConnection!.GetAccountModel(), true);

        ToSendAutomizeHelper.SendAllianceData(this,
            Databases.AllianceDatabase.LoadAlliance((long)serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId)),
            (long)serverConnection!.GetAccountModel()
                .GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AllianceId), true,
            serverConnection.GetAccountModel().GetAccountId());

        ToSendAutomizeHelper.SendAllianceStatus(
            Databases.AllianceDatabase.LoadAlliance(
                Convert.ToInt64(serverConnection!.GetAccountModel()
                    .GetFieldValueByAccountStructureParameterFromAccountModel(
                        AccountStructure.AllianceId))), serverConnection!.GetAccountModel().GetAccountId());
        return 1;
    }
}
