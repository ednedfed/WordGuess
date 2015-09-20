using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
	public float xOffset;
	public float startY;
	public float yOffset;
	
	public GameObject levelButtonPrefab;

	public string fileName;

	void Awake()
	{
		LoadLevelData();

		CreateLevelButtons();
	}

	void LoadLevelData()
	{
		try
		{
			TransitionData.Levels = new XmlLevelLoader(fileName).LoadLevels();
		}
		catch(Exception e)
		{
			Debug.LogError(e);
		}
	}

	void CreateLevelButtons()
	{
		for (int i = 0; i < TransitionData.Levels.Length; ++i)
		{
			GameObject levelButtonObject = UnityEngine.Object.Instantiate(levelButtonPrefab) as GameObject;

			Vector3 position = new Vector3 (xOffset, (TransitionData.Levels.Length - i) * yOffset + startY, 0f);
			levelButtonObject.transform.position = position;

			LevelSelectButton levelSelectButton = levelButtonObject.GetComponent<LevelSelectButton>();
			levelSelectButton.isCompleted = SaveData.IsLevelCompleted(TransitionData.Levels[i].answer);
			levelSelectButton.text.text = "LEVEL " + i.ToString ();
			levelSelectButton.id = i;
		}
	}
}
