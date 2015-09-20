using System.Collections.Generic;
using System.Xml;

class XmlLevelLoader
{
	string _fileName;

	public XmlLevelLoader(string fileName)
	{
		_fileName = fileName;
	}

	public LevelInfo[] LoadLevels()
	{
		List<LevelInfo> loadedLevels = new List<LevelInfo>();

		XmlDocument document = new XmlDocument();
		document.Load(_fileName);
		
		foreach (XmlNode node in document.DocumentElement.ChildNodes)
		{
			LevelInfo levelInfo = new LevelInfo();
			levelInfo.answer = node.Attributes["answer"].InnerText;
			levelInfo.image = node.Attributes["image"].InnerText;
			loadedLevels.Add(levelInfo);
		}
		
		return loadedLevels.ToArray();
	}
}
