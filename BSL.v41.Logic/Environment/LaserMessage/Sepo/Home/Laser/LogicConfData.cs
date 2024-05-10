using BSL.v41.Logic.Database.Account;
using BSL.v41.Logic.Dynamic;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Event;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;
using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser;

public class LogicConfData(AccountModel accountModel)
{
    public void Encode(ByteStream byteStream)
    {
        var v1 = (Dictionary<int, IntValueEntry>)accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(
            AccountStructure.IntValueEntryY); // object;
        int[] v2 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 20, 21, 22, 23, 24, 30, 31, 32];

        byteStream.WriteVInt(0);

        byteStream.WriteVInt(v2.Length);
        {
            foreach (var x in v2) new EventSlot(x).Encode(byteStream);
        }

        byteStream.WriteVInt(DynamicServerParameters.Event1DataMassive.Count);
        {
            foreach (var variable in DynamicServerParameters.Event1DataMassive) variable.Value.Encode(byteStream);
        }

        byteStream.WriteVInt(DynamicServerParameters.Event2DataMassive.Count);
        {
            foreach (var variable in DynamicServerParameters.Event2DataMassive) variable.Value.Encode(byteStream);
        }

        var v101 = byteStream.WriteVInt(1);
        {
            for (var i = 0; i < v101; i++) byteStream.WriteVInt(i);
        }

        var v102 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v102; i++) byteStream.WriteVInt(i);
        }

        var v103 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v103; i++) byteStream.WriteVInt(i);
        }

        byteStream.WriteBoolean(true);

        var v104 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v104; i++) new ReleaseEntry().Encode(byteStream);
        }

        byteStream.WriteVInt(v1.Count);
        {
            foreach (var intValueEntry in v1) intValueEntry.Value.Encode(byteStream);
        }

        var v105 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v105; i++) new TimedIntValueEntry().Encode(byteStream);
        }

        var v106 = byteStream.WriteVInt(0);
        {
            for (var i = 0; i < v106; i++) new CustomEvent().Encode(byteStream);
        }

        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
        byteStream.WriteVInt(0);
    }
}