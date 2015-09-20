using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class UserSelection : MonoBehaviour
{
	public float xOffset;
	public float startY;
	public float yOffset;
	
	public GameObject userButtonPrefab;
	
	public string fileName;

	List<string> _usernames = new List<string>();

	void Awake()
	{
		LoadUsers();
		
		CreateUserButtons();
	}
	
	void LoadUsers()
	{
		try
		{
			XmlDocument document = new XmlDocument();
			document.Load(fileName);

			XmlNodeList userElements = document.GetElementsByTagName("user");

			foreach (XmlNode node in userElements)
			{
				_usernames.Add(node["name"].InnerText);
			}
		}
		catch(Exception e)
		{
			Debug.LogError(e);
		}
	}
	
	void CreateUserButtons()
	{
		for (int i = 0; i<_usernames.Count; ++i)
		{
			GameObject levelButtonObject = UnityEngine.Object.Instantiate(userButtonPrefab) as GameObject;
			
			Vector3 position = new Vector3 (xOffset, (_usernames.Count - i) * yOffset + startY, 0f);
			levelButtonObject.transform.position = position;
			
			UserButton userButton = levelButtonObject.GetComponent<UserButton>();
			userButton.text.text = _usernames[i];
		}
	}

	string GetFullPath()
	{
		return Application.persistentDataPath + "\\" + fileName;
	}
}
