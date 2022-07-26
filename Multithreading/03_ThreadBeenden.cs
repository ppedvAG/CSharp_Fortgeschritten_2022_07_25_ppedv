﻿namespace Multithreading;

internal class _03_ThreadBeenden
{
	static void Main(string[] args)
	{
		try
		{
			Thread t = new Thread(Run);
			t.Start();

			Thread.Sleep(3000);

			t.Interrupt(); //Beende den Thread, wirft ThreadInterruptedException
			//t.Abort(); //deprecated
		}
		catch (ThreadInterruptedException)
		{
			//Funktioniert hier nicht
		}
	}

	static void Run()
	{
		try
		{
			for (int i = 0; i < 100; i++)
			{
				Console.WriteLine(i);
				Thread.Sleep(100); //10 Sekunden insgesamt
			}
		}
		catch (ThreadInterruptedException)
		{
			Console.WriteLine("Thread unterbrochen");
		}
	}
}
