using System.Text;

namespace Linq;

internal class Program
{
	static void Main(string[] args)
	{
		#region Einfaches Linq
		//Erstellt eine Liste von Start mit einer bestimmten Anzahl Elementen
		//(5, 20) -> Start bei 5, 20 Elemente -> 5-24
		List<int> ints = Enumerable.Range(0, 20).ToList();

		Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());

		Console.WriteLine(ints.Sum());

		Console.WriteLine(ints.First()); //Erstes Element der Liste, Exception wenn Liste leer
		Console.WriteLine(ints.FirstOrDefault()); //null wenn Liste leer

		Console.WriteLine(ints.Last()); //Erstes Element der Liste, Exception wenn Liste leer
		Console.WriteLine(ints.LastOrDefault()); //null wenn Liste leer

		Console.WriteLine(ints.Single(e => e == 2)); //Einziges Element mit Bedingung, Exception wenn leer oder mehr als ein Element
		Console.WriteLine(ints.SingleOrDefault(e => e == 2)); //Einziges Element mit Bedingung, null wenn leer und Exception wenn mehr als ein Element
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Vergleich Linq Schreibweisen
		//Alle BMWs mit Foreach
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);

		//Standard-Linq: SQL-ähnliche Schreibweise (alt)
		List<Fahrzeug> bmws = (from fzg in fahrzeuge
							   where fzg.Marke == FahrzeugMarke.BMW
							   select fzg).ToList();

		//Methodenkette
		List<Fahrzeug> bmwsNeu = fahrzeuge.Where(fzg => fzg.Marke == FahrzeugMarke.BMW).ToList();
		#endregion

		List<Fahrzeug[]> chunk = fahrzeuge.Chunk(3).ToList(); //Teilt die Liste in Teile auf (hier 3er Teile)

		IEnumerable<Fahrzeug> skipTake = fahrzeuge.Skip(3).Take(5); //Überspringe 3, Nimm 5

		IEnumerable<Fahrzeug> concat = fahrzeuge.Concat(skipTake); //Neue Liste mit 17 Fahrzeugen

		fahrzeuge.Append(new Fahrzeug(100, FahrzeugMarke.VW)); //Fahrzeug hinten anhängen (neue Liste)
		fahrzeuge.Prepend(new Fahrzeug(100, FahrzeugMarke.VW)); //Fahrzeug vorne anhängen (neue Liste)

		IEnumerable<(Fahrzeug First, int Second)> zip = fahrzeuge.Zip(Enumerable.Range(0, 12)); //Zwei Listen als Tupel kombinieren (Fahrzeug, Int)

		IEnumerable<(int First, Fahrzeug Second)> ids = Enumerable.Range(0, 12).Zip(fahrzeuge); //Int, Fahrzeug

		Dictionary<int, Fahrzeug> idsFzg = ids.ToDictionary(ids => ids.First, ids => ids.Second); //Zip zu einem Dictionary konvertieren

		idsFzg.Where(e => e.Key == 3).First();
		idsFzg.First(e => e.Key == 3);
		idsFzg.Single(e => e.Key == 3);

		Console.WriteLine(fahrzeuge.Aggregate("", (agg, fzg) => agg + $"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxGeschwindigkeit} fahren.\n")); //Schöne Ausgabe der Liste

		Console.WriteLine(fahrzeuge.Aggregate(new StringBuilder(), (agg, fzg) => agg.AppendLine($"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxGeschwindigkeit} fahren.\n"));

		Console.WriteLine(fahrzeuge.Aggregate(0, (agg, fzg) => agg + fzg.MaxGeschwindigkeit / 3)); //Auch auf int kann man aggregieren

		int summe = 0;
		foreach (Fahrzeug fzg in fahrzeuge)
			summe += fzg.MaxGeschwindigkeit / 3;
		Console.WriteLine(summe); //Selber Code wie Aggregate

		List<List<Fahrzeug>> selectMany = new();
		List<Fahrzeug> flatten = selectMany.SelectMany(e => e).ToList(); //Liste glätten (aus mehreren Listen eine Liste machen)

		#region Erweiterungsmethoden
		int zahl = 53287957;
		zahl.Quersumme();
		385729.Quersumme();
		Console.WriteLine(35972.Quersumme());

		fahrzeuge.Shuffle(); //Eigene Linq Funktion (Neue Liste)
		fahrzeuge.Shuffle().Where(e => e.MaxGeschwindigkeit > 300); //Methodenkette mit eigener Linq Methode
		#endregion
	}
}

public class Fahrzeug
{
	public int MaxGeschwindigkeit;

	public FahrzeugMarke Marke;

	public Fahrzeug(int v, FahrzeugMarke fm)
	{
		MaxGeschwindigkeit = v;
		Marke = fm;
	}
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}