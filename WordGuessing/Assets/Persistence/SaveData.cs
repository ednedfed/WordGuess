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
	const string fileName = "user_data";

	static HashSet<string> _completedLevels = new HashSet<string>();

	static SaveData()
	{
		Load();
	}

	public static bool IsCompleted(string answer)
	{
		return _completedLevels.Contains(answer);
	}

	public static void AddToCompleted(string answer)
	{
		_completedLevels.Add(answer);
	}

	public static void Load()
	{
		try
		{
			if(File.Exists(GetFullPath()) == false)
				return;

			_completedLevels.Clear();

			XmlDocument document = new XmlDocument();
			document.Load(GetFullPath());

			foreach (XmlNode node in document.DocumentElement.ChildNodes)
			{
				string currentLevel = node.Name;

				_completedLevels.Add(currentLevel);
			}
		}
		catch(Exception e)
		{
			Debug.LogError(e);
		}
	}

	public static void Save()
	{
		try
		{
			XmlDocument document = new XmlDocument();
			XmlNode rootNode = document.CreateElement("completedLevels");

			foreach (string answer in _completedLevels)
			{
				XmlNode node = document.CreateElement(answer);

				rootNode.AppendChild(node);
			}

			document.AppendChild(rootNode);

			document.Save (GetFullPath());
		}
		catch(Exception e)
		{
			Debug.LogError(e);
		}
	}

	static string GetFullPath()
	{
		return Application.persistentDataPath + "\\" + fileName;
	}
}
