using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutButton : MonoBehaviour
{
	void OnMouseUp()
	{
		SceneManager.LoadScene(LevelNames.Registration);
	}
}
