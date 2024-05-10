using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Home.Laser.Laser;

public class VanityItems(List<VanityItemEntry> vanityItemEntries)
{
    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteVInt(vanityItemEntries.Count);
        {
            if (vanityItemEntries.Count <= 0) return;

            foreach (var vanityItemEntry in vanityItemEntries) vanityItemEntry.Encode(byteStream);
        }
    }
}