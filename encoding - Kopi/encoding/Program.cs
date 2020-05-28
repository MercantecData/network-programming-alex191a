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
                Console.WriteLine("Indtast 1 for at skrive besked.\n Indtast 2 for at hente besked.\n indtast 3 for begge \n ekstra valg 4");
                string reader = Console.ReadLine();
                while(!int.TryParse(reader,out int value))
                {

                }
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
                else if (Convert.ToInt32(reader) == 4)
                {
                    Console.WriteLine("indtast 1 for client.\n indtast 2 for server");
                    reader = Console.ReadLine();
                    while (!int.TryParse(reader, out int value))
                    {
                        Console.WriteLine("indtast et tal istedet");
                        reader = Console.ReadLine();
                    }

                    if (Convert.ToInt32(reader) == 1)
                    {
                        client program = new client();
                    }
                    else if (Convert.ToInt32(reader) == 2)
                    {
                        server program = new server();
                    }
                }
                    if (reader == "exit")
                {
                    die = false;
                }
            }


        }
        public void Clientdie()
        {
        }
        public async void connectingServer()
        {
            bool conn = true;
            while (conn) 
            {
                int port = 5001;
                IPAddress ip = IPAddress.Any;
                IPEndPoint endpoint = new IPEndPoint(ip, port);
                TcpListener listener = new TcpListener(endpoint);
                TcpClient clientIncomming = await listener.AcceptTcpClientAsync();
                listener.Start();
                Console.WriteLine("leder efter klienter");

            }
        }
        public async void recivedMessage(NetworkStream stream, int choice)
        {
            bool conn = true;
            while (conn) {
                byte[] buffersize = new byte[1000];
                int nbytesRead = await stream.ReadAsync(buffersize, 0, 1000);

                string messageRecieve = Encoding.UTF8.GetString(buffersize, 0, nbytesRead);
                Console.WriteLine("\n" + messageRecieve);
                if (choice == 1)
                {

                }
            }
 
        }
         public void Server()
         {
            bool conn = true;
            int port = 5001;
            IPAddress ip = IPAddress.Any;
            IPEndPoint endpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(endpoint);

          listener.Start();
            Console.WriteLine("leder efter klienter");
            while (conn)
            {
                //connectingServer();
                TcpClient clientIncomming = listener.AcceptTcpClient();

                NetworkStream stream = clientIncomming.GetStream();
                if (stream.DataAvailable)
                {
                    recivedMessage(stream, 2);
                }
                Console.WriteLine("klient fundet. skriv besked");
                string message = Console.ReadLine();
                byte[] buffersize = Encoding.UTF8.GetBytes(message);

                stream.Write(buffersize, 0, buffersize.Length);
            }
            listener.Stop();
         }
       public void Client()
       {
            bool conn = true; 
            TcpClient client = new TcpClient();

            int port = 5001;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEndpoint = new IPEndPoint(ip, port);

            client.Connect(remoteEndpoint);
         
            
                NetworkStream stream2 = client.GetStream();
                recivedMessage(stream2, 1);


            //messages sends to choosen server
            Console.WriteLine("skriv din besked her");
                string message = Console.ReadLine();
                byte[] buffersize = Encoding.UTF8.GetBytes(message);

            stream2.Write(buffersize, 0, message.Length);
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
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {   if (ipInput == "fuck")
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
                recivedMessage(stream,0);
            }
            listener.Stop();


        }
    }
}
