using UnityEngine;
using System.Collections;

public class SubmitButton : MonoBehaviour
{
	public RegistrationManager registrationManager;

	void OnMouseUp()
	{
		registrationManager.SubmitUsername();
		
		Application.LoadLevel(LevelNames.LevelSelect);
	}
}
