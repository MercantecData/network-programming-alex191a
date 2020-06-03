using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.IO;
using System.Runtime.Remoting;

namespace encoding
{
    class client
    {
        public client()
        {
            bool conn = true;
            TcpClient client = new TcpClient();
            Console.WriteLine("skriv porten. Example: 5001");
            int port = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("skriv ip'en. \n example: 127.0.0.1");
            IPAddress ip = IPAddress.Parse(Console.ReadLine());
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            Smethods smethod = new Smethods();
            smethod.ReceiveMessages(stream);
            Console.WriteLine("Du kan skrive beskeder nu");
            while (conn)
            {
                smethod.cmessage(stream);
            }
            client.Close();
        }
    }
}
