namespace Multithreading;

internal class _04_ThreadCancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource(); //Source erstellen
		CancellationToken token = cts.Token; //Token aus Source entnehmen

		ParameterizedThreadStart pt = new ParameterizedThreadStart(Run);
		Thread t = new Thread(pt);
		t.Start(token); //Token als Parameter übergeben

		Thread.Sleep(2000);
		cts.Cancel(); //Auf der Source die Cancellation anfordern
	}

	static void Run(object o)
	{
		try
		{
			if (o is CancellationToken ct)
			{
				for (int i = 0; i < 100; i++)
				{
					if (ct.IsCancellationRequested)
						ct.ThrowIfCancellationRequested();

					Console.WriteLine(i);
					Thread.Sleep(100);
				}
			}
		}
		catch (OperationCanceledException) //Wieder hier unten
		{
			Console.WriteLine("Cancellation angefordert");
		}
	}
}
