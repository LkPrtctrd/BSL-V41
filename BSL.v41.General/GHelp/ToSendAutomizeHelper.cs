using BSL.v41.General.NetIsland;
using BSL.v41.Logic.Database;
using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Database.Alliance;
using BSL.v41.Logic.Environment.LaserListener;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_11;
using BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance;

namespace BSL.v41.General.GHelp;

public static class ToSendAutomizeHelper
{
    public static void SendAllianceData(MessageManager messageManager, AllianceModel allianceModel, long myAllianceId,
        bool toAllMembers = false, long myAccountId = -1)
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

        var allianceFullEntry = new AllianceFullEntry();
        {
            allianceFullEntry.SetAllianceDescription(allianceModel
                .GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                    .AllianceDescription).ToString()!);
            allianceFullEntry.SetAllianceHeaderEntry(allianceHeaderEntry);
            allianceFullEntry.SetAllianceMembers(ToOpenDictionaryHelper.ToAllianceMemberEntryDictionary(
                allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                    .AllianceMemberEntryList))!);
        }

        var allianceDataMessage = new AllianceDataMessage();
        {
            allianceDataMessage.IsMyAlliance = allianceModel.GetAllianceId() == myAllianceId;
            allianceDataMessage.MyAccountId = myAccountId;
            allianceDataMessage.SetAllianceFullEntry(allianceFullEntry);
        }

        messageManager.SendMessage(allianceDataMessage);

        if (!toAllMembers) return;

        foreach (var accountId in ToOpenDictionaryHelper.ToAllianceMemberEntryDictionary(
                         allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                             .AllianceMemberEntryList))
                     .Select(sAllianceMemberEntry => sAllianceMemberEntry.Value!.AccountId)
                     .Where(IdentifierListener.GetV2LogicGameListenerByAccountId))
        {
            allianceDataMessage.MyAccountId = myAccountId;
            IdentifierListener.GetLogicGameListenerByAccountId(accountId).Send(allianceDataMessage);
        }
    }

    public static void SendAllianceStatus(AllianceModel allianceModel, long ownAccountId)
    {
        var v1 = ToOpenDictionaryHelper.ToAllianceMemberEntryDictionary(
                allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                    .AllianceMemberEntryList))
            .Select(sAllianceMemberEntry => sAllianceMemberEntry.Value!)
            .Where(account => IdentifierListener.GetV2LogicGameListenerByAccountId(account.AccountId))
            .Where(account => account.AccountId != ownAccountId).ToList();

        foreach (var account in ToOpenDictionaryHelper.ToAllianceMemberEntryDictionary(
                         allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                             .AllianceMemberEntryList))
                     .Select(sAllianceMemberEntry => sAllianceMemberEntry.Value!))
        {
            var v2 = new AllianceMemberMessage();
            {
                v2.MemberAccountId = account.AccountId;
                v2.AllianceMemberEntry = account;
            }

            if (!IdentifierListener.GetV2LogicGameListenerByAccountId(account.AccountId)) continue;

            IdentifierListener.GetLogicGameListenerByAccountId(account.AccountId).Send(v2);
            IdentifierListener.GetLogicGameListenerByAccountId(account.AccountId).Send(
                new AllianceOnlineStatusUpdatedMessage(allianceModel)
                {
                    OnlineMembers = v1,
                    OwnAccountId = ownAccountId
                });
        }
    }

    public static void SendMyAlliance(MessageManager messageManager, AllianceModel allianceModel,
        AccountModel accountModel,
        bool toAllMembers = false)
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

        var myAllianceMessage = new MyAllianceMessage();
        {
            myAllianceMessage.OnlineMembers = ToOpenDictionaryHelper.ToAllianceMemberEntryDictionary(
                allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                    .AllianceMemberEntryList)).Count(keyValuePair =>
                IdentifierListener.GetV2LogicGameListenerByAccountId(keyValuePair.Key));
            myAllianceMessage.AllianceHeaderEntry = allianceHeaderEntry;
            myAllianceMessage.AllianceRole =
                ((AllianceMemberEntry)accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
                    AccountStructure
                        .AllianceMemberEntry)).AllianceRoleHelperTable;
        }

        messageManager.SendMessage(myAllianceMessage);

        if (!toAllMembers) return;

        foreach (var account in ToOpenDictionaryHelper.ToAllianceMemberEntryDictionary(
                         allianceModel.GetFieldValueByAllianceStructureParameterFromAllianceModel(AllianceStructure
                             .AllianceMemberEntryList))
                     .Select(sAllianceMemberEntry => sAllianceMemberEntry.Value!))
        {
            if (!IdentifierListener.GetV2LogicGameListenerByAccountId(account.AccountId)) continue;
            myAllianceMessage.AllianceRole = account.AllianceRoleHelperTable;
            IdentifierListener.GetLogicGameListenerByAccountId(account.AccountId).Send(myAllianceMessage);
        }
    }
}