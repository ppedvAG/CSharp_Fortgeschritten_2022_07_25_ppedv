using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		string path = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2022_07_25\Plugin\bin\Debug\net6.0\Plugin.dll";

		Assembly loaded = Assembly.LoadFrom(path);

		object withoutPluginBase = Activator.CreateInstance(loaded.DefinedTypes.First(e => e.Name == "Plugin")); //Ohne PluginBase

		withoutPluginBase.GetType().GetMethod("Method1").Invoke(withoutPluginBase, null); //Ohne PluginBase ausführen

		int x = (int) withoutPluginBase.GetType().GetMethod("Method2").Invoke(withoutPluginBase, new object[] { 3, 5 }); //Method2 ohne PluginBase

		ISpecificPlugin plugin = Activator.CreateInstance(loaded.DefinedTypes.First(e => e.Name == "Plugin")) as ISpecificPlugin;

		plugin.Method1();

		Console.WriteLine(plugin.Method2(3, 6));
	}
}