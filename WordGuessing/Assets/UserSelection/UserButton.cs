using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserButton : MonoBehaviour
{
	public Text text;

	void OnMouseUp()
	{
		TransitionData.SetUsername(this.text.text);
		
		Application.LoadLevel(LevelNames.LevelSelect);
	}
}
