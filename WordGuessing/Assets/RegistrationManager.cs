using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class RegistrationManager : MonoBehaviour
{
	public string answer = "        ";
	public string tiles = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
//	public RawImage image;
	
	public GameObject tilePrefab;
	public GameObject slotPrefab;
	
	public int tilesPerRow = 7;
	public float tileWidth = 0.22f;
	
	public float slotsY = -0.2f;
	public float tilesY = -0.7f;
	
//	AnswerManager _answerManager;
	SlotFactory _slotFactory;
	
	SlotPool _slotPool = new SlotPool();
	SlotPool _answerSlotPool = new SlotPool();
	
	void Awake()
	{
		_slotFactory = new SlotFactory(tilePrefab, slotPrefab, _slotPool, _answerSlotPool);
		
		SetupSlots();
		
		SetupTiles();
	}
	
	void SetupTiles()
	{
		Vector3 startPos = GetTileXStartCentered(tileWidth, tilesPerRow);
		startPos.y = tilesY;

		Vector3 position = Vector3.zero;
		for(int i=0; i<tiles.Length; ++i)
		{
			position.x = startPos.x + tileWidth*(i%tilesPerRow);
			position.y = startPos.y - tileWidth*Mathf.FloorToInt(i/tilesPerRow);
				
			_slotFactory.CreateTileWithSlot(tiles[i], position);
		}
	}
	
	void SetupSlots()
	{
		Vector3 startPos = GetTileXStartCentered(tileWidth, Mathf.Min(tilesPerRow, answer.Length));
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
	
	Vector3 GetTileXStartCentered(float tileWidth, int numTiles)
	{
		float screenHorizontalCenter = Screen.width * 0.5f;
		Vector3 screenCenter = Camera.main.ScreenToWorldPoint (new Vector3 (screenHorizontalCenter, 0f, 0f));
		float offset = screenCenter.x - (tileWidth * (numTiles-1) * 0.5f);
		Vector3 startPos = new Vector3(offset, 0, 0);
		
		return startPos;
	}
}