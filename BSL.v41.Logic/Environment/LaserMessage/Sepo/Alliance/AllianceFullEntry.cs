using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance;

public class AllianceFullEntry
{
    private string _allianceDescription = null!;
    private AllianceHeaderEntry _allianceHeaderEntry = null!;
    private Dictionary<long, AllianceMemberEntry> _allianceMemberEntries = [];

    public void Encode(ByteStream byteStream, long myAccountId = -1)
    {
        _allianceHeaderEntry.Encode(byteStream);
        byteStream.WriteString(_allianceDescription);

        byteStream.WriteVInt(_allianceHeaderEntry.IsDeleted() ? -1 : _allianceMemberEntries.Count);
        {
            foreach (var allianceMemberEntry in _allianceMemberEntries)
                allianceMemberEntry.Value.Encode(byteStream, myAccountId);
        }
    }

    public void SetAllianceMembers(Dictionary<long, AllianceMemberEntry> allianceMemberEntries)
    {
        _allianceMemberEntries = allianceMemberEntries;
    }

    public void SetAllianceHeaderEntry(AllianceHeaderEntry allianceHeaderEntry)
    {
        _allianceHeaderEntry = allianceHeaderEntry;
    }

    public void SetAllianceDescription(string allianceDescription)
    {
        _allianceDescription = allianceDescription;
    }
}