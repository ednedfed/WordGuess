using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DraggableObject : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;
	public Canvas canvas;

	public char character;

	protected SlotPool _tileSlotPool;
	protected SlotPool _answerSlotPool;

	protected SlotPool _currentSlotPool;
	protected SlotPool _nextSlotPool;
	
	void Awake()
	{
		Text myText = GetComponentInChildren<Text>();
		character = char.Parse(myText.text);
	}

	public void SetSlotPools(SlotPool tileSlotPool, SlotPool answerSlotPool)
	{
		_tileSlotPool = tileSlotPool;
		_answerSlotPool = answerSlotPool;

		_currentSlotPool = _tileSlotPool;
		_nextSlotPool = _answerSlotPool;

		_currentSlotPool.AddToNextFreeSlot(this);
	}

	protected void SwapCurrentSlotPool()
	{
		if(_nextSlotPool.IsFull())
			return;
		
		_currentSlotPool.RemoveFromSlot(this);
		_nextSlotPool.AddToNextFreeSlot(this);

		SlotPool tempPool = _currentSlotPool;
		_currentSlotPool = _nextSlotPool;
		_nextSlotPool = tempPool;
	}

	protected virtual void OnMouseDown()
	{
		SwapCurrentSlotPool();
	}
}
