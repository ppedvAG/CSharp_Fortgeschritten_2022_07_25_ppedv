namespace Multithreading;

internal class _05_ThreadMitReturn
{
	static string ReturnString = string.Empty;

	static event Action<object> ReturnValue;

	static void Main(string[] args)
	{
		ParameterizedThreadStart pt = new ParameterizedThreadStart(ToUpper);
		Thread t = new Thread(pt);
		t.Start("abc");

		ReturnValue += (o) => Console.WriteLine(o); //Rückgabewert über Callback
	}

	static void ToUpper(object o)
	{
		if (o is string s)
		{
			ReturnString = s.ToUpper(); //"Returnwert" zuweisen
			ReturnValue(s.ToUpper());
		}
	}
}
