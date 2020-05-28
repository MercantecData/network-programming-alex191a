using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.IO;
using System.Security;

namespace encoding
{
    class server
    {
        public server()
        {
            bool conn = true;
            int port = 5001;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localendpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(localendpoint);
            listener.Start();

            Console.WriteLine("leder efter potientel klient");
            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();
            ReceiveMessages(stream);
            while (conn)
            {

                Console.WriteLine("Skriv din besked");
                string besked = Console.ReadLine();
                byte[] buffersize = Encoding.UTF8.GetBytes(besked);

                stream.Write(buffersize, 0, buffersize.Length);
                Console.ReadKey();
            }
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
