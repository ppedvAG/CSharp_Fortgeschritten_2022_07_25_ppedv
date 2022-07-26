namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		Task<int> t = Task.Run(() => 1);

		t.ContinueWith(task => Console.WriteLine(task.Result * 5)); //Tasks verketten, weitermachen wenn originaler Task fertig
		t.ContinueWith(task => Console.WriteLine(task.Result * 10), TaskContinuationOptions.NotOnFaulted); //Mehrere Tasks als Folgetasks festlegen, nur weiter hier wenn keine Exception
		t.ContinueWith(task => Console.WriteLine(task.Result * 20), TaskContinuationOptions.OnlyOnFaulted); //Fehlertask
		t.ContinueWith(task => Console.WriteLine(task.Result * 40), TaskContinuationOptions.OnlyOnRanToCompletion); //Erfolgstask

		Console.ReadKey();
	}
}
