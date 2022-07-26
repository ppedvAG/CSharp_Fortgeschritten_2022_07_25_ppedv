using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		//Stopwatch sw = Stopwatch.StartNew(); //Sequentiell, ineffizient
		//Toast();
		//GeschirrHerrichten();
		//KaffeeZubereiten();
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//Stopwatch sw = Stopwatch.StartNew(); //Sequentiell, ineffizient
		//ToastAsync(); //Task erzeugen
		//GeschirrHerrichtenAsync(); //Task erzeugen
		//KaffeeZubereitenAsync(); //Task erzeugen
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7ms, Main Thread einfach weitergelaufen
		//Console.ReadKey(); //Durch Console.ReadKey() auf Tasks warten, 4 Sekunden

		Stopwatch sw = Stopwatch.StartNew(); //Sequentiell, ineffizient
		Task<Toast> toast = ToastAsyncAwait(); //Task erzeugen
		Task<Tasse> tasse = GeschirrHerrichtenAsyncAwait(); //Task erzeugen
		Tasse t = await tasse; //await Tasse statt tasse.Result -> kein Main Thread Block
		Task<Kaffee> kaffee = KaffeeZubereitenAsyncAwait(t); //Task erzeugen
		Toast to = await toast; //Warte auf den Toast
		Kaffee k = await kaffee; //Warte auf den Kaffee
		sw.Stop();
		Console.WriteLine(sw.ElapsedMilliseconds);
	}

	static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	static void GeschirrHerrichten()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Geschirr hergerrichtet");
	}

	static void KaffeeZubereiten()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee zubereitet");
	}

	static async void ToastAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
	}

	static async void GeschirrHerrichtenAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr hergerrichtet");
	}

	static async void KaffeeZubereitenAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee zubereitet");
	}

	static async Task<Toast> ToastAsyncAwait() //Task als Rückgabewert
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> GeschirrHerrichtenAsyncAwait()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr hergerrichtet");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeZubereitenAsyncAwait(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee zubereitet");
		return new Kaffee();
	}
}

class Toast { }

class Tasse { }

class Kaffee { }