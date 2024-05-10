using BSL.v41.Titan.DataStream;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Player;

public class PlayerDisplayData
{
    private string _avatarName = null!;
    private int _nameColor;
    private int _playerThumbnail;

    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteString(_avatarName);
        byteStream.WriteVInt(100);
        byteStream.WriteVInt(_playerThumbnail);
        byteStream.WriteVInt(_nameColor);
        byteStream.WriteVInt(0);
    }

    public void Decode(ByteStream byteStream)
    {
        _avatarName = byteStream.ReadString(1024);
        byteStream.ReadVInt();
        _playerThumbnail = byteStream.ReadVInt();
        _nameColor = byteStream.ReadVInt();
        byteStream.ReadVInt();
    }

    public string GetAvatarName()
    {
        return _avatarName;
    }

    public void SetAvatarName(string avatarName)
    {
        _avatarName = avatarName;
    }

    public int GetNameColor()
    {
        return _nameColor;
    }

    public void SetNameColor(int nameColor)
    {
        _nameColor = nameColor;
    }

    public int GetPlayerThumbnail()
    {
        return _playerThumbnail;
    }

    public void SetPlayerThumbnail(int playerThumbnail)
    {
        _playerThumbnail = playerThumbnail;
    }
}