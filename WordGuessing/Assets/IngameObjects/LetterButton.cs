using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LetterButton : DraggableObject
{
	public SlotFactory slotFactory {set; private get;}
	public NameInputManager nameInputManager {set; private get;}

	protected override void OnMouseDown()
	{
		if(_currentSlotPool == _slotPool)
		{
			if(_answerSlotPool.IsFull())
				return;

			Slot slot = _answerSlotPool.GetNextFreeSlot();
			_answerSlotPool.SetUsed(slot);

			LetterButton letterButton = slotFactory.CreateTile(this.character, this.transform.position) as LetterButton;

			//must happen before swapping, unless we refactor
			nameInputManager.ListenTo(letterButton);

			letterButton.SwapCurrentSlotPool();
			letterButton.SetSlot(slot);
			letterButton.nameInputManager = nameInputManager;

			Vector3 newPos = slot.transform.position;
			newPos.z = -1f;
			
			letterButton.transform.position = newPos;
		}
		else
		{
			SwapCurrentSlotPool();

			_answerSlotPool.SetFree(_mySlot);

			GameObject.Destroy(this.gameObject);
		}
	}
}
