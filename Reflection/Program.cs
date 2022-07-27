using System.Reflection;

namespace Reflection;

internal class Program
{
	public string EinString;

	public int EinInt { get; set; }

	static void Main(string[] args)
	{
		//Program p = new();
		//Über p.GetType() Informationen über den Aufbau des Objekts erhalten

		//typeof(Program): Ohne Objekt Informationen erhalten

		object o = Activator.CreateInstance(typeof(Program)); //Objekt erstellen ohne new

		typeof(Program).GetMethod("Test").Invoke(o, null); //Methode indirekt ohne Parameter aufrufen

		o.GetType().GetMethod("Test2").Invoke(o, new[] { "Tests" }); //Methode über Typ vom Objekt ausführen mit Parameter

		o.GetType().GetField("EinString").SetValue(o, "123"); //Feld indirekt ansprechen

		o.GetType().GetProperty("EinInt").SetValue(o, 43); //Property indirekt ansprechen
		o.GetType().GetProperty("EinInt").GetValue(o); //Wert von Property holen


		object o2 = Activator.CreateInstance("Reflection", "Reflection.Program"); //Objekt erstellen ohne Type, nur über strings

		Assembly a = Assembly.GetExecutingAssembly(); //Derzeitiges Assembly (derzeitiges Projekt)
		List<TypeInfo> types = a.DefinedTypes.ToList(); //Alle Typen aus dem Assembly holen

		string path = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2022_07_25\DelegatesEvents\bin\Debug\net6.0\DelegatesEvents.dll";

		Assembly loaded = Assembly.LoadFrom(path); //DLL laden

		Type componentType = loaded.DefinedTypes.First(e => e.Name == "PrimeComponent").AsType(); //Typ von PrimeComponent finden

		object primeComp = Activator.CreateInstance(componentType); //PrimeComponent erstellen

		primeComp.GetType().GetEvent("Prime").AddEventHandler(primeComp, (int i) => Console.WriteLine($"Primzahl: {i}")); //EventHandler anhängen
		primeComp.GetType().GetEvent("NotPrime").AddEventHandler(primeComp, (int i, int t) => Console.WriteLine($"Keine Primzahl: {i}, Teiler: {t}"));
		primeComp.GetType().GetEvent("Prime100").AddEventHandler(primeComp, (int i) => Console.WriteLine($"Hundertste Primzahl: {i}"));

		primeComp.GetType().GetMethod("StartProcess").Invoke(primeComp, null); //StartProcess aufrufen
	}

	public void Test() => Console.WriteLine("Ein Test");

	public void Test2(string test) => Console.WriteLine(test);
}