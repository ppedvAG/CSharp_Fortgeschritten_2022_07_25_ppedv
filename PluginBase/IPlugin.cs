namespace PluginBase;

public interface IPlugin //Basis Interface
{
	public string Name { get; set; }

	public string Description { get; set; }
}

public interface ISpecificPlugin : IPlugin //Pluginspezifikation
{
	void Method1();

	int Method2(int z1, int z2);
}