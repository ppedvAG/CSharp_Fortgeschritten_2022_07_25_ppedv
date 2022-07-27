using PluginBase;

namespace Plugin
{
	public class Plugin : ISpecificPlugin
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public void Method1()
		{
			Console.WriteLine("Method1 wurde aufgerufen");
		}

		public int Method2(int z1, int z2)
		{
			return z1 + z2;
		}
	}
}