using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class RegistrationManager : MonoBehaviour
{
	public string answer = "        ";
	public string tiles = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public GameObject tilePrefab;
	public GameObject slotPrefab;
	
	public int tilesPerRow = 7;
	public float tileWidth = 0.22f;
	
	public float slotsY = -0.2f;
	public float tilesY = -0.7f;

	SlotFactory _slotFactory;
	
	SlotPool _slotPool = new SlotPool();
	SlotPool _answerSlotPool = new SlotPool();

	NameInputManager _nameInputManager;

	public void Submit()
	{
		string username = _nameInputManager.GetString();

		TransitionData.SetUsername(username);
		SaveData.Save();

		Debug.Log(username);
	}

	void Awake()
	{
		_nameInputManager = new NameInputManager(answer);

		_slotFactory = new SlotFactory(tilePrefab, slotPrefab, _slotPool, _answerSlotPool);
		
		SetupSlots();
		
		SetupTiles();
	}
	
	void SetupTiles()
	{
		Vector3 startPos = TileLayoutUtilities.GetTileXStartCentered(tileWidth, tilesPerRow);
		startPos.y = tilesY;

		Vector3 position = Vector3.zero;
		for(int i=0; i<tiles.Length; ++i)
		{
			position.x = startPos.x + tileWidth*(i%tilesPerRow);
			position.y = startPos.y - tileWidth*Mathf.FloorToInt(i/tilesPerRow);
				
			LetterButton letterButton = _slotFactory.CreateTileWithSlot(tiles[i], position) as LetterButton;
			letterButton.slotFactory = _slotFactory;
			letterButton.nameInputManager = _nameInputManager;

			_nameInputManager.ListenTo(letterButton);
		}
	}
	
	void SetupSlots()
	{
		Vector3 startPos = TileLayoutUtilities.GetTileXStartCentered(tileWidth, Mathf.Min(tilesPerRow, answer.Length));
		startPos.y = slotsY;
		
		int index = 0;
		Vector3 position = Vector3.zero;
		while(index < answer.Length)
		{
			for(int x=0; x<tilesPerRow; ++x)
			{
				position.x = startPos.x + tileWidth*x;
				position.y = startPos.y - tileWidth*Mathf.FloorToInt(index/tilesPerRow);
				
				_slotFactory.CreateAnswerSlot(position);
				
				index++;
				if(index >= answer.Length)
					break;
			}
		}
	}
}