namespace Sprachfeatures;

internal class Sprachfeatures
{
	static void Main(string[] args)
	{
		object o = new object();
		if (o is int)
		{
			int x = (int) o; //Lange Form ohne direkten Cast
		}

		if (o is int x2) //automatischer Cast (Kurzform)
		{

		}

		string text = "123";
		if (int.TryParse(text, out int ergebnis))
		{

		}

		BennanteArgumente(z: 5, x: 7);

		int y = 0;
		Test(out y);


		string str = null; //Ohne Null-Coalescing Operator
		if (str != null)
		{
			str = text;
		}
		else
		{
			throw new Exception();
		}

		string str1 = text != null ? text : throw new Exception(); //Kürzung mit ? Operator

		string str2 = text ?? throw new Exception(); //Null-Coalescing Operator

		List<string> list = new();
		Dictionary<string, int> dict = new();

		List<int> ints = new List<int>(); //new Expression can be simplified

		Person p = new Person(4, "Test", null);
		(int id, string name, Person v) = p; //Deconstruct Methode wird auch generiert

		Console.WriteLine(); //Global Using

		JsonSerializer.Serialize(p); //Eigenes Global Using in Usings.cs

		Person vorg = p switch
		{
			{ Vorgesetzter: { Vorgesetzter: { Vorgesetzter: null } } } => null, //Alten Pattern mit vielen { }
			{ Vorgesetzter.Vorgesetzter.Vorgesetzter.Vorgesetzter: null } => null //Neue Pattern mit .
		};

		string str3 = $"Das ist ein Hochkomma {{ (\")";
	}

	public static void BennanteArgumente(int x = 0, int y = 0, int z = 0) { }

	public static void Test(out int x) //out: nur Zuweisungen, ref: auch Wertänderungen
	{
		//x++; //nicht möglich mit out aber mit ref
		x = 0;
	}

	public static string SchereSteinPapier(string s1, string s2)
	{
		return (s1, s2) switch
		{
			("stein", "schere") => "Stein gewinnt",
			_ => "Unentschieden",
		};
	}
}

public interface ITest
{
	static int Wochenstunden => 40;

	void Test();

	public void Code()
	{
		//Bad practice
	}
}