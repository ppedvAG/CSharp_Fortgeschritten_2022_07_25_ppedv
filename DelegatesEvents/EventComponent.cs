namespace DelegatesEvents;

public class EventComponent
{
	static void Main(string[] args)
	{
		Component comp = new();
		comp.ProcessCompleted += () => Console.WriteLine("Fertig"); //Parameterlose Action mit () =>
		comp.ValueChanged += (counter) => Console.WriteLine($"Zähler: {counter}"); //Ein Parameter kommt zu uns über Action
		comp.StartProcess();
	}
}

public class Component
{
	public event Action ProcessCompleted; //Action statt EventHandler

	public event Action<int> ValueChanged; //Action mit einem Parameter

	public void StartProcess()
	{
		for (int i = 0; i < 10; i++)
		{
			//Längerer Prozess
			ValueChanged?.Invoke(i);
			Thread.Sleep(200);
		}
		ProcessCompleted?.Invoke();
	}
}