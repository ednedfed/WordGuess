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

	TileLayoutCreator _tileLayoutCreator;
	AnswerSlotLayoutCreator _answerSlotLayoutCreator;

	public void SubmitUsername()
	{
		string enteredUsername = _nameInputManager.GetString();

		TransitionData.SetUsername(enteredUsername);

		SaveData.LoadDataForCurrentUser();

		Debug.Log(enteredUsername);
	}

	void Awake()
	{
		_nameInputManager = new NameInputManager(answer);
		_nameInputManager.ListenTo(_answerSlotPool);

		_slotFactory = new SlotFactory(tilePrefab, slotPrefab, _slotPool, _answerSlotPool);

		_answerSlotLayoutCreator = new AnswerSlotLayoutCreator(tileWidth, tilesPerRow, slotsY, _slotFactory);
		_tileLayoutCreator = new TileLayoutCreator(tileWidth, tilesPerRow, tilesY, _slotFactory);

		_answerSlotLayoutCreator.CreateAnswerSlotLayout(answer.Length);
		_tileLayoutCreator.CreateTileLayout(tiles);
	}
}