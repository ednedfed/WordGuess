using UnityEngine;
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

		Application.LoadLevel(LevelNames.Gameplay);
	}
}
