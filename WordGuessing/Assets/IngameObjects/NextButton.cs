using UnityEngine;
using System.Collections;

public class NextButton : MonoBehaviour
{
	void OnMouseUp()
	{
		TransitionData.IncrementLevel();

		Application.LoadLevel(LevelNames.Gameplay);
	}
}
