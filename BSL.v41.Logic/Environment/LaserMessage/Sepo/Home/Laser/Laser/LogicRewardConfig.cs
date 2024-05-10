using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.Mathematical.Data;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;

public class LogicRewardConfig
{
    public void Encode(ByteStream byteStream)
    {
        new LogicCondition().Encode(byteStream);

        var c = new LogicGemOffer(ShopItemHelperTable.Coin, 1);
        {
            c.SetItem(GlobalId.CreateGlobalId(16, 0), true);
        }

        c.Encode(byteStream);
    }
}