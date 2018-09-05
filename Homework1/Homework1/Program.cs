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
            //Test();

            var input = GetSizeInput(args.ElementAtOrDefault(0));
            PrintHeaders();

            if(input == -1)
            {
                for (int i = 1; i <= 20; i++)
                {
                    RunIteration(i * 1000000);
                }
            } 
            else
            {
                RunIteration(input);
            }
        }

        static void PrintHeaders()
        {
            Console.Write(" Arr. Len., Time (ms),");
            for (int i = MIN_RANDOM; i < MAX_RANDOM+1; i++)
            {
                Console.Write("{0,10},", i);
            }
            Console.WriteLine();
        }

        static void RunIteration(int length)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var randoms = GenerateRandomArray(length);
            var hist = GenerateHistogram(randoms);
            stopwatch.Stop();

            Console.Write("{0,10},{1,10},", length, stopwatch.ElapsedMilliseconds);
            for (int i = MIN_RANDOM; i < MAX_RANDOM+1; i++)
            {
                Console.Write("{0,10},", hist.GetValueOrDefault(i, 0));
            }

            Console.WriteLine();
        }

        static int GetSizeInput(string raw)
        {
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
                randoms[i] = factory.Next(MIN_RANDOM, MAX_RANDOM+1);
            }
            return randoms;
        }

        static Dictionary<int, int> GenerateHistogram(int[] randoms)
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
            return histogram;
        }

        static void Test()
        {
            Console.WriteLine("Running Test:");
            int[] randoms = GenerateRandomArray(100);
            var hist = GenerateHistogram(randoms);
        }
    }
}
