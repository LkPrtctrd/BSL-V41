using System.Net.Sockets;
using BSL.v41.General.Network.Data;
using BSL.v41.General.Networking;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.General.Network;

public class TcpLaserSocketListener
{
    private readonly string _domain;

    private readonly int _secondsToFloodManagement;
    private readonly bool _wasCheckedToFlood;
    private int _allAllowedUsersConnectedCount;

    private int _lastUsersConnectedCount;
    private ManualResetEvent _manualResetEvent = null!;
    private bool _manualResetEventStarted;
    private int _remainedSecondsToFloodManagement;

    private SocketAcceptanceLevel _socketAcceptanceLvl;

    private Socket _tcpLaserSocket = null!;

    public TcpLaserSocketListener(string domain, bool wasCheckedToFlood, int secondsToFloodManagement = 10)
    {
        _socketAcceptanceLvl = SocketAcceptanceLevel.NowOpened;
        _domain = domain;
        _wasCheckedToFlood = wasCheckedToFlood;

        _secondsToFloodManagement = secondsToFloodManagement;
        _remainedSecondsToFloodManagement = secondsToFloodManagement;
        _manualResetEventStarted = false;

        _lastUsersConnectedCount = 1;
        _allAllowedUsersConnectedCount = 1;

        FloodAdministrative();
        FloodManagement();
    }

    public void StartSocket(bool restart = false)
    {
        _tcpLaserSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _tcpLaserSocket.Bind(Helper.GetFullyEndPointByDomain(_domain)); 
        _tcpLaserSocket.Listen(_secondsToFloodManagement * 10 * 24);

        _manualResetEvent = new ManualResetEvent(false); 

        _socketAcceptanceLvl = SocketAcceptanceLevel.NowOpened;
        {
            if (!restart)
                ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Start,
                    $"Network-server started! Information: " +
                    $"server protocol type: {_tcpLaserSocket.ProtocolType}; " +
                    $"server address: {Helper.GetIpFromDomain(_domain)}; " +
                    $"server port: {Helper.GetPortFromDomain(_domain)}; " +
                    $"server status: {Enum.GetName(typeof(SocketAcceptanceLevel), _socketAcceptanceLvl)}.");
            else
                ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Restart,
                    $"Network-server restarted! Information: " +
                    $"server protocol type: {_tcpLaserSocket.ProtocolType}; " +
                    $"server address: {Helper.GetIpFromDomain(_domain)}; " +
                    $"server port: {Helper.GetPortFromDomain(_domain)}; " +
                    $"server status: {Enum.GetName(typeof(SocketAcceptanceLevel), _socketAcceptanceLvl)}; " +
                    $"server restarted time: {DateTime.Now:yyyy/MM/dd-(dddd) HH:mm:ss}.");
        }

        _lastUsersConnectedCount = 1;
        _allAllowedUsersConnectedCount = 1;

        if (!_manualResetEventStarted) ManualResetUpdate();
    }

    public void StopSocket()
    {
        _manualResetEventStarted = false;

        _tcpLaserSocket.Close();

        _manualResetEvent.Dispose();
        _manualResetEvent.Close();
        _manualResetEvent.Reset();

        _socketAcceptanceLvl = SocketAcceptanceLevel.NowClosed;
        {
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Stop, "Network-server stopped! Information: " +
                                                                           $"server protocol type: {_tcpLaserSocket.ProtocolType}; " +
                                                                           $"server address: {Helper.GetIpFromDomain(_domain)}; " +
                                                                           $"server port: {Helper.GetPortFromDomain(_domain)}.");
        }

        _lastUsersConnectedCount = 1;
        _allAllowedUsersConnectedCount = 1;
    }

    public void FloodAdministrative()
    {
        new Thread(() =>
        {
            while (_wasCheckedToFlood)
            {
                Thread.Sleep(1 * 1000);

                _remainedSecondsToFloodManagement = _remainedSecondsToFloodManagement >= 0
                    ? --_remainedSecondsToFloodManagement
                    : _secondsToFloodManagement;

                if (_remainedSecondsToFloodManagement <= 0)
                    _lastUsersConnectedCount = Helper.GetChanceByPercentage(50) ? 1 : 2;
            }
        }).Start();
    }

    public void FloodManagement()
    {
        new Thread(() =>
        {
            while (_wasCheckedToFlood)
            {
                Thread.Sleep(2 * 1000);

                switch (_socketAcceptanceLvl)
                {
                    case SocketAcceptanceLevel.NowOpened:
                    {
                        if (_lastUsersConnectedCount > _secondsToFloodManagement * 1.5 ||
                            _lastUsersConnectedCount >
                            _allAllowedUsersConnectedCount + _secondsToFloodManagement / (24 / 10) ||
                            _lastUsersConnectedCount + _allAllowedUsersConnectedCount >
                            _secondsToFloodManagement * 10 * 24)
                        {
                            _socketAcceptanceLvl = SocketAcceptanceLevel.ClosingStage;
                            {
                                _remainedSecondsToFloodManagement = _secondsToFloodManagement;
                            }
                        }

                        break;
                    }
                    case SocketAcceptanceLevel.ClosingStage:
                    {
                        if (_remainedSecondsToFloodManagement <= 0)
                        {
                            StopSocket();
                            _remainedSecondsToFloodManagement = _secondsToFloodManagement;
                        }

                        break;
                    }
                    case SocketAcceptanceLevel.NowClosed:
                    {
                        if (_remainedSecondsToFloodManagement <= 0)
                        {
                            _socketAcceptanceLvl = SocketAcceptanceLevel.OpeningStage;
                            {
                                _remainedSecondsToFloodManagement = _secondsToFloodManagement;
                            }
                        }

                        break;
                    }
                    case SocketAcceptanceLevel.OpeningStage:
                    {
                        if (_remainedSecondsToFloodManagement <= 0) StartSocket(true);

                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }).Start();
    }

    public void ManualResetUpdate()
    {
        _manualResetEventStarted = true;

        new Thread(() =>
        {
            while (true)
            {
                _manualResetEvent.Reset();
                _tcpLaserSocket.BeginAccept(StartAccepting, null);
                _manualResetEvent.WaitOne();
            }
        }).Start();
    }

    public void StartAccepting(IAsyncResult iAsyncResult)
    {
        try
        {
            var socket = _tcpLaserSocket.EndAccept(iAsyncResult);
            {
                ProcessInAcceptMethod(socket);
            }
        }
        catch (Exception)
        {
            // ignored.
        }
    }

    public void ProcessInAcceptMethod(Socket socket)
    {
        _allAllowedUsersConnectedCount++;
        _lastUsersConnectedCount++;

        var connection = new Connection();
        {
            connection.SetListener(_tcpLaserSocket);
            connection.SetUserListener(socket);
        }

        var serverConnection = new ServerConnection(connection);
        {
            serverConnection.Initialize();
        }

        var task = Task.Factory.StartNew(() =>
        {
            connection.Timer = new Timer(serverConnection.Update, null, 0, 100);
        });

        socket.BeginReceive(connection.ByteBuffer!, 0, 1024, SocketFlags.None, OnReceive, connection);
        {
            serverConnection.OwnTask = task;
            _manualResetEvent.Set();
        }
    }

    public static void OnReceive(IAsyncResult iAsyncResult)
    {
        try
        {
            var connection = (Connection)iAsyncResult.AsyncState!;
            {
                var data = connection.GetListener()!.EndReceive(iAsyncResult);
                {
                    if (data <= 0)
                    {
                        connection.Close();
                        return;
                    }

                    connection.MemoryStream!.Write(connection.ByteBuffer!, 0, data);
                    {
                        if (connection.GetMessaging()!.NextMessage() != 0)
                        {
                            connection.Close();
                            return;
                        }
                    }
                }
            }

            connection.GetUserListener()!.BeginReceive(connection.ByteBuffer!, 0, 1024, SocketFlags.None, OnReceive,
                connection);
        }
        catch
        {
            // ignored.
        }
    }

    public static void OnSend(IAsyncResult iAsyncResult)
    {
        try
        {
            ((Socket)iAsyncResult.AsyncState!).EndSend(iAsyncResult);
        }
        catch (Exception)
        {
            // ignored.
        }
    }
}