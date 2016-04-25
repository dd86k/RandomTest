using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RandomTest
{
    static class Program
    {
        const int Iterations = 1000000;

        static Random sr;
        static CryptoRandom csr;
        static PCGRandom spcgr;
        static PCGRandom spcgri;

        static Stopwatch w = new Stopwatch();
        static int[] ra;

        static void Main(string[] args)
        {
            Console.WriteLine($"Iterations: {Iterations}");
            Console.WriteLine($"One item is {Math.Round(1m / Iterations * 100, 6)}%, lower values are better.");
            Console.Write("Making array... ");
            w.Start();
            ra = new int[Iterations];
            w.Stop();
            Console.WriteLine($"{w.ElapsedTicks} Ticks [0x{w.ElapsedTicks:X16}]");
            Console.WriteLine();

            Console.WriteLine("-- Local Random --");
            w.Restart();
            Random r = new Random(); // Seed is system clock
            for (int i = 0; i < Iterations; ++i) { ra[i] = r.Next(Iterations); }
            w.Stop();
            ws();

            Console.WriteLine("-- New Random --");
            ra = new int[Iterations];
            w.Restart();
            for (int i = 0; i < Iterations; ++i) { ra[i] = new Random().Next(Iterations); }
            w.Stop();
            ws();

            Console.WriteLine("-- Static Random --");
            ra = new int[Iterations];
            w.Restart();
            sr = new Random();
            for (int i = 0; i < Iterations; ++i) { ra[i] = sr.Next(Iterations); }
            w.Stop();
            ws();

            Console.WriteLine("-- Local CryptoRandom --");
            ra = new int[Iterations];
            w.Restart();
            CryptoRandom cr = new CryptoRandom();
            for (int i = 0; i < Iterations; ++i) { ra[i] = cr.Next(Iterations); }
            w.Stop();
            ws();

            Console.WriteLine("-- Static CryptoRandom --");
            ra = new int[Iterations];
            w.Restart();
            csr = new CryptoRandom();
            for (int i = 0; i < Iterations; ++i) { ra[i] = csr.Next(Iterations); }
            w.Stop();
            ws();

            Console.WriteLine("-- Local PCGRandom(0, 0) --");
            ra = new int[Iterations];
            w.Restart();
            PCGRandom pcgr = new PCGRandom(0, 0);
            for (int i = 0; i < Iterations; ++i) { ra[i] = pcgr.Next(Iterations); }
            w.Stop();
            ws();

            Console.WriteLine("-- Local PCGRandom(inits) --");
            ra = new int[Iterations];
            w.Restart();
            PCGRandom pcgri = new PCGRandom();
            for (int i = 0; i < Iterations; ++i) { ra[i] = pcgri.Next(Iterations); }
            w.Stop();
            ws();

            Console.WriteLine("-- Static PCGRandom(0, 0) --");
            ra = new int[Iterations];
            w.Restart();
            spcgr = new PCGRandom(0, 0);
            for (int i = 0; i < Iterations; ++i) { ra[i] = spcgr.Next(Iterations); }
            w.Stop();
            ws();

            Console.WriteLine("-- Static PCGRandom(inits) --");
            ra = new int[Iterations];
            w.Restart();
            spcgri = new PCGRandom();
            for (int i = 0; i < Iterations; ++i) { ra[i] = spcgri.Next(Iterations); }
            w.Stop();
            ws();

            Console.ReadLine();
        }

        static void ws()
        {
            Console.Write($"{w.Elapsed} | {w.ElapsedTicks,8} t. | ");
            Console.WriteLine(ra.s());
        }

        // Some stack overflow answer I forgot and modified
        static string s(this int[] l)
        {
            var cnt = new Dictionary<int, int>();
            foreach (int value in l)
            {
                if (cnt.ContainsKey(value))
                {
                    cnt[value]++;
                }
                else
                {
                    cnt.Add(value, 1);
                }
            }
            int c = 0; // Most common
            int t = 0; // Times appearing
            foreach (KeyValuePair<int, int> pair in cnt)
            {
                if (pair.Value > t)
                {
                    c = pair.Key;
                    t = pair.Value;
                }
            }
            
            decimal m = Math.Round((decimal)t / Iterations * 100, 6);
            return $"{c,6} | x{t,6} | {m}%";
        }
    }
}
