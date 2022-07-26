﻿namespace Multitasking;

internal class _07_Unwrap
{
	static void Main(string[] args)
	{
		Task<Task<int>> verschachtelt = null;
		Task<int> einzeln = verschachtelt.Unwrap(); //Äußerste Schicht entfernen

		Task<byte[]> data = Task.Run(GetData);
		Task<Task<byte>> schritt2 = data.ContinueWith(t => Task.Run(() => Compute(t.Result))); //Bei ContinueWith einen weiteren Task machen

		byte b = schritt2.Unwrap().Result; //Ergebnis von innen holen
	}

	private static byte[] GetData()
	{
		Random rand = new Random();
		byte[] bytes = new byte[64];
		rand.NextBytes(bytes);
		return bytes;
	}

	static byte Compute(byte[] data)
	{
		byte final = 0;
		foreach (byte item in data)
		{
			final ^= item;
			Console.WriteLine("{0:x}", final);
		}
		Console.WriteLine("Done computing");
		return final;
	}
}
