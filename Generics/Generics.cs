namespace Generics;

internal class Generics
{
	static void Main(string[] args)
	{
		List<string> list = new List<string>(); //Generic: T wird nach unten übernommen (hier T = string);
		list.Add("123"); //T wird durch string ersetzt: Add(T) -> Add(string)

		Dictionary<string, int> dictionary = new Dictionary<string, int>(); //Klasse mit 2 Generics: TKey (string), TValue (int)
		dictionary.Add("Max", 1); //string als Key, int als Value: Add(TKey, TValue) -> Add(string, int)
	}
}

public class DataStore<T> :
	IProgress<T>, //T hier übergeben
	IEquatable<int> //int hier übergeben (fixer Typ)
{
	private T[] data = new T[10]; //Array vom Typ T

	public List<T> Data => data.ToList(); //T nach unten in ein weiteres Generic übergeben

	public void Add(int index, T item) => data[index] = item; //Generic in Methodenparameter

	public T GetIndex(int index) //T als return Wert
	{
		if (index < 0 || index >= data.Length)
			return default(T); //default(T): Standardwert von T (int: 0, string: null, bool: false, ...)
		return data[index];
	}

	public void PrintType<MyType>() //Generic in Methode
	{
		Console.WriteLine(typeof(MyType)); //Typ von T holen
		Console.WriteLine(nameof(MyType)); //MyType als string: "int", "bool", "Program", ...
		Console.WriteLine(typeof(MyType).Name); //Selbiges wie nameof
		Console.WriteLine(default(MyType));
	}

	public void Report(T value) //T als Parameter durch Vererbung
	{
		throw new NotImplementedException();
	}

	public bool Equals(int other) //Hier int statt T
	{
		throw new NotImplementedException();
	}
}

public class DataStore2<T> : DataStore<T> { } //Klassen mit T vererben: braucht wieder T beim Klassennamen