using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Homework1
{
    class Program
    {
        enum Sortings { None, Insertion, Quick };

        //Presets
        const int MIN_RANDOM = 1;
        const int MAX_RANDOM = 10;
        const Sortings SORT_FUNCTION = Sortings.Quick;


        static void Main(string[] args)
        {
            //Test();
            //Environment.Exit(0);
            var input = GetSizeInput(args.ElementAtOrDefault(0));

            if (input == -1)
            {
                RunSequence();
            }
            else
            {
                PrintHeaders();
                RunIteration(input, Sortings.None);
            }
        }

        static void PrintHeaders()
        {
            Console.Write(" Arr. Len., Time (ms),");
            for (int i = MIN_RANDOM; i < MAX_RANDOM + 1; i++)
            {
                Console.Write("{0,10},", i);
            }
            Console.WriteLine();
        }

        static void RunSequence()
        {
            Console.WriteLine("No Sorting, 10-100M numbers:");
            PrintHeaders();
            for (int i = 1; i <= 10; i++)
            {
                RunIteration(i * 10000000, Sortings.None);
            }

            Console.WriteLine("Insertion Sorting, 10k-100k numbers:");
            PrintHeaders();
            for (int i = 1; i <= 10; i++)
            {
                RunIteration(i * 10000, Sortings.Insertion);
            }

            Console.WriteLine("Quick Sorting, 10k-100k numbers:");
            PrintHeaders();
            for (int i = 1; i <= 10; i++)
            {
                RunIteration(i * 10000, Sortings.Quick);
            }
        }

        static void RunIteration(int length, Sortings function)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var randoms = GenerateRandomArray(length, function);
            var hist = GenerateHistogram(randoms);
            stopwatch.Stop();

            Console.Write("{0,10},{1,10},", length, stopwatch.ElapsedMilliseconds);
            for (int i = MIN_RANDOM; i < MAX_RANDOM + 1; i++)
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

        static int[] GenerateRandomArray(int length, Sortings function)
        {
            int[] randoms = new int[length];
            Random factory = new Random();
            for (int i = 0; i < randoms.Length; i++)
            {
                randoms[i] = factory.Next(MIN_RANDOM, MAX_RANDOM + 1);
            }


            switch(function)
            {
                case Sortings.Insertion:
                    {
                        InsertionSort(randoms);
                        break;
                    }
                case Sortings.Quick:
                    {
                        QuickSort(randoms);
                        break;
                    }
            }
            return randoms;
        }

        static Dictionary<int, int> GenerateHistogram(int[] randoms)
        {
            var histogram = new Dictionary<int, int>();
            foreach (int number in randoms)
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
            int[] randoms = GenerateRandomArray(20, SORT_FUNCTION);
            var hist = GenerateHistogram(randoms);

            InsertionSort(randoms);
            foreach (int i in randoms)
            {
                Console.WriteLine(i);
            }
        }

        //Sorting Algorithms
        static int[] InsertionSort(int[] array)
        {
            for (int outer = 0; outer < array.Length - 1; outer++)
            {
                for (int inner = outer + 1; inner > 0; inner--)
                {
                    if (array[inner - 1] > array[inner])
                    {
                        int swap = array[inner - 1];
                        array[inner - 1] = array[inner];
                        array[inner] = swap;
                    }
                }
            }
            return array;
        }

        // QuickSort/3 and Partition/3 were taken from:
        // http://csharpexamples.com/c-quick-sort-algorithm-implementation/
        // I wrote QuickSort/1

        static void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }

        static void QuickSort(int[] arr, int start, int end)
        {
            int i;
            if (start < end)
            {
                i = Partition(arr, start, end);

                QuickSort(arr, start, i - 1);
                QuickSort(arr, i + 1, end);
            }
        }

        static int Partition(int[] arr, int start, int end)
        {
            int temp;
            int p = arr[end];
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                if (arr[j] <= p)
                {
                    i++;
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            temp = arr[i + 1];
            arr[i + 1] = arr[end];
            arr[end] = temp;
            return i + 1;
        }
    }
}
