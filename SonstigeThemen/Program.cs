using System.Collections;

namespace SonstigeThemen;

internal class Program
{
	static void Main(string[] args)
	{
		Wagon w1 = new();
		Wagon w2 = new();
		if (w1 == w2) //AnzSitze und Farbe vergleichen, ohne Überladung werden Speicheradressen verglichen
		{
			//...
		}

		Zug z = new();
		z++;
		z++;
		z++;
		z++;
		z++;

		Zug z1 = new();
		z1++;
		z1++;
		z1++;

		z += z1;

		z += w1;

		foreach (Wagon w in z)
		{

		}

		z[3] = w2;
		Console.WriteLine(z["Rot"]);
		z["Rot"] = w2;
		Console.WriteLine(z[0, "Rot"]);

		var x = new { ID = 0, Wert = "Test", WF = false };
		Console.WriteLine(x.WF);

		var x2 = z.Select(e => new { AnzS = e.AnzSitze, HC = e.GetHashCode() });
		Console.WriteLine(x2.First().HC);
	}
}

public class Zug : IEnumerable, IEnumerable<Wagon>
{
	private List<Wagon> Wagons = new();

	private Dictionary<int, Wagon> WagonIDs;

	public Wagon this[int index]
	{
		get => Wagons[index];
		set => Wagons[index] = value;
	}

	public Wagon this[string farbe]
	{
		get => Wagons.First(e => e.Farbe == farbe);
	}

	public Wagon this[int index, string farbe]
	{
		get => null;// Wagons[index].Farbe == farbe;
	}

	public static Zug operator ++(Zug z1)
	{
		z1.Wagons.Add(new Wagon());
		return z1;
	}

	public static Zug operator +(Zug z1, Zug z2)
	{
		z1.Wagons.AddRange(z2.Wagons);
		return z1;
	}

	public static Zug operator +(Zug z1, Wagon w)
	{
		z1.Wagons.Add(w);
		return z1;
	}

	public IEnumerator GetEnumerator()
	{
		return Wagons.GetEnumerator();
	}

	IEnumerator<Wagon> IEnumerable<Wagon>.GetEnumerator()
	{
		return WagonIDs.Values.GetEnumerator();
	}
}

public class Wagon
{
	public int AnzSitze;

	public string Farbe;

	public static bool operator ==(Wagon w1, Wagon w2)
	{
		return w1.AnzSitze == w2.AnzSitze && w1.Farbe == w2.Farbe;
	}

	public static bool operator !=(Wagon w1, Wagon w2)
	{
		return !(w1 == w2);
	}
}