using UnityEngine;
using System.Collections;

public class LogoutButton : MonoBehaviour
{
	void OnMouseUp()
	{
		Application.LoadLevel(LevelNames.Registration);
	}
}
