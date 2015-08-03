using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
	public Text text;
	public LevelInfo levelInfo;

	void OnMouseUp()
	{
		TransitionData.levelInfo = this.levelInfo;

		Application.LoadLevel("scene1");
	}
}
