using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
	void OnMouseUp()
	{
		SceneManager.LoadScene(LevelNames.LevelSelect);
	}
}
