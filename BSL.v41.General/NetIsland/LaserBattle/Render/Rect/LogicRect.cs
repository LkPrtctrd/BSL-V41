namespace BSL.v41.General.NetIsland.LaserBattle.Render.Rect;

public class LogicRect(int startX, int startY, int endX, int endY)
{
    private readonly int _endX = endX;
    private readonly int _endY = endY;
    private int _startX = startX;
    private int _startY = startY;

    public void Destruct()
    {
        _startX = 0;
        _startY = 0;
    }

    public bool IsInside(int x, int y)
    {
        if (_startX >= x) return false;
        if (_startY <= y)
            return _endX - 300 > x && _endY - 300 > y;

        return false;
    }

    public bool IsInside(LogicRect rect)
    {
        if (_startX >= rect._startX) return false;
        if (_startY <= rect._startY)
            return _endX > rect._endX && _endY > rect._endY;

        return false;
    }
}