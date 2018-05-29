using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace images
{
    class Methods
    {
        public static bool IsCyrillic(string name)
        {
            Regex regexCyrillic = new Regex(@"\p{IsCyrillic}+");  // Cyrillic letters

            foreach (var item in name.ToCharArray())
            {
                if (regexCyrillic.IsMatch(item.ToString()))
                    return true;
                else
                    return false;
            }

            return false;
        }

        public static void WorkWithExtension(ref List<string> extList, ref string[] fileArr)
        {
            List<string> fileNames = new List<string>();

            List<char> chars = new List<char>();
            if (extList.Contains("end"))
                extList.Remove("end");

            bool flag = true;
            while (flag)
            {
            start:
                Console.WriteLine
                (
                    "1 - Показать все символы\n" +
                    "2 - заменить символ\n" +
                    "3 - Очистить экран\n" +
                    "4 - Выйти"
                );

                string choice = Console.ReadLine();
                switch (choice)
                {
                    default:
                        {
                            Console.WriteLine("Такого варианта нет, попробуйте еще раз");
                            goto start;
                        }
                    case "1":
                        {
                            foreach (var item in fileArr)
                            {
                                foreach (char s in item.ToCharArray())
                                    if (!Char.IsLetter(s) && !chars.Contains(s))
                                        chars.Add(s);
                            }
                            Console.WriteLine("Найденные символы : ");
                            foreach (var item in chars)
                                Console.Write("«{0}» ", item);
                            Console.WriteLine();
                        }
                        break;
                    case "2":
                        {
                            Console.WriteLine("Чтобы остановиться введите stop");
                            foreach (var item in chars)
                            {
                                Console.Write("символ «{0}» заменить на : ", item);
                                string ch = Console.ReadLine();
                                if (ch != "stop")
                                    foreach (var name in fileArr)
                                    {
                                        if (!IsCyrillic(name.Substring(0, name.LastIndexOf('.'))))
                                            fileNames.Add(name.Replace(item.ToString(), ch).Replace(" ", ""));
                                        else
                                            fileNames.Add(name.Replace(item.ToString(), ch));
                                    }
                                else
                                    break;
                            }
                            //здесь ошибка хз как исправить, но  старался
                            Console.Write("Введите путь для копирования : ");
                            string newPath = Console.ReadLine();
                            Directory.CreateDirectory(newPath);
                            foreach (var item in fileNames)
                                File.Copy(item, newPath);
                        }
                        break;
                    case "3":
                        {
                            Console.Clear();
                        }
                        break;
                    case "4":
                        {
                            flag = false;
                        }
                        break;
                }
            }
        }



        public static void WorkWithFile(string path)
        {
            string[] fileArr = Directory.GetFileSystemEntries(path);
            Console.WriteLine("Файлов в папке : {0}", fileArr.Count());

            List<string> extensions = new List<string>();
            foreach (var item in fileArr)
                if (extensions.Contains(Path.GetExtension(item)) == false)
                    extensions.Add(Path.GetExtension(item));

            Console.Write("Расширения : ");
            for (int i = 0; i < extensions.Count; i++)
                Console.Write(extensions[i] + " ");
            Console.WriteLine();

            List<string> extList = new List<string>();

            Console.WriteLine("1 - Ввести расширение(стоп-слово: end)\n2 - Работать со всеми расширениями");
            string choice = Console.ReadLine();

            switch (choice)
            {
                default:
                    {
                        Console.WriteLine("Такого варианта нет");
                    }
                    break;
                case "1":
                    {
                        string type;
                        foreach (var item in extList)
                        {
                            type = Console.ReadLine();
                            if (type != "end")
                            {
                                extList.Add(type);
                            }
                            else
                                WorkWithExtension(ref extList, ref fileArr);
                            extList.Remove("end");
                        }
                    }
                    break;
                case "2":
                    {
                        WorkWithExtension(ref extensions, ref fileArr);
                    }
                    break;
            }
        }
    };
}
