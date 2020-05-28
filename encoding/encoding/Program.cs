using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace encoding
{
    class Program
    {
        static void Main()
        {
            Program program = new Program();
        }
        public Program()
        {
            bool die = true;
            while (die)
            {
                Console.WriteLine(" Indtast 1 for at skrive besked.\n Indtast 2 for at hente besked.\n indtast 3 for begge");
                string reader = Console.ReadLine();
                while(!int.TryParse(reader,out int value))
                {
                    if (reader == "exit")
                    {
                        die = false;
                        reader = "0";
                    }
                    else
                    {
                        Console.WriteLine("Skriv exit for at stoppe programmet");
                        reader = Console.ReadLine();
                    }
                }
                if (Convert.ToInt32(reader) == 1)
                {
                    client program = new client();
                }
                else if (Convert.ToInt32(reader) == 2)
                {
                    server program = new server();
                }
                else if (Convert.ToInt32(reader) == 3)
                {
                    Console.WriteLine("press enter to write a message. press everything else to load if you got a message");
                    ClientServer();
                }
            }


        }
        void ClientServer()
        {
            string ipInput= "fuck";
            bool bol = true;
            int port = 0 ;
            IPAddress ip = IPAddress.Any;
            IPEndPoint endpoint = new IPEndPoint(ip, 5001);

            TcpListener listener = new TcpListener(endpoint);
            listener.Start();
            while (bol)
            {
                Smethods funktioner = new Smethods();
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    if (ipInput == "fuck")
                    {
                        Console.WriteLine("skriv den ip du vil sende til\n example: 127.0.0.1");
                        ipInput = Console.ReadLine();
                        Console.WriteLine("Skriv hvilken port du vil sende det til\n example 5001");
                        port = Convert.ToInt32(Console.ReadLine());
                    }
                        TcpClient clients = new TcpClient();
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
                funktioner.ReceiveMessages(stream);
            }
            listener.Stop();


        }
    }
}
