using UnityEngine;
using UnityEngine.SceneManagement;

public class SubmitButton : MonoBehaviour
{
	public RegistrationManager registrationManager;

	void OnMouseUp()
	{
		registrationManager.SubmitUsername();
		
		SceneManager.LoadScene(LevelNames.LevelSelect);
	}
}
