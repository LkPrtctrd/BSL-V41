using BSL.v41.Logic.Database.Account;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Titan.Mathematical;
using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Player;

public class LogicPlayer
{
    public readonly List<LogicCardData> CardInfo = [];
    public readonly List<int> HeroInfo = [];
    public readonly List<int> PinInfo = [];

    private AccountModel _accountModel = null!;
    private bool _isBot;
    private bool _isBounty;
    private PlayerDisplayData _playerDisplayData = null!;
    private int _playerIndex;
    private int _teamIndex;
    private int _ultiMul;
    public int InputCounter;
    public int OwnObjectId;
    public long SessionId;

    public bool TcpModeRefract { get; set; }

    public void Encode(ByteStream byteStream)
    {
        new LogicLong(0, (int)(!_isBot
            ? _accountModel.GetAccountModel().GetAccountId()
            : 10000000 + Helper.GenerateRandomIntForBetween(100, 200))).Encode(byteStream);
        byteStream.WriteVInt(_playerIndex);
        byteStream.WriteVInt(_teamIndex);
        byteStream.WriteVInt(0);
        byteStream.WriteInt(0);
        if (byteStream.WriteByte(1) > 0)
        {
            ByteStreamHelper.WriteDataReference(byteStream, GlobalId.CreateGlobalId(16, HeroInfo[0]));

            if (byteStream.WriteBoolean(CardInfo.Count > 1)) new LogicHeroUpgrades().Encode(byteStream, CardInfo);
            if (byteStream.WriteBoolean(true)) new LogicBattleEmotes().Encode(byteStream);

            ByteStreamHelper.WriteDataReference(byteStream, HeroInfo[3] > 0 ? 29000000 + HeroInfo[3] : 0);
        }

        _playerDisplayData.Encode(byteStream);
        byteStream.WriteBoolean(false);
        byteStream.WriteBoolean(false);
        byteStream.WriteByte(0);
    }

    public void Decode(ByteStream byteStream)
    {
        _ = byteStream.ReadLong();
        _playerIndex = byteStream.ReadVInt();
        _teamIndex = byteStream.ReadVInt();
        byteStream.ReadVInt();
        byteStream.ReadInt();
        ByteStreamHelper.ReadDataReference(byteStream);
        ByteStreamHelper.ReadDataReference(byteStream);
        byteStream.ReadBoolean();
        byteStream.ReadBoolean();

        new PlayerDisplayData().Decode(byteStream);
        byteStream.ReadBoolean();
    }

    public AccountModel GetAccountModel()
    {
        return _accountModel;
    }

    public void SetAccountModel(AccountModel accountModel)
    {
        _accountModel = accountModel;
    }

    public void SetBot(bool isBot)
    {
        _isBot = isBot;
    }

    public bool IsBot()
    {
        return _isBot;
    }

    public int GetPlayerIndex()
    {
        return _playerIndex;
    }

    public void SetPlayerIndex(int playerIndex)
    {
        _playerIndex = playerIndex;
    }

    public int GetTeamIndex()
    {
        return _teamIndex;
    }

    public void SetTeamIndex(int teamIndex)
    {
        _teamIndex = teamIndex;
    }

    public PlayerDisplayData GetPlayerDisplayData()
    {
        return _playerDisplayData;
    }

    public void SetPlayerDisplayData(PlayerDisplayData playerDisplayData)
    {
        _playerDisplayData = playerDisplayData;
    }

    public bool IsBounty()
    {
        return _isBounty;
    }

    public void SetBounty(bool bounty)
    {
        _isBounty = bounty;
    }

    public int ChargeUlti(bool wasReturnValue, int damageValue, int ultiChargeMul)
    {
        if (wasReturnValue) return _ultiMul;

        _ultiMul += ultiChargeMul * (damageValue / 100);
        {
            _ultiMul = LogicMath.Min(_ultiMul, 4000);
            {
                return _ultiMul;
            }
        }
    }

    public bool HasUlti()
    {
        return _ultiMul >= 4000;
    }

    public int GetDps(int a1)
    {
        // todo.
        return 0;
    }
}
