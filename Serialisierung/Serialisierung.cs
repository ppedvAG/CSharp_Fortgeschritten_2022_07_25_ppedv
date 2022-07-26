using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;
using static System.Text.Json.JsonElement;

namespace Serialisierung;

internal class Serialisierung
{
	static void Main(string[] args)
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt"); //Dateipfad generieren

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//NewtonsoftJson();

		//Json();

		//Xml();

		//Binary();

		//CSV();
	}

	static void NewtonsoftJson()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt"); //Dateipfad generieren

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		JsonSerializerSettings settings = new JsonSerializerSettings(); //Einstellungen beim Serialisieren vornehmen
		settings.Formatting = Newtonsoft.Json.Formatting.Indented;

		string json = JsonConvert.SerializeObject(fahrzeuge, settings); //Json String aus Objektliste generieren (WICHTIG: Alle Felder zum Serialisieren müssen Properties sein)
		File.WriteAllText(filePath, json); //Json auf Festplatte schreiben

		string readJson = File.ReadAllText(filePath); //File wieder einlesen
		List<Fahrzeug> readFzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson); //Json String zu Objektliste konvertieren

		JToken jt = JToken.Parse(json); //Jsonelemente "händisch" durchgehen, hier kommt ein Array raus
		foreach (JToken children in jt.Children()) //Children durchgehen, jedes Children ist ein einzelnes Fahrzeug
		{
			Console.WriteLine(children["MaxGeschwindigkeit"].Value<int>()); //mit [] auf bestimmte Werte zugreifen, mit Value<T> zu einem Typ konvertieren

			Fahrzeug f = JsonConvert.DeserializeObject<Fahrzeug>(children.ToString()); //Einzelnes Children zu Fahrzeug konvertieren
			Console.WriteLine(f.MaxGeschwindigkeit);
		}
	}

	static void Json()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt"); //Dateipfad generieren

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		JsonSerializerOptions options = new JsonSerializerOptions(); //Einstellungen beim Serialisieren

		string json = System.Text.Json.JsonSerializer.Serialize(fahrzeuge, options); //Genau wie bei Newtonsoft Json (WICHTIG: Alle Felder zum Serialisieren müssen Properties sein)
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		List<Fahrzeug> readFzg = System.Text.Json.JsonSerializer.Deserialize<List<Fahrzeug>>(readJson); //Genau wie bei Newtonsoft Json

		JsonDocument doc = JsonDocument.Parse(json); //JsonDocument statt JToken
		ArrayEnumerator ae = doc.RootElement.EnumerateArray(); //Childrenliste generieren
		foreach (JsonElement e in ae)
		{
			Console.WriteLine(e.GetProperty("MaxGeschwindigkeit").GetInt32()); //GetProperty statt [], Get<Typ> statt Value<T>

			Fahrzeug f = e.Deserialize<Fahrzeug>(); //hier direkt Deserialisieren statt über ToString()
			Console.WriteLine(f.MaxGeschwindigkeit);
		}
	}

	static void Xml()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt"); //Dateipfad generieren

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		XmlSerializer xmlS = new XmlSerializer(typeof(List<Fahrzeug>)); //Hier keine statischen Zugriffspunkte, Typ(en) zum Serialisieren müssen in der Klammer angegeben werden
		using FileStream fs = new FileStream(filePath, FileMode.Create); //FileStream erstellen mit Create -> Überschreiben
		xmlS.Serialize(fs, fahrzeuge);

		fs.Close();

		using FileStream readStream = new FileStream(filePath, FileMode.Open); //Diesmal Open weil lesen
		List<Fahrzeug> readFzg = xmlS.Deserialize(readStream) as List<Fahrzeug>; //Hier Deserialize ohne Generic, Cast erforderlich

		readStream.Position = 0;

		XmlDocument doc = new XmlDocument(); //Kein Parse hier sondern new
		doc.Load(readStream); //"Parse"

		List<XmlNode> arrayOfFahrzeugNode = doc.ChildNodes[1].OfType<XmlNode>().ToList(); //[1] um Deklarationen oben im Dokument zu überspringen
		foreach (XmlNode node in arrayOfFahrzeugNode) //node = Fahrzeug
		{
			Console.WriteLine(node.ChildNodes.OfType<XmlNode>().First(e => e.Name == "MaxGeschwindigkeit").InnerText); //ChildNodes um Werte zu iterieren, InnerText um Wert zu holen
		}
	}

	static void Binary()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt"); //Dateipfad generieren

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		BinaryFormatter formatter = new BinaryFormatter(); //1:1 wie bei Xml
		using FileStream fs = new FileStream(filePath, FileMode.Create);
		formatter.Serialize(fs, fahrzeuge);

		using FileStream readStream = new FileStream(filePath, FileMode.Open);
		List<Fahrzeug> readFahrzeug = formatter.Deserialize(readStream) as List<Fahrzeug>; //1:1 wie bei Xml
	}

	static void CSV()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt"); //Dateipfad generieren

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		File.WriteAllText(filePath, fahrzeuge.Aggregate("", (agg, fzg) => agg + $"{fzg.ID};{fzg.MaxGeschwindigkeit};{fzg.Marke}\n"));

		TextFieldParser tfp = new TextFieldParser(filePath); //Einfache Klasse um CSVs einzulesen
		tfp.SetDelimiters(";"); //Trennzeichen angeben

		List<string[]> csvLines = new();
		//csvLines.Add(tfp.ReadFields()); //Header vor der Schleife einlesen

		while (!tfp.EndOfData)
		{
			csvLines.Add(tfp.ReadFields()); //Nächste Zeile lesen
		}

		List<Fahrzeug> fzg = new();
		foreach (string[] line in csvLines) //Einzelne CSV Lines zu Fahrzeuge konvertieren
		{
			Fahrzeug f = new();
			f.ID = int.Parse(line[0]);
			f.MaxGeschwindigkeit = int.Parse(line[1]);
			f.Marke = Enum.Parse<FahrzeugMarke>(line[2]);
			fzg.Add(f);
		}
	}
}

[Serializable] //Für BinaryFormatter als Serializable kennzeichnen
public class Fahrzeug
{
	//[JsonIgnore] //Feld beim Serialisieren ignorieren (für Newtonsoft und System Json)
	[JsonProperty("Identifier")] //Feld beim Serialisieren einen anderen Namen geben (Newtonsoft)
	[JsonPropertyName("Identifier")] //Feld beim Serialisieren einen anderen Namen geben (System.Text)
	public int ID { get; set; }

	[field: NonSerialized] //"Binary Ignore"
	public int MaxGeschwindigkeit { get; set; }

	[XmlIgnore]
	[XmlAttribute] //Attribut statt eigenes Feld
	[XmlElement("Marke")] //Eigenen Namen festlegen für Feld
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int id, int v, FahrzeugMarke fm)
	{
		ID = id;
		MaxGeschwindigkeit = v;
		Marke = fm;
	}

	public Fahrzeug() { }
}

public enum FahrzeugMarke { Audi, BMW, VW }