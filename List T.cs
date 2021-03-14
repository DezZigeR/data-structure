using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace lesson_5
{
    class Program
    {
        static void Main(string[] args)
        {
            long start, end;
            start = DateTime.Now.Ticks;
            GenerateData(15000000);
            end = DateTime.Now.Ticks;
            Console.Write("Time of data generation: {0} sec", new TimeSpan(end - start));
            Console.ReadLine();
            IEnumerable<uint> data;
            start = DateTime.Now.Ticks;
            using (var readFile = File.OpenRead("ip.txt"))
            using (var bufferedStream = new BufferedStream(readFile, 1048576))
            using (var reader = new StreamReader(bufferedStream))
                data = GetIPAddress(reader).ToList();
            end = DateTime.Now.Ticks;
            Console.Write("Data is being read: {0} sec", new TimeSpan(end - start));
            Console.ReadLine();
            start = DateTime.Now.Ticks;
            data.GroupBy(element => element)
                .OrderBy(element => element.LongCount())
                .Reverse()
                .ToList()
                .ForEach(element => Console.WriteLine("Ip: {0}, Count: {1}", new IPAddress(element.Key), element.Count()));
            end = DateTime.Now.Ticks;
            Console.WriteLine("Time data: {0} sec", new TimeSpan(end - start));
            Console.ReadLine();
        }

        public static IEnumerable<uint> GetIPAddress(StreamReader reader)
        {
            const string pattern = "(?<address>(\\d{1,3}\\.{0,1}){4})";
            const string address = "address";
            while (!reader.EndOfStream)
            {
                var data = reader.ReadLine();
                var ipString =
                    Regex.Match(data, pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline)
                         .Groups[address].Value;
                var ipBytes = IPAddress.Parse(ipString).GetAddressBytes();
                yield return BitConverter.ToUInt32(ipBytes, 0);
            }
        }

        public static void GenerateData(int count)
        {
            var random = new Random();
            using (var writeFile = File.OpenWrite("ip.txt"))
            using (var bufferedStream = new BufferedStream(writeFile, 1048576))
            using (var writer = new StreamWriter(bufferedStream))
            {
                for (int i = 0; i < count; i++)
                {
                    if (i < count - 1)
                        writer.WriteLine("10.1.{0}.{1}", random.Next(0, 3), random.Next(1, 254));
                    else
                        writer.Write("10.1.{0}.{1}", random.Next(0, 3), random.Next(1, 254));
                }
            }
        }
    }
}