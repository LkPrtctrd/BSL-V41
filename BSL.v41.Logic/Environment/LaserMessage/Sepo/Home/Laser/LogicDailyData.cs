using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Titan.Mathematical;
using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Titan.Utilities;
using BSL.v41.Tools.LaserFactory;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser;

public class LogicDailyData(AccountModel accountModel)
{
    public void Encode(ByteStream byteStream) 
    {
        var v1 = (List<LogicOfferBundle>)accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
            AccountStructure.LogicOfferBundleList); // object;
        var v2 = (LanguageFactory)accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure
            .LanguageLaserFactory); // object;
        var v3 = (Dictionary<int, IntValueEntry>)accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
            AccountStructure.IntValueEntryX); // object;

        byteStream.WriteVInt(Helper.GetCurrentTimeInSecondsSinceEpoch());
        byteStream.WriteVInt(Helper.GetCurrentTimeInSecondsSinceEpoch());

        byteStream.WriteVInt(
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.Trophies), true);
        byteStream.WriteVInt(
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.MaxTrophies), true);
        byteStream.WriteVInt(
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.MaxTrophies), true);
        byteStream.WriteVInt(
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.TrophyRoadProgress),
            true);
        byteStream.WriteVInt(999);

        ByteStreamHelper.WriteDataReference(byteStream,
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure
                .PlayerThumbnailGlobalId));
        ByteStreamHelper.WriteDataReference(byteStream,
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.NameColorGlobalId));

        var v101 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v101; i++) byteStream.WriteVInt(i);
        }

        var v102 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v102; i++) ByteStreamHelper.WriteDataReference(byteStream, 0, 0);
        }

        var v103 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v103; i++)
            {
                byteStream.WriteVInt(i);
                ByteStreamHelper.WriteDataReference(byteStream, 0, 0);
            }
        }

        var v104 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v104; i++) ByteStreamHelper.WriteDataReference(byteStream, 0, 0);
        }

        var v105 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v105; i++) ByteStreamHelper.WriteDataReference(byteStream, 0, 0);
        }

        var v106 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v106; i++) ByteStreamHelper.WriteDataReference(byteStream, 0, 0);
        }

        var v107 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v107; i++) ByteStreamHelper.WriteDataReference(byteStream, 0, 0);
        } 

        byteStream.WriteVInt(0);
        byteStream.WriteVInt(
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.MaxTrophies), true);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);

        byteStream.WriteBoolean(true);

        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);

        new ForcedDrops().Encode(byteStream);

        if (byteStream.WriteBoolean(false))
            new TimedOffer().Encode(byteStream);

        if (byteStream.WriteBoolean(false))
            new TimedOffer().Encode(byteStream);

        byteStream.WriteBoolean(true);

        byteStream.WriteVInt(2);
        byteStream.WriteVInt(2);
        byteStream.WriteVInt(2);

        byteStream.WriteVInt(Helper.GetChangeNameCostByCount(Convert.ToInt32(
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.NameChangeCount))));
        byteStream.WriteVInt(LogicMath.Max(
            Convert.ToInt32(
                accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
                    AccountStructure.NameChangeEndTime)) - LogicTimeUtil.GetTimestamp(), 0));

        byteStream.WriteVInt(v1.Count);
        {
            foreach (var v1L in v1) v1L.Encode(byteStream);
        }

        var v108 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v108; i++) new AdStatus().Encode(byteStream);
        }

        byteStream.WriteVInt(0); // available battle tokens;
        byteStream.WriteVInt(-1);

        var v109 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v109; i++) byteStream.WriteVInt(i);
        }

        byteStream.WriteVInt(0); 
        byteStream.WriteVInt(0);

        var v110 = byteStream.WriteByte(1);
        {
            for (var i = 0; i < v110; i++) ByteStreamHelper.WriteDataReference(byteStream, 16, i);
        }
        
        byteStream.WriteString(v2.GetTextCodeByLanguage().ToUpper());
        byteStream.WriteString(
            accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.SupportedCreator),
            true);

        var v111 = byteStream.WriteVInt(v3.Count);
        {
            if (v111 > 0)
                foreach (var v111L in v3)
                    v111L.Value.Encode(byteStream);
        }

        var v112 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v112; i++) new CooldownEntry().Encode(byteStream);
        }

        var v113 = byteStream.WriteVInt(1);
        {
            for (var i = 0; i < v113; i++) new BrawlPassSeasonData(accountModel).Encode(byteStream);
        }

        var v114 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v114; i++) new ProLeagueSeasonData().Encode(byteStream);
        }

        if (byteStream.WriteBoolean(true)) new LogicQuests([]).Encode(byteStream);

        if (byteStream.WriteBoolean(true)) new VanityItems([]).Encode(byteStream);

        if (byteStream.WriteBoolean(true)) new LogicPlayerRankedSeasonData().Encode(byteStream);

        byteStream.WriteInt(0);
    }
}