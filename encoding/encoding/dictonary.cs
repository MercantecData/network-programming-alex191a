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
using System.Security.Cryptography;
using System.Runtime.InteropServices;

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
        public void dictionayList( TcpClient requestingUser,string command)
        {
            int guess = 0;
            NetworkStream requester = requestingUser.GetStream();
            Smethods calling = new Smethods();
            if (command == "/?")
            {
                string coices = DLCH();
                calling.send(requester, coices);
            }
            else if (command == "/wisper")
            {
                string text = "hvem vil du gerne skrive til?";
                calling.send(requester, text);
                string choiceOfUser = calling.recivingString(requester);
                foreach (users brugernavn in Smethods.userlist)
                {
                    if (choiceOfUser == brugernavn.navne)
                    {
                        text = "Hvad vil du gerne sende som besked?";
                        calling.send(requester, text);
                        text = calling.recivingString(requester);

                        NetworkStream wisperbruger = brugernavn.brugere.GetStream();
                        calling.send(wisperbruger, text);
                    }
                }
            }
            else if (command == "/list")
            {

                foreach (users brugernavn in Smethods.userlist)
                {
                    calling.send(requester, brugernavn.navne + "\n");
                }
            }
            else if (command == "/game")
            {
                int RandomNumber(int min, int max)
                {
                    Random random = new Random();
                    return random.Next(min, max);
                }
                bool conn = true;
                while (conn) {
                    if (guess == 0)
                    {
                        guess = RandomNumber(0, 100);
                    }
                    string Text = "gæt på et nummer \n";
                    calling.send(requester, Text);
                    string theGuess = calling.recivingString(requester);
                    Console.WriteLine(guess);
                    if (int.TryParse(theGuess, out int value))
                    {
                        if (Convert.ToInt32(theGuess)== guess)
                        {
                            Text = "du gættede rigtigt \n";
                            calling.send(requester, Text);
                            conn = false;
                            guess = 0;
                        }
                        else if (Convert.ToInt32(theGuess) > guess)
                        {
                            Text = "dit gæt var for højt \n";
                            calling.send(requester, Text);
                        }
                        else if (Convert.ToInt32(theGuess)< guess)
                        {
                            Text = "Dit gæt var for lavt \n";
                            calling.send(requester, Text);
                        }
                    }
                    else if (theGuess == "stop")
                    {
                        conn = false;
                    }
                    else 
                    { 
                        Text = "skriv et tal din spade \n" +
                                "eller skriv 'stop' for at lukke";
                        calling.send(requester, Text);
                    }
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
            "/list   = En liste over alle der er tilkoblet til servern\n" +
            "/game   = starter et talgættespil\n";
            return DLCH;
        }
    }
}
