using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Titan.Mathematical.Data;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;

public class LogicGemOffer(ShopItemHelperTable shopItemHelperTable, int count)
{
    private int _itemGlobalId;
    private int _itemGlobalIdX;

    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteVInt((int)shopItemHelperTable);
        byteStream.WriteVInt(_itemGlobalIdX > 0 ? 0 : count);
        ByteStreamHelper.WriteDataReference(byteStream, _itemGlobalIdX > 0 ? 0 : _itemGlobalId);
        byteStream.WriteVInt(_itemGlobalIdX > 1000000 ? GlobalId.GetInstanceId(_itemGlobalIdX) : _itemGlobalIdX);
    }

    public void SetItem(int itemGlobalId, bool isX = false)
    {
        _itemGlobalId = itemGlobalId;
        if (isX) _itemGlobalIdX = itemGlobalId;
    }

    public void SetSkin(int skinInstanceId)
    {
        _itemGlobalIdX = skinInstanceId;
    }
}