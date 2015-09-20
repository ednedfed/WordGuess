using UnityEngine;

public static class TransitionData
{
	public static LevelInfo CurrentLevel
	{
		get
		{
			return Levels[_currentId];
		}
	}

	public static LevelInfo[] Levels;

	static int _currentId;
	static string _username;

	public static void SetUsername(string username)
	{
		_username = username;
	}

	public static string GetUsername()
	{
		return _username;
	}

	public static void SetCurrentLevel(int newId)
	{
		_currentId = newId;
	}

	public static void IncrementLevel()
	{
		_currentId = (_currentId + 1) % Levels.Length;
	}
}
