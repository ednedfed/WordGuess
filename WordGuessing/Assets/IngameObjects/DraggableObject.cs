using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;
	public Canvas canvas;

	public char character;

	Slot _mySlot;

	SlotPool _slotPool;
	SlotPool _answerSlotPool;

	SlotPool _currentSlotPool;
	SlotPool _nextSlotPool;

	AnswerManager _answerManager;

	void Awake()
	{
		Text myText = GetComponentInChildren<Text>();
		character = char.Parse(myText.text);
	}

	public void SetAnswerManager(AnswerManager answerManager)
	{
		_answerManager = answerManager;
	}

	public void SetSlot(Slot slot)
	{
		_mySlot = slot;
	}

	public void SetSlotPools(SlotPool slotPool, SlotPool answerSlotPool)
	{
		_slotPool = slotPool;
		_answerSlotPool = answerSlotPool;

		_currentSlotPool = _slotPool;
		_nextSlotPool = _answerSlotPool;
	}

	void SwapCurrentSlotPool()
	{
		SlotPool tempPool = _currentSlotPool;
		_currentSlotPool = _nextSlotPool;
		_nextSlotPool = tempPool;

		if(_currentSlotPool == _answerSlotPool)
			_answerManager.AddLetter(this);
		else
			_answerManager.RemoveLetter(this);
	}

	void OnMouseDown()
	{
		//Debug.Log("On Mouse Up");
		if(_nextSlotPool.IsFull())
			return;

		_currentSlotPool.SetFree(_mySlot);

		_mySlot = _nextSlotPool.GetNextFreeSlot();

		_nextSlotPool.SetUsed(_mySlot);

		SwapCurrentSlotPool();

		Vector3 newPos = _mySlot.transform.position;
		newPos.z = -1f;
		
		transform.position = newPos;
	}
}
