using System;
using System.Collections.Generic;

public class RandomShuffle
{
	static Random _random = new Random();

	static public void Shuffle<T>(List<T> input)
	{
		int n = input.Count;
		for (int i=0; i<n; ++i)
		{
			int dest = i + (int)(_random.NextDouble() * (n-i));

			T t = input[dest];
			input[dest] = input[i];
			input[i] = t;
		}
	}
}
