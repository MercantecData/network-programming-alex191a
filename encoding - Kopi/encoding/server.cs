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
            Smethods smethod = new Smethods();
            smethod.serverInfo(port);

            IPAddress ip = IPAddress.Any;
            IPEndPoint localendpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(localendpoint);
            listener.Start();
            Console.WriteLine("leder efter potientel klient");
            smethod.acceptconn(listener);
            Console.WriteLine("Skriv din besked");
            while (conn)
            {
                smethod.sMessage();
            }
        }
    }
}
