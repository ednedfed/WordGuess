using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class GameSetup : MonoBehaviour
{
	public string _answer = "TITBAG";
	
	public GameObject tilePrefab;
	public GameObject slotPrefab;

	public int tilesPerRow = 7;
	
	public float tileWidth = 0.22f;

	public float slotsY = -0.2f;
	public float tilesY = -0.7f;

	public string wonSceneName;

	AnswerManager _answerManager;

	Slot[] _answerSlots;
	SlotPool _slotPool = new SlotPool();
	SlotPool _answerSlotPool = new SlotPool();
	
	void Awake()
	{
		if(TransitionData.levelInfo.answer != null)
			_answer = TransitionData.levelInfo.answer;

		_answerManager = new AnswerManager(_answer);

		_answerManager.onWon += OnWon;

		SetupSlots();
		
		SetupTiles();
	}
	
	void SetupTiles()
	{
		Vector3 startPos = GetTileXStartCentered(tileWidth, tilesPerRow);
		startPos.y = tilesY;

		//get randomised valid positions
		List<char> randomLetters = new List<char>();
		
		for(int i=0; i<_answer.Length; ++i)
			randomLetters.Add(_answer[i]);
		
		int requiredLetters = Mathf.CeilToInt((float)_answer.Length / tilesPerRow) * tilesPerRow;
		for(int i=randomLetters.Count; i<requiredLetters; ++i)
			randomLetters.Add(RandomLetter.GetRandomLetter());
		
		RandomShuffle.Shuffle(randomLetters);
		
		int index = 0;
		Vector3 position = Vector3.zero;
		while(index < _answer.Length)
		{
			for(int x=0; x<tilesPerRow; ++x)
			{
				position.x = startPos.x + tileWidth*x;
				position.y = startPos.y - tileWidth*Mathf.FloorToInt(index/tilesPerRow);
				
				CreateTileWithSlot(randomLetters[index++], position);
			}
		}
	}
	
	void SetupSlots()
	{
		_answerSlots = new Slot[_answer.Length];

		Vector3 startPos = GetTileXStartCentered(tileWidth, Mathf.Min(tilesPerRow, _answer.Length));
		startPos.y = slotsY;

		int index = 0;
		Vector3 position = Vector3.zero;
		while(index < _answer.Length)
		{
			for(int x=0; x<tilesPerRow; ++x)
			{
				position.x = startPos.x + tileWidth*x;
				position.y = startPos.y - tileWidth*Mathf.FloorToInt(index/tilesPerRow);
				
				CreateAnswerSlot(index++, position);

				if(index >= _answer.Length)
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
	
	void CreateTileWithSlot(char character, Vector3 position)
	{
		GameObject tile = Object.Instantiate(tilePrefab) as GameObject;
		GameObject slot = Object.Instantiate(slotPrefab) as GameObject;
		
		slot.transform.position = position;

		position.z = -1f;
		tile.transform.position = position;

		_slotPool.AddSlot(slot.GetComponent<Slot>());

		DraggableObject draggableObject = tile.GetComponent<DraggableObject>();
		draggableObject.character = character;
		draggableObject.SetAnswerManager(_answerManager);
		draggableObject.SetSlotPools(_slotPool, _answerSlotPool);

		Text text = tile.GetComponentInChildren<Text>();
		text.text = character.ToString();

		draggableObject.SetSlot(slot.GetComponent<Slot>());
		_slotPool.SetUsed(slot.GetComponent<Slot>());
	}
	
	void CreateAnswerSlot(int index, Vector3 position)
	{
		GameObject slot = Object.Instantiate(slotPrefab) as GameObject;
		
		slot.transform.position = position;

		_answerSlotPool.AddSlot(slot.GetComponent<Slot>());

		_answerSlots[index] = slot.GetComponent<Slot>();
	}

	void OnWon()
	{
		StartCoroutine(LoadLevelAfterDelay(0.2f, wonSceneName));
	}

	IEnumerator LoadLevelAfterDelay(float seconds, string levelName)
	{
		yield return new WaitForSeconds(seconds);

		Application.LoadLevel(wonSceneName);
	}
}