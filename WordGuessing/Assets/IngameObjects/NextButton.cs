using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
	void OnMouseUp()
	{
		TransitionData.IncrementLevel();

		SceneManager.LoadScene(LevelNames.Gameplay);
	}
}
