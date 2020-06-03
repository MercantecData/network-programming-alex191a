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
using System.Security.Policy;

namespace encoding
{
    public class users
    {
        public string navne;
        public TcpClient brugere;
        public users(string navn, TcpClient brugeren)
        {
            this.navne = navn;
            this.brugere = brugeren;
        }
    }
    class Smethods
    {
        public static List<users> userlist = new List<users>();

        public void cmessage(NetworkStream Stream) 
        {

            string besked = Console.ReadLine();
            byte[] buffersize = Encoding.UTF8.GetBytes(besked);
            Stream.Write(buffersize, 0, buffersize.Length);

        }
        public void sMessage()
        {
           string besked = Console.ReadLine() ;
            byte[] buffersize = Encoding.UTF8.GetBytes(besked);
            foreach (users bruger in userlist)
            {
                bruger.brugere.GetStream().Write(buffersize, 0, buffersize.Length);
            }
        }
        public async void echoMmessages(TcpClient brugern)
        {
            bool conn = true;
            while (conn)
            {
                NetworkStream stream = brugern.GetStream();
                byte[] buffersize = new byte[1000];
                int NOBR = await stream.ReadAsync(buffersize, 0, 1000);
                string RM = Encoding.UTF8.GetString(buffersize, 0, NOBR);
                if (RM[0] == '/')
                {
                    dictonary function = new dictonary();
                    function.dictionayList(brugern, RM);
                }
                string brugerens = "";
                foreach (users bruger in userlist)
                {
                    if (brugern == bruger.brugere)
                    {
                        brugerens = bruger.navne;
                    }
                }
                foreach (users bruger in userlist)
                {
                    byte[] send = Encoding.UTF8.GetBytes(brugerens + ": " + RM + "\n");
                    bruger.brugere.GetStream().Write(send, 0, send.Length);
                    Console.WriteLine(brugerens + ": " + RM + "\n");
                }
            }
        }
        public async void ReceiveMessages(NetworkStream stream)
        {
            bool conn = true;
            while (conn)
            {
                byte[] buffersize = new byte[1000];
                int NOBR = await stream.ReadAsync(buffersize, 0, 1000);
                string RM = Encoding.UTF8.GetString(buffersize, 0, NOBR);
                Console.WriteLine("\n" + RM);
            }
        }
        public async void acceptconn(TcpListener incomming)
        {
            bool conn = true;
            while (conn)
            {
                byte[] bug = new byte[20];
                TcpClient bruger = await incomming.AcceptTcpClientAsync();
                NetworkStream stream = bruger.GetStream();
                string mes = "write your name";
                byte[] mess = Encoding.UTF8.GetBytes(mes);
                stream.Write(mess, 0, mess.Length);
                int nameing = stream.Read(bug, 0, 20);
                string navn = Encoding.UTF8.GetString(bug, 0, nameing);
                userlist.Add(new users(navn, bruger));

                mess = Encoding.UTF8.GetBytes("welcome "+ navn);
                stream.Write(mess, 0, mess.Length);

                echoMmessages(bruger);
            }
        }
        public void serverInfo( int port)
        {
            Console.WriteLine("serverens port er: " + port);
            IPHostEntry serverip = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] adress = serverip.AddressList;
            for(int counter = 1; counter < adress.Length; counter++)
            {
                Console.WriteLine("ip'en er: " + adress[counter].ToString());
            }
        }
        public void send(NetworkStream stream, string text)
        {
            byte[] send = Encoding.UTF8.GetBytes(text);
           stream.Write(send, 0, send.Length);
        }
        public string recivingString(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            int recieving = stream.Read(buffer, 0, 256);
            string GetText = Encoding.UTF8.GetString(buffer, 0,recieving);
            return GetText;
        }
    }
}
