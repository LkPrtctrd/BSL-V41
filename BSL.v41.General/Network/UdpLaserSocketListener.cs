using System.Net;
using System.Net.Sockets;
using BSL.v41.General.Cloud;
using BSL.v41.General.Networking.LaserUdp;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.General.Network;

public class UdpLaserSocketListener(int port)
{
    public readonly Dictionary<long, IPEndPoint>? Sessions = new();
    private Thread? _thread;
    public Socket? UdpAdministrator;

    public void StartSocket()
    {
        UdpAdministrator = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpAdministrator!.Bind(new IPEndPoint(IPAddress.Any, port));

        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Start, $"Network-server started! Information: " +
                                                                        $"server protocol type: {UdpAdministrator.ProtocolType}; " +
                                                                        $"server port: {port}.");

        _thread = new Thread(Receive);
        _thread.Start();
    }

    [Obsolete("Obsolete")]
    public void StopSocket()
    {
        _thread!.Abort();
        UdpAdministrator!.Close();
        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Stop, $"Network-server stopped! Information: " +
                                                                       $"server protocol type: {UdpAdministrator.ProtocolType}; " +
                                                                       $"server port: {port}.");
    }

    private void Receive()
    {
        while (true)
        {
            var buffer = new byte[1024];
            EndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
            var bytesRead = UdpAdministrator!.ReceiveFrom(buffer, ref ipEndPoint);

            var byteStream = new ByteStream(buffer, bytesRead);
            try
            {
                long sessionId = byteStream.ReadLong();
                byteStream.ReadShort();

                if (sessionId < Saver.LastUdpSessionId / 2) return;

                Sessions!.TryAdd(sessionId, (IPEndPoint)ipEndPoint);
                Task.Run(() => { new UdpPacketHandler(sessionId, byteStream).Receive(); });
            }
            catch
            {
                // ignored.
            }
        }
    }
}