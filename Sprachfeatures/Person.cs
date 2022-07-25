using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sprachfeatures;

public record Person
(
	[field: JsonPropertyName("")] int ID, //Feld hier braucht field:
	 string Name,
	 Person Vorgesetzter
)
{
	[JsonPropertyName("id")] //hier kein field: notwendig
	public int MyProperty { get; set; }

	public void Test()
	{
		//Code schreiben wie in normaler Klasse
	}
}