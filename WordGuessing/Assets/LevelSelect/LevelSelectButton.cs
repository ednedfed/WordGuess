using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
	public GameObject completedIcon;

	public Text text;
	public LevelInfo levelInfo;

	void Start()
	{
		bool isCompleted = SaveData.IsCompleted(levelInfo.answer);

		completedIcon.SetActive(isCompleted);
	}

	void OnMouseUp()
	{
		TransitionData.levelInfo = this.levelInfo;

		Application.LoadLevel("scene1");
	}
}
