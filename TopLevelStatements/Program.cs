//Main Methode und Program Klasse rundherum (nicht sichtbar)
//Gut um schnell Skripte zu schreiben


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int x = 0;
string[] str = args; //auf args zugreifen
for (int i = 0; i < 10; i++)
{
	Console.WriteLine(i * x);
}

void Test() { } //lokale Methode