using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HipperChat.Core.Helpers
{
    public class TcpServer : IDisposable
    {
        private const int _incomingBufferSize = 10280;
        private const int _port = 43823;


        private readonly TcpListener _tcpListener;
        private readonly string _internalIp;

        public TcpServer()
        {
            _internalIp = GetLocalIpAddress();
            _tcpListener = new TcpListener(IPAddress.Any, _port);
            new Task(ListenForClients).Start();
        }

        public void Dispose()
        {
            Stop();
        }

        private void Stop()
        {
            try
            {
                _tcpListener.Stop();
            }
            catch { }
        }

        private string GetLocalIpAddress()
        {
            string response;
            using (var client = new WebClient())
            {
                response = client.DownloadString("http://checkip.dyndns.org");
            }

            var ipMatch = Regex.Match(response, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            if (!ipMatch.Success)
            {
                throw new Exception("Couldn't find an IP address in " + response);
            }

            return ipMatch.Value;
        }

        private void ListenForClients()
        {
            _tcpListener.Start();

            try
            {
                while (true)
                {
                    // Blocks until there's an incoming connection
                    var client = _tcpListener.AcceptTcpClient();
                    new Task(() => HandleClientCommunication(client)).Start();
                }
            }
            finally
            {
                Stop();
            }
        }

        private void HandleClientCommunication(TcpClient client)
        {
            var stream = client.GetStream();
            var message = new byte[_incomingBufferSize];

            try
            {
                while (true)
                {
                    var bytesRead = stream.Read(message, 0, _incomingBufferSize);

                    // Disconnect
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    var text = Encoding.UTF8.GetString(message, 0, bytesRead);
                    System.Console.WriteLine(text);
                }
            }
            finally
            {
                Stop();
            }
        }
    }
}
