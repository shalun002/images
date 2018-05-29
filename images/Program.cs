using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace images
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите путь к папке : ");
            string URL = @Console.ReadLine();
            if (Directory.Exists(URL)) 
            {
                Methods.WorkWithFile(URL);
            }
        }
    }
}
