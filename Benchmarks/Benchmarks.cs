using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Text;

namespace Benchmarks;

[MemoryDiagnoser(false)]
[RankColumn]
//[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Benchmarks
{
	public List<Fahrzeug> Fahrzeuge;

	[Params(1000, 5000, 10000)]
	public int Anzahl;

	[GlobalSetup]
	public void Setup()
	{
		Fahrzeuge = new();
		Random rnd = new Random();
		for (int i = 0; i < Anzahl; i++)
		{
			Fahrzeug f = new(i, rnd.Next(100, 500), (FahrzeugMarke) rnd.Next(0, 3));
			Fahrzeuge.Add(f);
		}
	}

	[Benchmark]
	[IterationCount(50)]
	public void ForEach()
	{
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in Fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);
	}

	[Benchmark]
	[IterationCount(50)]
	public void LinqSQL()
	{
		List<Fahrzeug> bmws = (from fzg in Fahrzeuge
							   where fzg.Marke == FahrzeugMarke.BMW
							   select fzg).ToList();
	}

	[Benchmark]
	[IterationCount(50)]
	public void Linq()
	{
		List<Fahrzeug> bmwsNeu = Fahrzeuge.Where(fzg => fzg.Marke == FahrzeugMarke.BMW).ToList();
	}

	//[Benchmark]
	[IterationCount(50)]
	public void StringPlus()
	{
		string s = "";
		for (int i = 0; i < 10000; i++)
			s += i;
	}

	//[Benchmark]
	[IterationCount(50)]
	public void StringBuilderTest()
	{
		StringBuilder sb = new();
		for (int i = 0; i < 100000; i++)
			sb.Append(i);
		sb.ToString();
	}
}

public class Fahrzeug
{
	public int ID;
	public int MaxGeschwindigkeit;
	public FahrzeugMarke Marke;
	public List<Sitzplatz> Sitze;

	public Fahrzeug(int id, int v, FahrzeugMarke fm)
	{
		ID = id;
		MaxGeschwindigkeit = v;
		Marke = fm;
		Sitze = new();

		//Anzahl Sitzplätze anhand der Geschwindigkeit (6: max 150km/h, 5 bis 250km/h, 4 ab 250km/h)
		int sitze = v <= 150 ? 6 : v <= 250 ? 5 : 4;

		//Sitzplätze erstellen
		for (int i = 0; i < sitze; i++)
			Sitze.Add(new Sitzplatz());

		//Sitzplätze semi-zufällig belegen damit die Übung zwischen Teilnehmern gleiche Ergebnisse liefert
		//Geschwindigkeit modulo Anzahl Sitzplätze besetzen
		for (int i = 0; i < v % (sitze + 1); i++)
			Sitze[i].IstBesetzt = true;
	}
}

public record Sitzplatz
{
	public bool IstBesetzt;
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}