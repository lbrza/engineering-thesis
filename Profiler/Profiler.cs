using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace profiler
{
    public partial class Program
    {
        public const int POS = 32;
        public const int CYCLES = 1;
        public static int[] msgLength = new int[POS];
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Profiler - Lightweight Hash Function in C#. UADE - LMB.");
            TestVector1();
        }

        public static string GetARandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return length == 0 ? "" : new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string[] GetAllRandomStrings()
        {
            string[] rndStr = new string[POS];

            int i = 0;
            for (int j = 0; j < 10; j++)
            {
                msgLength[i] = j;
                rndStr[i] = GetARandomString(j);
                ++i;
            }

            for (int j = 10; j < 101; j = j + 10)
            {
                msgLength[i] = j;
                rndStr[i] = GetARandomString(j);
                ++i;
            }

            for (int j = 200; j < 1001; j += 100)
            {
                msgLength[i] = j;
                rndStr[i] = GetARandomString(j);
                ++i;
            }

            for (int j = 10000; j < 1000001; j = j * 10)
            {
                msgLength[i] = j;
                rndStr[i] = GetARandomString(j);
                ++i;
            }
            return rndStr;
        }
        public static void TestVector1()
        {
            string[] rndStrs = GetAllRandomStrings();
            double[] totalTime = new double[POS];
            double[] oneTime = new double[POS];
            double[] avgTime = new double[POS];
            double[] hashRate = new double[POS];

            byte[] photonDigest = new byte[photoncs.Constants.DIGESTSIZE / 8];
            byte[] quarkDigest = new byte[quarkcs.Constants.MAXDIGEST];
            byte[] spongentDigest = new byte[32];

            for (int i = 0; i < POS; ++i)
            {
                Console.WriteLine("Computing Hash Function {0} times for position  {1}: ", CYCLES, i);

                var startTime = DateTime.UtcNow;
                for (int j = 0; j < CYCLES + 1; ++j)
                {
                    photoncs.Program.hash(ref photonDigest, rndStrs[i], rndStrs[i].Length * 8);
                    //quarkcs.Program.Quark(ref quarkDigest, rndStrs[i], (ulong)rndStrs[i].Length);
                    //spongentcs.Program.spongent(rndStrs[i], spongentDigest);
                }
                var endTime = DateTime.UtcNow;
                TimeSpan interval = endTime - startTime;
                totalTime[i] = interval.TotalSeconds;
                avgTime[i] = (interval.TotalSeconds / CYCLES);
                hashRate[i] = (CYCLES / interval.TotalSeconds);

                startTime = DateTime.UtcNow;

                photoncs.Program.hash(ref photonDigest, rndStrs[i], rndStrs[i].Length * 8);
                //quarkcs.Program.Quark(ref quarkDigest, rndStrs[i], (ulong)rndStrs[i].Length);
                //spongentcs.Program.spongent(rndStrs[i], output);

                endTime = DateTime.UtcNow;
                interval = endTime - startTime;
                oneTime[i] = interval.TotalSeconds;

            }

            Console.WriteLine("N.; MSGLGTH; ONETIME; TTLTME; AVGTIME; HASHRATE");
            for (int i = 0; i < POS; i++)
                Console.WriteLine("{0}; {1}; {2}; {3}; {4}; {5};", i, msgLength[i], oneTime[i], totalTime[i], avgTime[i], hashRate[i]);

            Console.ReadLine();
        }
    }
}
