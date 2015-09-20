using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LetterButton : DraggableObject
{
	public SlotFactory slotFactory {set; private get;}

	protected override void OnMouseDown()
	{
		if(_currentSlotPool == _tileSlotPool)
		{
			if(_answerSlotPool.IsFull())
				return;

			LetterButton letterButton = slotFactory.CreateTile(this.character, this.transform.position) as LetterButton;

			//new letter gets put into answer
			letterButton.SwapCurrentSlotPool();
		}
		else
		{
			SwapCurrentSlotPool();

			GameObject.Destroy(this.gameObject);
		}
	}
}
