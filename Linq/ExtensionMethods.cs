namespace Linq;

internal static class ExtensionMethods //Klasse muss Statisch sein
{
	public static int Quersumme(this int x) //Auf Int basiert
	{
		return x.ToString().ToCharArray().Sum(e => (int) char.GetNumericValue(e));
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
	{
		return list.OrderBy(e => Random.Shared.Next());
	}
}
