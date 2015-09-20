using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// Save data.
/// 
/// singleton currently, until we work out a system for preserving this data between transitions
/// 
/// contains which levels we have completed
/// </summary>
using System;
using System.IO;

public static class SaveData
{
	static HashSet<string> _completedLevels = new HashSet<string>();

	static XmlDocument _document = new XmlDocument();

	public static bool IsCompleted(string answer)
	{
		return _completedLevels.Contains(answer);
	}

	public static void AddToCompleted(string answer)
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

			_completedLevels.Clear();

			_document.Load(GetSaveDataPathForUser(currentUser));

			XmlNodeList completedLevelNodeList = _document["completedLevels"].ChildNodes;

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

			XmlElement completedLevels = _document.CreateElement("completedLevels");

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

	public static string GetSaveDataPathForUser(string username)
	{
		return Application.persistentDataPath + "\\" + username;
	}
}
