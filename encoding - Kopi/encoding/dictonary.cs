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
    class dictonary
    {
        public dictonary()
        {

        }
        /// <summary>
        /// denne funtktion tager imod en command som den får sendt igennem hvis en skriver det til den. Denne her compare den så med alle ekisterende commands
        /// </summary>
        /// <param name="command"></param>
        public async void dictionayList( TcpClient requestingUser,string command)
        {
            NetworkStream requester = requestingUser.GetStream();
            Smethods calling = new Smethods();
            if (command == "/?")
            {
                string coices = DLCH();
            }
            else if (command == "/wisper")
            {
                string text = "hvem vil du gerne skrive til?";
                calling.send(requester, text);
                string choiceOfUser = Console.ReadLine();
                foreach(users brugernavn in calling.userlist)
                {
                    if (choiceOfUser == brugernavn.navne)
                    {
                        text = "Hvad vil du gerne sende som besked?";
                        calling.send(requester, text);
                        text = Console.ReadLine();

                        NetworkStream wisperbruger = brugernavn.brugere.GetStream();
                        calling.send(wisperbruger, text);
                    }
                }
            }
            else if (command == "/list")
            {
              
                foreach(users brugernavn in calling.userlist)
                {
                    byte[] liste = Encoding.UTF8.GetBytes(brugernavn.navne +"\n");
                    requestingUser.GetStream().Write(liste, 0, liste.Length);
                }
            }
        }
        /// <summary>
        /// Denne er som holder for text i forhold til, hva der er muligt
        /// </summary>
        string DLCH()
        {
            string DLCH = 
            "/?      = Fuld liste over funktioner\n" +
            "/wisper = Skriv til en specifik person\n" +
            "/list   = En liste over alle der er tilkoblet til servern\n";
            return DLCH;
        }
    }
}
