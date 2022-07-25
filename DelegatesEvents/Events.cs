namespace DelegatesEvents;

public class Events
{
	static event EventHandler TestEvent;

	static event EventHandler<TestEventArgs> ArgsEvent;

	static void Main(string[] args)
	{
		TestEvent += Events_TestEvent; //Extra Methode anhängen ohne new
		TestEvent += (sender, e) => Console.WriteLine("Test"); //Anonymes Event mit Sender und EventArgs
		TestEvent(null, EventArgs.Empty);
		TestEvent?.Invoke(null, EventArgs.Empty); //Wie bei delegate

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent(null, new TestEventArgs() { Status = "Ein Status" });

		TestEvent(null, new TestEventArgs()); //TestEventArgs übergeben, da Vererbungshierarchie
	}

	private static void Events_TestEvent(object? sender, EventArgs e) => Console.WriteLine("Test");

	private static void Events_ArgsEvent(object? sender, TestEventArgs e) //TestEventArgs, da im Event (oben) definiert
	{
		Console.WriteLine(e.Status);
		e.Test(); //In EventArgs reingreifen
	}
}

public class TestEventArgs : EventArgs
{
	public string Status { get; set; }

	public void Test() => Console.WriteLine(Status);
}