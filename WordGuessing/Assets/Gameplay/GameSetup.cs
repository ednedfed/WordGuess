using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class GameSetup : MonoBehaviour
{
	public string answer = "TITBAG";
	public RawImage image;

	public GameObject tilePrefab;
	public GameObject slotPrefab;

	public int tilesPerRow = 7;
	public float tileWidth = 0.22f;

	public float slotsY = -0.2f;
	public float tilesY = -0.7f;

	AnswerManager _answerManager;
	SlotFactory _slotFactory;
	
	SlotPool _slotPool = new SlotPool();
	SlotPool _answerSlotPool = new SlotPool();
	
	void Awake()
	{
		if(TransitionData.CurrentLevel.answer != null)
		{
			answer = TransitionData.CurrentLevel.answer;
			image.texture = Resources.Load(TransitionData.CurrentLevel.image) as Texture;
		}

		_answerManager = new AnswerManager(answer);
		_answerManager.onWon += OnWon;

		_slotFactory = new SlotFactory(tilePrefab, slotPrefab, _slotPool, _answerSlotPool);

		SetupSlots();
		
		SetupTiles();
	}
	
	void SetupTiles()
	{
		Vector3 startPos = TileLayoutUtilities.GetTileXStartCentered(tileWidth, tilesPerRow);
		startPos.y = tilesY;

		//get randomised valid positions
		List<char> randomLetters = new List<char>();
		
		for(int i=0; i<answer.Length; ++i)
			randomLetters.Add(answer[i]);
		
		int requiredLetters = Mathf.CeilToInt((float)answer.Length / tilesPerRow) * tilesPerRow;
		for(int i=randomLetters.Count; i<requiredLetters; ++i)
			randomLetters.Add(RandomLetter.GetRandomLetter());
		
		RandomShuffle.Shuffle(randomLetters);
		
		int index = 0;
		Vector3 position = Vector3.zero;
		while(index < answer.Length)
		{
			for(int x=0; x<tilesPerRow; ++x)
			{
				position.x = startPos.x + tileWidth*x;
				position.y = startPos.y - tileWidth*Mathf.FloorToInt(index/tilesPerRow);
				
				DraggableObject tile = _slotFactory.CreateTileWithSlot(randomLetters[index++], position);

				_answerManager.ListenTo(tile);
			}
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

	void OnWon(string answer)
	{
		SaveData.AddToCompleted(answer);
		SaveData.SaveDataForCurrentUser();

		StartCoroutine(LoadLevelAfterDelay.Execute(0.2f, LevelNames.Complete));
	}
}