using UnityEngine;
using UnityEngine.UI;

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

	public DraggableObject CreateTileWithSlot(char character, Vector3 position)
	{
		GameObject tile = Object.Instantiate(_tilePrefab) as GameObject;
		GameObject slot = Object.Instantiate(_slotPrefab) as GameObject;
		
		slot.transform.position = position;
		
		position.z = -1f;
		tile.transform.position = position;
		
		_slotPool.AddSlot(slot.GetComponent<Slot>());
		
		DraggableObject draggableObject = tile.GetComponent<DraggableObject>();
		draggableObject.character = character;
		draggableObject.SetSlotPools(_slotPool, _answerSlotPool);
		
		Text text = tile.GetComponentInChildren<Text>();
		text.text = character.ToString();
		
		draggableObject.SetSlot(slot.GetComponent<Slot>());
		_slotPool.SetUsed(slot.GetComponent<Slot>());

		return draggableObject;
	}

	public void CreateAnswerSlot(Vector3 position)
	{
		GameObject slot = Object.Instantiate(_slotPrefab) as GameObject;
		
		slot.transform.position = position;
		
		_answerSlotPool.AddSlot(slot.GetComponent<Slot>());
	}
}
