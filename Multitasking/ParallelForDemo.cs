using System.Diagnostics;

namespace Multitasking;

internal class ParallelForDemo
{
	static void Main(string[] args)
	{
		int[] durchgänge = { 1000, 10000, 50000, 100000, 250000, 500000, 1000000, 5000000, 10000000, 100000000 };
		foreach (int i in durchgänge)
		{
			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(i);
			sw.Stop();
			Console.WriteLine($"For Durchgänge {i}: {sw.ElapsedMilliseconds}");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(i);
			sw2.Stop();
			Console.WriteLine($"Parallel For Durchgänge {i}: {sw2.ElapsedMilliseconds}");
		}

		/*
		    For Durchgänge 1000: 0
			Parallel For Durchgänge 1000: 73
			For Durchgänge 10000: 1
			Parallel For Durchgänge 10000: 9
			For Durchgänge 50000: 10
			Parallel For Durchgänge 50000: 9
			For Durchgänge 100000: 19
			Parallel For Durchgänge 100000: 18
			For Durchgänge 250000: 73
			Parallel For Durchgänge 250000: 35
			For Durchgänge 500000: 119
			Parallel For Durchgänge 500000: 95
			For Durchgänge 1000000: 164
			Parallel For Durchgänge 1000000: 288
			For Durchgänge 5000000: 1613
			Parallel For Durchgänge 5000000: 293
			For Durchgänge 10000000: 1304
			Parallel For Durchgänge 10000000: 1561
			For Durchgänge 100000000: 12123
			Parallel For Durchgänge 100000000: 6216
		*/
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
		{
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
		}
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		Parallel.For(0, iterations, i => //int i = 0; i < iterations; i++
		{
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
		});
	}
}
