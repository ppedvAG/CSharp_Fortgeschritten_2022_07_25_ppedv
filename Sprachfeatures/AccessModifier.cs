namespace Sprachfeatures;

class AccessModifier //Klassen/Member ohne Modifier sind internal
{
	public string Name { get; set; } //Kann überall gesehen werden (auch außerhalb vom Projekt)

	private int Groesse { get; set; } //Kann nur in dieser Klasse gesehen werden

	internal string Wohnort { get; set; } //Kann überall aber nur in diesem Projekt gesehen werden


	protected string Lieblingsfarbe { get; set; } //Kann nur in dieser Klasse und in Unterklassen gesehen werden, aber auch außerhalb vom Projekt

	protected internal string Lieblingsnahrung { get; set; } //Kann im ganzen Projekt und in Unterklassen außerhalb gesehen werden

	protected private DateTime Geburtsdatum { get; set; } //Kann nur in dieser Klasse und Unterklassen nur im Projekt gesehen werden
}