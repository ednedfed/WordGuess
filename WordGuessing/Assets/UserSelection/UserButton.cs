using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserButton : MonoBehaviour
{
	public Text text;

	void OnMouseUp()
	{
		TransitionData.SetUsername(this.text.text);
		
		SceneManager.LoadScene(LevelNames.LevelSelect);
	}
}
