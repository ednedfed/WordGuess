using UnityEngine;
using System.Collections;

public class SubmitButton : MonoBehaviour
{
	public RegistrationManager registrationManager;

	void OnMouseUp()
	{
		registrationManager.Submit();
		
		Application.LoadLevel(LevelNames.LevelSelect);
	}
}
