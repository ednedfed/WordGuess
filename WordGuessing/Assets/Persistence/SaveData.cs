using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public static class SaveData
{
	const string COMPLETED_LEVELS_NODE_NAME = "completedLevels";

	static HashSet<string> _completedLevels = new HashSet<string>();

	static XmlDocument _document = new XmlDocument();

	public static bool IsLevelCompleted(string answer)
	{
		return _completedLevels.Contains(answer);
	}

	public static void AddToCompletedLevels(string answer)
	{
		_completedLevels.Add(answer);
	}

	public static void LoadDataForCurrentUser()
	{
		try
		{
			_completedLevels.Clear();

			string currentUser = TransitionData.GetUsername();

			if(File.Exists(GetSaveDataPathForUser(currentUser)) == false)
				return;

			_document.Load(GetSaveDataPathForUser(currentUser));

			XmlNodeList completedLevelNodeList = _document[COMPLETED_LEVELS_NODE_NAME].ChildNodes;

			foreach(XmlNode completedLevelNode in completedLevelNodeList)
				_completedLevels.Add(completedLevelNode.Name);
		}
		catch(Exception e)
		{
			Debug.LogError(e);
		}
	}

	public static void SaveDataForCurrentUser()
	{
		try
		{
			string currentUser = TransitionData.GetUsername();

			_document = new XmlDocument();

			XmlElement completedLevels = _document.CreateElement(COMPLETED_LEVELS_NODE_NAME);

			foreach (string answer in _completedLevels)
			{
				XmlElement completedLevelNode = _document.CreateElement(answer);

				completedLevels.AppendChild(completedLevelNode);
			}

			_document.AppendChild(completedLevels);

			_document.Save(GetSaveDataPathForUser(currentUser));
		}
		catch(Exception e)
		{
			Debug.LogError(e);
		}
	}

	static string GetSaveDataPathForUser(string username)
	{
		return Application.persistentDataPath + "\\" + username;
	}
}
