using UnityEngine;
using UnityEngine.UI;
using System;

public class SlotFactory
{
	GameObject _tilePrefab;
	GameObject _slotPrefab;

	SlotPool _slotPool;
	SlotPool _answerSlotPool;

	public SlotFactory(GameObject tilePrefab, GameObject slotPrefab, SlotPool slotPool, SlotPool answerSlotPool)
	{
		_tilePrefab = tilePrefab;
		_slotPrefab = slotPrefab;

		_slotPool = slotPool;
		_answerSlotPool = answerSlotPool;
	}

	public DraggableObject CreateTile(char character, Vector3 position)
	{
		GameObject tile = UnityEngine.Object.Instantiate(_tilePrefab) as GameObject;

		position.z = -1f;
		tile.transform.position = position;
		
		DraggableObject draggableObject = tile.GetComponent<DraggableObject>();
		draggableObject.character = character;
		draggableObject.SetSlotPools(_slotPool, _answerSlotPool);

		if(draggableObject is LetterButton)
			(draggableObject as LetterButton).slotFactory = this;

		Text text = tile.GetComponentInChildren<Text>();
		text.text = character.ToString();

		return draggableObject;
	}

	public void CreateSlot(Vector3 position)
	{
		GameObject slot = UnityEngine.Object.Instantiate (_slotPrefab) as GameObject;
		slot.transform.position = position;
		_slotPool.AddSlot (slot.GetComponent<Slot> ());
	}

	public void CreateAnswerSlot(Vector3 position)
	{
		GameObject slot = UnityEngine.Object.Instantiate(_slotPrefab) as GameObject;
		slot.transform.position = position;
		_answerSlotPool.AddSlot(slot.GetComponent<Slot>());
	}
}
