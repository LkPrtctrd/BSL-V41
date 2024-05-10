using System.Net;
using System.Text;
using BSL.v41.Supercell.Titan.CommonUtils.Utils.Own;
using BSL.v41.Titan.Graphic;
using BSL.v41.Titan.Utilities;

namespace BSL.v41.General.Network;

public class HttpLaserSocketListener(string host, int port)
{
    public void StartSocket()
    {
        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Start, $"Network-server started! Information: " +
                                                                        $"server protocol type: Http; " +
                                                                        $"server domain: http://{host}:{port}/.");

        new Thread(() =>
        {
            var listener = new HttpListener();
            {
                listener.Prefixes.Add($"http://{host}:{port}/");
                listener.Start();
            }

            while (true)
            {
                var context = listener.GetContext();
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    var request = context.Request;
                    var response = context.Response;

                    const string responseString =
                        "<html><body><head><title>BrawlStars-Administrative</title></head><form method='post' action='/logic'><label for='mode'>Select mode: </label><select name='mode'><option value='v00000201'>ENTER COMMAND</option><option value='v00000202'>SERVER STATUS</option></select><br/><br/><input type='submit' value='Submit'/></form></body></html>";

                    var pageContent = responseString;

                    if (request.HttpMethod == "POST")
                    {
                        var mode =
                            new StreamReader(request.InputStream, request.ContentEncoding).ReadToEnd().Split('=')[1];

                        switch (mode)
                        {
                            case "v00000202":
                                pageContent = "<html><body><h1>server status: unknown now</h1></body></html>";
                                pageContent += """
                                                <br><br> <html>
                                                    <head>
                                                        <title>BrawlStars-Administrative</title>
                                                    </head>
                                                    <body>
                                                        <form method='get' action='/'>
                                                            <input type='submit' value='Exit'>
                                                        </form>
                                                    </body>
                                                </html>
                                               """;
                                break;
                            case "v00000201":
                                pageContent = """
                                              
                                                                              <html>
                                                                                  <head><title>BrawlStars-Administrative</title></head>
                                                                                  <body>
                                                                                      <h1>Enter values:</h1>
                                                                                      <form method='post' action='/'>
                                                                                          <input type='text' name='v00000401' placeholder='your command'><br><br>
                                                                                          <input type='submit' value='Submit'>
                                                                                      </form>
                                                                                  </body>
                                                                              </html>
                                              """;
                                break;
                            default:
                            {
                                try
                                {
                                    if (mode[..3] == "%2F")
                                    {
                                        mode = mode[3..];
                                        {
                                            ExecuteCommand(mode);
                                        }

                                        pageContent +=
                                            "<html><head><title>BrawlStars-Administrative</title></head><body><h1>Last command was executed correctly!</h1></body></html>";
                                        {
                                            // ops.
                                        }
                                    }
                                    else
                                    {
                                        pageContent +=
                                            "<html><head><title>BrawlStars-Administrative</title></head><body><h1>Last command was executed incorrectly!</h1></body></html>";
                                    }
                                }
                                catch (Exception)
                                {
                                    // ignored.
                                }

                                break;
                            }
                        }
                    }

                    var buffer = Encoding.UTF8.GetBytes(pageContent);
                    {
                        response.ContentLength64 = buffer.Length;
                    }

                    var output = response.OutputStream;
                    {
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }
                }, null);
            }
        }).Start();
    }

    private static void ExecuteCommand(string command)
    {
        command = command.Replace("%23", "#");
        command = command.Replace("%2F", "/");

        var args = command.Split('+');
        var commandLvl = args.Length;
        {
            switch (commandLvl)
            {
                case 1:
                    switch (args[0].ToLower())
                    {
                        case "exit":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    }

                    break;
                case 2:
                {
                    var playerTag = args[1].ToUpper();

                    switch (args[0].ToLower())
                    {
                        case "close":
                        {
                            var playerAccountId =
                                new LogicLongToCodeConverterUtil(Table.Hashtag, Table.ConversionTagCharacters)
                                    .ToId(playerTag)
                                    .GetLowerInt();
                            {
                                // todo: close session.
                            }
                            break;
                        }
                    }

                    break;
                }
            }
        }
    }
}