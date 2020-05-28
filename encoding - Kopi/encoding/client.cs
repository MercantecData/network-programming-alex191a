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
    class client
    {
        public client()
        {
            bool conn = true;
            TcpClient client = new TcpClient();
            int port = 5000;
            IPAddress ip = IPAddress.Parse("172.16.112.238");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            ReceiveMessages(stream);
            while (conn)
            {
                Console.WriteLine("Du kan skrive beskeder nu");
                string besked = Console.ReadLine();
                byte[] buffersize = Encoding.UTF8.GetBytes(besked);
                stream.Write(buffersize, 0, buffersize.Length);
            }
            client.Close();
        }
        public async void ReceiveMessages(NetworkStream stream)
        {
            bool conn = true;
            while (conn)
            {
                byte[] buffersize = new byte[1000];
                // number of bytes read
                int NOBR = await stream.ReadAsync(buffersize, 0, 1000);
                string RM = Encoding.UTF8.GetString(buffersize, 0, NOBR);
                Console.WriteLine("\n" + RM);
            }
        }
    }
}
