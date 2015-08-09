using UnityEngine;

/// <summary>
/// Transition data.
/// 
/// - stores current session data in global cache
/// </summary>

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

	static int _currentId = 0;

	public static void SetCurrentLevel(int newId)
	{
		_currentId = newId;
	}

	public static void IncrementLevel()
	{
		_currentId = (_currentId + 1) % Levels.Length;
	}
}
