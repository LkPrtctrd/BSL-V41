using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicGameModeVariationData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _chatSuggestionItemName = null!;
    private bool _disabled;
    private string _endNotification = null!;
    private int _friendlyMenuOrder;
    private string _gameModeIconName = null!;
    private string _gameModeRoomIconName = null!;
    private string _introDescText = null!;
    private string _introDescText2 = null!;
    private string _introText = null!;
    private string _opponentScoreSfx = null!;
    private string _scoreSfx = null!;
    private string _scoreText = null!;
    private string _scoreTextEnd = null!;
    private string _startNotification = null!;
    private int _variation;

    // LogicGameModeVariationData.

    public override void CreateReferences()
    {
        _variation = GetIntegerValue("Variation", 0);
        _disabled = GetBooleanValue("Disabled", 0);
        _chatSuggestionItemName = GetValue("ChatSuggestionItemName", 0);
        _gameModeRoomIconName = GetValue("GameModeRoomIconName", 0);
        _gameModeIconName = GetValue("GameModeIconName", 0);
        _scoreSfx = GetValue("ScoreSfx", 0);
        _opponentScoreSfx = GetValue("OpponentScoreSfx", 0);
        _scoreText = GetValue("ScoreText", 0);
        _scoreTextEnd = GetValue("ScoreTextEnd", 0);
        _friendlyMenuOrder = GetIntegerValue("FriendlyMenuOrder", 0);
        _introText = GetValue("IntroText", 0);
        _introDescText = GetValue("IntroDescText", 0);
        _introDescText2 = GetValue("IntroDescText2", 0);
        _startNotification = GetValue("StartNotification", 0);
        _endNotification = GetValue("EndNotification", 0);
    }

    public int GetVariation()
    {
        return _variation;
    }

    public bool GetDisabled()
    {
        return _disabled;
    }

    public string GetChatSuggestionItemName()
    {
        return _chatSuggestionItemName;
    }

    public string GetGameModeRoomIconName()
    {
        return _gameModeRoomIconName;
    }

    public string GetGameModeIconName()
    {
        return _gameModeIconName;
    }

    public string GetScoreSfx()
    {
        return _scoreSfx;
    }

    public string GetOpponentScoreSfx()
    {
        return _opponentScoreSfx;
    }

    public string GetScoreText()
    {
        return _scoreText;
    }

    public string GetScoreTextEnd()
    {
        return _scoreTextEnd;
    }

    public int GetFriendlyMenuOrder()
    {
        return _friendlyMenuOrder;
    }

    public string GetIntroText()
    {
        return _introText;
    }

    public string GetIntroDescText()
    {
        return _introDescText;
    }

    public string GetIntroDescText2()
    {
        return _introDescText2;
    }

    public string GetStartNotification()
    {
        return _startNotification;
    }

    public string GetEndNotification()
    {
        return _endNotification;
    }
}