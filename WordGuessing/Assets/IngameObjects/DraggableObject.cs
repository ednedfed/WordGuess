using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DraggableObject : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;
	public Canvas canvas;

	public char character;

	protected Slot _mySlot;

	protected SlotPool _slotPool;
	protected SlotPool _answerSlotPool;

	protected SlotPool _currentSlotPool;
	protected SlotPool _nextSlotPool;

	protected event Action<DraggableObject> _onLetterAdded;
	protected event Action<DraggableObject> _onLetterRemoved;

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

	protected void SwapCurrentSlotPool()
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

	protected virtual void OnMouseDown()
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
