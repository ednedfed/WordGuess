using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System;

[System.Serializable]
public struct LevelInfo
{
	public string answer;
	public string image;
}

public class LevelSelect : MonoBehaviour
{
	public float xOffset;
	public float startY;
	public float yOffset;
	
	public GameObject levelButtonPrefab;

	public LevelInfo[] levels;

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
			throw new Exception();

			List<LevelInfo> loadedLevels = new List<LevelInfo>();

			XmlDocument document = new XmlDocument();
			document.Load(fileName);

			foreach (XmlNode node in document.DocumentElement.ChildNodes)
			{
				LevelInfo levelInfo = new LevelInfo();

				levelInfo.answer = node.Attributes["answer"].InnerText;
				levelInfo.image = node.Attributes["image"].InnerText;

				loadedLevels.Add(levelInfo);
			}

			levels = loadedLevels.ToArray();
		}
		catch(Exception e)
		{
			Debug.LogError(e);
		}
	}

	void CreateLevelButtons()
	{
		//set up buttons with levels
		for (int i = 0; i < levels.Length; ++i)
		{
			GameObject levelButtonObject = UnityEngine.Object.Instantiate(levelButtonPrefab) as GameObject;

			Vector3 position = new Vector3 (xOffset, (levels.Length - i) * yOffset + startY, 0f);
			levelButtonObject.transform.position = position;

			LevelSelectButton levelSelectButton = levelButtonObject.GetComponent<LevelSelectButton> ();
			levelSelectButton.levelInfo = levels [i];
			levelSelectButton.text.text = "LEVEL " + i.ToString ();
		}
	}
}
