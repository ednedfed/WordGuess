using System;

public class RandomLetter
{
	const string _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	static Random _random = new Random();

	static public char GetRandomLetter()
	{
		int index = _random.Next(_letters.Length);

		return _letters[index];
	}
}
