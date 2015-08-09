using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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

	event Action<DraggableObject> _onLetterAdded;
	event Action<DraggableObject> _onLetterRemoved;

	void Awake()
	{
		Text myText = GetComponentInChildren<Text>();
		character = char.Parse(myText.text);
	}

	public void Register(Action<DraggableObject> onLetterAdded, Action<DraggableObject> onLetterRemoved)
	{
		_onLetterAdded += onLetterAdded;
		_onLetterRemoved += onLetterRemoved;
	}

	public void Unregister(Action<DraggableObject> onLetterAdded, Action<DraggableObject> onLetterRemoved)
	{
		_onLetterAdded -= onLetterAdded;
		_onLetterRemoved -= onLetterRemoved;
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
		{
			if(_onLetterAdded != null)
				_onLetterAdded(this);
		}
		else
		{
			if(_onLetterRemoved != null)
				_onLetterRemoved(this);
		}
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
