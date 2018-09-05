using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Homework1
{
    class Program
    {
        const int MIN_RANDOM = 1;
        const int MAX_RANDOM = 10;

        static void Main(string[] args)
        {
            //TestGenerateRandomArray();
            //TestGenerateHistogram();

            Stopwatch stopwatch = new Stopwatch();
            var input = GetSizeInput(args[1]);

            stopwatch.Start();
            var randoms = GenerateRandomArray(input);
            var hist = GenerateHistogram(randoms);
            stopwatch.Stop();

            Console.WriteLine("Time Elapsed: {}", stopwatch.ElapsedMilliseconds);



        }

        static int GetSizeInput(string raw)
        {
            Console.WriteLine("Array length:");
            if (int.TryParse(raw, out int parsed))
            {
                return parsed;
            }
            else
            {
                return -1;
            }
        }

        static int[] GenerateRandomArray(int length)
        {
            int[] randoms = new int[length];
            Random factory = new Random();
            for (int i = 0; i < randoms.Length;i++)
            {
                randoms[i] = factory.Next(MIN_RANDOM, MAX_RANDOM);
            }
            return randoms;
        }

        static int[] GenerateHistogram(int[] randoms)
        {
            var histogram = new Dictionary<int, int>();
            foreach(int number in randoms)
            {
                if (histogram.ContainsKey(number))
                {
                    histogram[number]++;
                } 
                else 
                {
                    histogram.Add(number, 1);
                }
            }

            var kv = histogram.ToList();
            kv.OrderBy((kvp) => kvp.Key);
            return kv.Select((arg) => arg.Value).ToArray();
        }

        static void TestGenerateRandomArray()
        {
            Console.WriteLine("Test GenerateRandomArray(10):");
            int[] randoms = GenerateRandomArray(10);
            Console.WriteLine(string.Join(",", randoms.Select(x => x.ToString()).ToArray()));

        }

        static void TestGenerateHistogram()
        {
            Console.WriteLine("Test GenerateHistogram(random of size 100):");
            int[] randoms = GenerateRandomArray(100);
            int[] hist = GenerateHistogram(randoms);
            Console.WriteLine(string.Join(",", hist.Select(x => x.ToString()).ToArray()));
            Console.WriteLine(hist.Sum());
        }
    }
}
