public class PrimeComponent
{
	public event Action<int, int> NotPrime;
	public event Action<int> Prime;
	public event Action<int> Prime100;
	
	public void StartProcess()
	{
		Prime(2);
		for (int i = 3, counter = 0; ; i += 2)
		{
			if (CheckPrime(i))
			{
				counter++;
				if (counter % 100 != 0)
					Prime(i);
				else
					Prime100(i);
			}
			Thread.Sleep(500);
		}
	}
	
	public bool CheckPrime(int num)
	{
		if (num % 2 == 0)
			return false;
		
		for (int i = 3; i <= num / 2; i += 2) //Nur bis zu Hälfte gehen, da größere Zahlen nicht teilbar sein können
		{
			if (num % i == 0)
			{
				NotPrime(num, i);
				return false;
			}
		}
		return true;
	}
}