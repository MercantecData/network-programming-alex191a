using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.IO;

namespace encoding
{
    class Program
    {
        static void Main(string[] args)
        {
            void Server()
            {
                IPAddress ip = IPAddress.Any;
                IPEndPoint endpoint = new IPEndPoint(ip, 5001);

                TcpListener listener = new TcpListener(endpoint);
                listener.Start();

                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                byte[] buffersize = new byte[1000];
                int nbytesRead = stream.Read(buffersize, 0, 1000);
                string messageRecieve = Encoding.UTF8.GetString(buffersize, 0, nbytesRead);
                Console.WriteLine(messageRecieve);
                listener.Stop();
            }
            void Client()
            {
                TcpClient client = new TcpClient();
                int port = 5001;
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEndpoint = new IPEndPoint(ip, port);
                client.Connect(remoteEndpoint);
                NetworkStream stream = client.GetStream();
                string message = Console.ReadLine();
                byte[] buffersize = Encoding.UTF8.GetBytes(message);

                stream.Write(buffersize, 0, message.Length);
                client.Close();
            }
            void ClientServer()
            {
                bool bol = true;
                IPAddress ip = IPAddress.Any;
                IPEndPoint endpoint = new IPEndPoint(ip, 5001);

                TcpListener listener = new TcpListener(endpoint);
                listener.Start();
                while (bol)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine("skriv den ip du vil sende til\n example: 127.0.0.1");
                        string ipInput = Console.ReadLine();
                        TcpClient clients = new TcpClient();
                        int port = 5001;
                        IPAddress ipp = IPAddress.Parse(ipInput);
                        IPEndPoint remoteEndpoint = new IPEndPoint(ipp, port);
                        clients.Connect(remoteEndpoint);
                        NetworkStream streams = clients.GetStream();
                        string message = Console.ReadLine();
                        byte[] buffersixe = Encoding.UTF8.GetBytes(message);

                        streams.Write(buffersixe, 0, message.Length);
                        clients.Close();
                    }
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                        byte[] buffersize = new byte[1000];
                        int nbytesRead = stream.Read(buffersize, 0, 1000);
                        string messageRecieve = Encoding.UTF8.GetString(buffersize, 0, nbytesRead);
                        Console.WriteLine(messageRecieve);
                }
                listener.Stop();
                

            }
            bool die = true;
            while (die)
            {
                Console.WriteLine("Indtast 1 for at skrive besked.\n Indtast 2 for at hente besked.\n indtast 3 for begge");
                string reader = Console.ReadLine();
                while (int.TryParse(reader, out int value))
                {
                    if (Convert.ToInt32(reader) == 1)
                    {
                        Client();
                    }
                    else if (Convert.ToInt32(reader) == 2)
                    {
                        Server();
                    }
                    else if (Convert.ToInt32(reader) == 3)
                    {
                        Console.WriteLine("press enter to write a message. press everything else to load if you got a message");
                        ClientServer();
                    }
                }
                if (reader == "exit")
                {
                    die = false;
                }
            }
        }
    }
}
