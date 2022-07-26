namespace Multitasking;

internal class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = Task.Run(SumI); //Task.Run nimmt automatisch Generic an
		Console.WriteLine(t.Result); //.Result blockt den Main Thread

		for (int i = 0; i < 100; i++) //Schleife wird verhindert durch .Result
		{
			Console.WriteLine(i);
			Thread.Sleep(50);
		}

		Task t2 = Task.Run(() => Console.WriteLine("x")); //Task mit Anonymer Methode

		Task<int> t3 = Task.Run(() => Enumerable.Range(0, 20000).Sum());

		t.Wait(); //Warte auf diesen Task
		Task.WaitAll(t, t2, t3); //Warte auf alle Tasks
		Task.WaitAny(t, t2, t3); //Warte auf mindestens einen Task
	}

	static int SumI()
	{
		int sum = 0;
		for (int i = 0; i < 1000; i++)
			sum += i;
		return sum;
	}
}
