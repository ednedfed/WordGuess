using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
	public GameObject completedIcon;

	public Text text;
	public bool isCompleted;
	public int id;

	void Start()
	{
		completedIcon.SetActive(isCompleted);
	}

	void OnMouseUp()
	{
		TransitionData.SetCurrentLevel(this.id);

		SceneManager.LoadScene(LevelNames.Gameplay);
	}
}
