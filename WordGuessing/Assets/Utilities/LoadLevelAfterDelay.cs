using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadLevelAfterDelay
{
	public static IEnumerator Execute(float seconds, string levelName)
	{
		yield return new WaitForSeconds(seconds);
		
		SceneManager.LoadScene(levelName);
	}
}