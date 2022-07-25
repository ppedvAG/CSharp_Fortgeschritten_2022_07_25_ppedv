namespace DelegatesEvents;

internal class Program
{
	public delegate void Vorstellungen(string name); //Definition von Delegate, speichert Referenzen zu Methoden, können zur Laufzeit hinzugefügt oder weggenommen werden

	static void Main(string[] args)
	{
		Vorstellungen vorstellungen; //Variable
		vorstellungen = new Vorstellungen(VorstellungDE); //Delegate erstellen mit new und Standardmethode
		vorstellungen("Max"); //Delegate ausführen mit vorstellungen(Parameter)

		vorstellungen += new Vorstellungen(VorstellungEN); //Methode anhängen (lang)
		vorstellungen += VorstellungEN; //Methode anhängen (kurz), Methoden können mehrmals angehängt werden
		vorstellungen("Max"); //Alle Methoden werden nacheinander ausgeführt

		vorstellungen -= VorstellungDE; //Methode abhängen mit -=
		vorstellungen -= VorstellungDE;
		vorstellungen -= VorstellungDE;
		vorstellungen -= VorstellungDE; //Methode die nicht dranhängt abnehmen bringt keine Fehlermeldung
		vorstellungen("Max");

		vorstellungen -= VorstellungEN;
		vorstellungen -= VorstellungEN; //Delegate ohne Methoden wird null
		vorstellungen("Max"); //Exception

		if (vorstellungen != null)
			vorstellungen("Max"); //Null-Check vor Ausführung

		vorstellungen.Invoke("Max"); //Selber Effekt wie einfache Ausführung mit ()
		vorstellungen?.Invoke("Max"); //? vor . ist ein einfacher Null-Check ("automatischer" Null-Check)

		vorstellungen.GetInvocationList(); //Methoden die anhängen überprüfen
		foreach (Delegate dg in vorstellungen.GetInvocationList())
		{
			Console.WriteLine(dg.Method.Name); //mit dg.Method in Methode reinschauen
		}

		vorstellungen = null; //Delegate entleeren
	}

	public static void VorstellungDE(string name) => Console.WriteLine($"Hallo mein Name ist {name}");

	public static void VorstellungEN(string name) => Console.WriteLine($"Hello my name is {name}");
}