using System.Collections;
using UnityEngine;

public static class LoadLevelAfterDelay
{
	public static IEnumerator Execute(float seconds, string levelName)
	{
		yield return new WaitForSeconds(seconds);
		
		Application.LoadLevel(levelName);
	}
}