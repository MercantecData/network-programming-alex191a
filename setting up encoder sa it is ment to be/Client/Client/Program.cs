using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.IO;

namespace client
{
    class client
    {
        static void Main()
        {
            bool conn = true;
            client program = new client();
            Console.WriteLine("vil du lukke?: 1");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int value))
            {
                if (Convert.ToInt32(input) == 1)
                {
                    conn = false;
                }
            }
        }
        public client()
        {

            TcpClient client = new TcpClient();
            int port = 5001;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            ReceiveMessages(stream);

            Console.WriteLine("Du kan skrive beskeder nu");
            string besked = Console.ReadLine();
            byte[] buffersize = Encoding.UTF8.GetBytes(besked);
            client.Close();
        }
        public async void ReceiveMessages(NetworkStream stream)
        {
            byte[] buffersize = new byte[1000];
            // number of bytes read
            int NOBR = await stream.ReadAsync(buffersize, 0, 1000);
            string RM = Encoding.UTF8.GetString(buffersize, 0, NOBR);
            Console.WriteLine("\n" + RM);
        }


    }
}