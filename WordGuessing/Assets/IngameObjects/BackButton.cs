using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour
{
	void OnMouseUp()
	{
		Application.LoadLevel("LevelSelect");
	}
}
