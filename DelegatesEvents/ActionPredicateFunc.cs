namespace DelegatesEvents;

internal class ActionPredicateFunc
{
	static void Main(string[] args)
	{
		Action<int, int> action = Addiere; //Action: Methode mit void und bis zu 16 Parametern
		action += Subtrahiere; //Methode anhängen wie bei Delegate
		action(4, 6);
		action?.Invoke(5, 6); //Ausführen mit Null-Check

		DoSomething(4, 2, Addiere); //Verhalten von Methode anpassen
		DoSomething(4, 2, Subtrahiere); //Unterschiedliche Actions als Parameter

		Predicate<int> predicate = CheckForZero; //Predicate: Methode mit bool als Rückgabewert und genau einem Parameter
		predicate += CheckForOne;
		bool b = predicate(4); //Zuweisung auf bool Variable (da Rückgabewert bool)
		bool? nullBool = predicate?.Invoke(3); //Muss auf Nullable Boolean zugewiesen werden, da ?.Invoke null sein kann

		DoSomething(3, CheckForZero);
		DoSomething(3, CheckForOne);

		Func<int, int, double> func = Multipliziere; //Func: Methode mit Rückgabewert, bis zu 16 Parameter, Rückgabetyp MUSS letztes Generic sein
		func += Dividiere;
		double d = func(3, 6);
		double? nullDouble = func?.Invoke(5, 6); //Zuweisung möglich wie bei Predicate

		DoSomething(3, 6, Multipliziere);
		DoSomething(3, 6, Dividiere);

		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		func += (int x, int y) => { return x + y; }; //Kürzere Form

		func += (x, y) => { return x - y; };

		func += (x, y) => (double) x / y;

		DoSomething(3, 4, (z1, z2) => Console.WriteLine(z1 + z2)); //Anonyme Action
		DoSomething(3, z => z != 0); //Predicate, bei einem Parameter keine Klammern
		DoSomething(3, 6, (z1, z2) => z1 + z2); //Func Methode implizieren durch Rückgabewert (+ gibt int zurück, WriteLine gibt void zurück)
	}

	#region Action
	private static void Addiere(int arg1, int arg2) => Console.WriteLine(arg1 + arg2);

	private static void Subtrahiere(int arg1, int arg2) => Console.WriteLine(arg1 - arg2);

	private static void DoSomething(int z1, int z2, Action<int, int> action) => action(z1, z2);
	#endregion

	#region Predicate
	private static bool CheckForZero(int obj) => obj == 0;

	private static bool CheckForOne(int obj) => obj == 1;

	private static bool DoSomething(int z, Predicate<int> predicate) => predicate(z);
	#endregion

	#region Func
	private static double Multipliziere(int arg1, int arg2) => arg1 * arg2;

	private static double Dividiere(int arg1, int arg2) => (double) arg1 / arg2;

	private static double DoSomething(int z1, int z2, Func<int, int, double> func) => func(z1, z2);
	#endregion
}