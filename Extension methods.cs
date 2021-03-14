using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_5
{
    class Extension
    {

        
        
        static void Main(string[] args)
        {
            Console.WriteLine("Программа поиска количества символов в строке \n Посчитаем сколько раз встречается буква <И> в тексте : \n <Реализовать метод расширения для поиска количество символов в строке> ");
            
            string s = "Реализовать метод расширения для поиска количество символов в строке";
            char c = 'и';
            int i = s.CharCount(c);
            Console.WriteLine(i);

            Console.Read();
        }
    }

    public static class StringExtension
    {
        public static int CharCount(this string str, char c)
        {
            int counter = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c)
                    counter++;
            }
            return counter;
        }
    }
}