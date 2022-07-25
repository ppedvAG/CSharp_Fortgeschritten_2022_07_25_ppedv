namespace Generics;

internal class Constraints
{
	int Test = 0;

	public Constraints(int test) => Test = test; //Standardkonstruktor wird entfernt wenn eigener Konstruktor definiert wird

	//spublic Constraints() { }

	static void Main(string[] args)
	{
		Point p = new Point(5, 3);
		Point p2 = p; //Werte werden kopiert
		p2.X = 10; //X bei Point1 wird nicht verändert (da keine Referenz)
		//== und != vergleichen die Werte (p.X == p2.X && p.Y == p2.Y)

		Constraints program = new Constraints();
		Constraints program2 = program; //Referenz zu program wird hergestellt
		program2.Test = 3; //Test bei Program1 wird verändert, da Referenz
		//== und != vergleichen HashCodes (program.GetHashCode() == program2.GetHashCode());

		DataStore4<Constraints> ds4; //Nicht möglich ohne Standardkonstruktor
		DataStore5<DayOfWeek> ds5; //Valide
		DataStore5<DayOfWeek.Friday> ds52; //Nicht valide
	}

	public class DataStore1<T> where T : class { } //Referenztyp erzwingen (class)

	public class DataStore2<T> where T : struct { } //Wertetyp erzwingen (struct)

	public class DataStore3<T> where T : Constraints { } //Vererbungshierarchie (Constraints und Unterklassen)

	public class DataStore4<T> where T : new() { } //Nur Typen die einen Default Konstruktor

	public class DataStore5<T> where T : Enum { } //Nur Enums (hier keine Enumwerte)

	public class DataStore6<T> where T : Delegate { } //Nur Delegatetypen (Action, Predicate, Func, eigenes Delegate)

	public class DataStore7<T> where T : unmanaged { } //Bestimmte Typen (z.B.: int, bool, double, ...)
	//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/unmanaged-types

	public class DataStore8<T>
		where T : class, new() //Mehrere Constraints (Ein Referenztyp mit Default Konstruktor)
	{ }

	public class DataStore9<T1, T2> //Mehrere Constraints auf mehrere Generics
		where T1 : class, new()
		where T2 : struct
	{ }

	public class DataStore<T1, T2, T3, T4, T5, T6, T7> { } //Beliebig viele Generics möglich

	public struct Point
	{
		public int X;
		public int Y;

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
