using BSL.v41.Titan.Mathematical;

namespace BSL.v41.General.NetIsland.LaserBattle.Render.Help;

public enum FigureCreator
{
    Circle,
    Oval,
    Triangle,
    Square,
    RectangleLine,
    Heart,
    Jug,
    Star
}

public static class FigureCreatorExtensions
{
    public static List<LogicVector2> DrawFigure(this FigureCreator figureCreator, LogicVector2 centralCoordinates,
        int radius, int radius2 = 10, int maxObj = 100)
    {
        var objectsNumber = Math.Min(radius, maxObj);
        var centerX = centralCoordinates.GetX();
        var centerY = centralCoordinates.GetY();
        var angleStep = 360f / objectsNumber;
        var coords = new List<LogicVector2>();

        switch (figureCreator)
        {
            case FigureCreator.Star:
            {
                // todo.
                break;
            }
            
            case FigureCreator.Jug:
            {
                while (objectsNumber > 0)
                {
                    var angle = (objectsNumber - 1) * angleStep;
                    var x = centerX + radius * (Math.Cos(angle * 2) * Math.Cos(angle) + Math.Cos(angle * 3) * Math.Sin(angle));
                    var y = centerY + radius * (Math.Cos(angle * 2) * Math.Sin(angle) - Math.Cos(angle) * Math.Sin(angle * 2));

                    coords.Add(new LogicVector2((int)x, (int)y));
                    objectsNumber--;
                }

                break;
            }
            
            case FigureCreator.Heart:
            {
                while (objectsNumber > 0)
                {
                    var angle = (objectsNumber - 1) * angleStep;
                    var x = centerX + radius * (16 * Math.Pow(Math.Sin(angle), 3));
                    var y = centerY - radius * (13 * Math.Cos(angle) - 5 * Math.Cos(2 * angle) -
                                                2 * Math.Cos(3 * angle) - Math.Cos(4 * angle));

                    coords.Add(new LogicVector2((int)x, (int)y));
                    objectsNumber--;
                }

                break;
            }

            case FigureCreator.Circle:
            {
                while (objectsNumber > 0)
                {
                    var angle = (objectsNumber - 1) * angleStep;
                    var x = centerX + radius * Math.Cos(angle * Math.PI / 180);
                    var y = centerY + radius * Math.Sin(angle * Math.PI / 180);

                    coords.Add(new LogicVector2((int)x, (int)y));
                    objectsNumber--;
                }

                break;
            }

            case FigureCreator.Triangle:
            {
                objectsNumber = 3;

                while (objectsNumber > 0)
                {
                    var angle = (objectsNumber - 1) * angleStep;
                    var x = centerX + radius * Math.Cos(angle * Math.PI / 180);
                    var y = centerY + radius * Math.Sin(angle * Math.PI / 180);

                    coords.Add(new LogicVector2((int)x, (int)y));
                    objectsNumber--;
                }

                break;
            }

            case FigureCreator.Oval:
            {
                while (objectsNumber > 0)
                {
                    var angle = (objectsNumber - 1) * angleStep;
                    var x = centerX + radius * Math.Cos(angle * Math.PI / 180);
                    var y = centerY + radius2 * Math.Sin(angle * Math.PI / 180);

                    coords.Add(new LogicVector2((int)x, (int)y));
                    objectsNumber--;
                }

                break;
            }

            case FigureCreator.RectangleLine:
            {
                while (objectsNumber > 0)
                {
                    var x = centerX - radius / 2 + (objectsNumber - 1) * radius / objectsNumber;
                    var y = centerY - radius2 / 2 + (objectsNumber - 1) * radius2 / objectsNumber;

                    coords.Add(new LogicVector2(x, y));
                    objectsNumber--;
                }

                break;
            }

            case FigureCreator.Square:
            {
                while (objectsNumber > 0)
                {
                    var angle = (objectsNumber - 1) * angleStep;

                    int x, y;
                    {
                        switch (angle)
                        {
                            case < 90:
                                x = (int)(centerX + angle / 90 * radius);
                                y = centerY;
                                break;
                            case < 180:
                                x = centerX + radius;
                                y = (int)(centerY + (angle - 90) / 90 * radius);
                                break;
                            case < 270:
                                x = (int)(centerX + radius - (angle - 180) / 90 * radius);
                                y = centerY + radius;
                                break;
                            default:
                                x = centerX;
                                y = (int)(centerY + radius - (angle - 270) / 90 * radius);
                                break;
                        }
                    }

                    coords.Add(new LogicVector2(x, y));
                    objectsNumber--;
                }

                break;
            }

            default:
                throw new ArgumentOutOfRangeException(nameof(figureCreator), figureCreator, null);
        }

        return coords;
    }
}