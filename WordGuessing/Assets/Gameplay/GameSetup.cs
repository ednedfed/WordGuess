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

	TileLayoutCreator _tileLayoutCreator;
	AnswerSlotLayoutCreator _answerSlotLayoutCreator;

	SlotPool _slotPool = new SlotPool();
	SlotPool _answerSlotPool = new SlotPool();

	void Awake()
	{
		if(TransitionData.CurrentLevel.answer != null)
		{
			answer = TransitionData.CurrentLevel.answer;
			image.texture = Resources.Load(TransitionData.CurrentLevel.image) as Texture;
		}

		_answerManager = new AnswerManager(answer, OnWon);
		_answerManager.ListenTo(_answerSlotPool);

		_slotFactory = new SlotFactory(tilePrefab, slotPrefab, _slotPool, _answerSlotPool);
		_tileLayoutCreator = new TileLayoutCreator(tileWidth, tilesPerRow, tilesY, _slotFactory);
		_answerSlotLayoutCreator = new AnswerSlotLayoutCreator(tileWidth, tilesPerRow, slotsY, _slotFactory);

		_tileLayoutCreator.CreateTileLayout(GenerateTileLettersFromAnswer());
		_answerSlotLayoutCreator.CreateAnswerSlotLayout(answer.Length);
	}

	string GenerateTileLettersFromAnswer()
	{
		List<char> randomLetters = new List<char> ();

		for (int i = 0; i < answer.Length; ++i)
			randomLetters.Add (answer [i]);

		int requiredLetters = Mathf.CeilToInt ((float)answer.Length / tilesPerRow) * tilesPerRow;

		for (int i = randomLetters.Count; i < requiredLetters; ++i)
			randomLetters.Add(RandomLetter.GetRandomLetter ());

		RandomShuffle.Shuffle(randomLetters);

		return new string(randomLetters.ToArray());
	}

	void OnWon(string answer)
	{
		SaveData.AddToCompletedLevels(answer);
		SaveData.SaveDataForCurrentUser();

		StartCoroutine(LoadLevelAfterDelay.Execute(0.2f, LevelNames.Complete));
	}
}