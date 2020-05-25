using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace encoding
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            foreach (byte Integer in bytes)
            {
                Console.WriteLine(Integer);
            }
            string shit = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(shit);
            Console.ReadKey();

        }
    }
}
