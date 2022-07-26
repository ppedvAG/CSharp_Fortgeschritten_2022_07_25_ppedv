namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run); //Genau wie bei Threads (vor .NET 4.0)
		t.Start();

		Task t2 = Task.Factory.StartNew(Run); //Direkt starten (ab .NET 4.0)

		Task t3 = Task.Run(Run); //1:1 der selbe Code wie Factory.StartNew (ab .NET 4.5)

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");

		Console.ReadKey(); //Auf Tasks warten, verhalten sich wie Hintergrundthreads (werden beendet wenn Main Thread zu Ende)
	}

	static void Run()
	{
		for (int i = 0; i < 10000; i++)
			Console.WriteLine($"Task: {i}");
	}
}
