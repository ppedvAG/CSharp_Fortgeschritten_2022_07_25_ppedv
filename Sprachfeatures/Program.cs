namespace Sprachfeatures
{
	internal class Program
	{
		static void Main(string[] args)
		{
			object o = new object();
			if (o is int)
			{
				int x = (int) o;
			}

			if (o is int x) //auto
			{

			}

			string text = "123";
			if (int.TryParse(text, out int ergebnis))
			{

			}
		}
	}
}