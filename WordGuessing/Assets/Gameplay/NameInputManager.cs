using System.Text;
using System;

public class NameInputManager
{
	DraggableObject[] _answer;
	int _earliestFreeSlot;

	public NameInputManager(string answer)
	{
		_answer = new DraggableObject[answer.Length];
		
		_earliestFreeSlot = 0;
	}

	public void ListenTo(SlotPool slotPool)
	{
		slotPool.OnTileAdded += AddLetter;
		slotPool.OnTileRemoved += RemoveLetter;//need to unregister later
	}

	public string GetString()
	{
		StringBuilder finalAnswer = new StringBuilder();

		for(int i=0; i<_answer.Length; ++i)
		{
			if(_answer[i] == null)
			{
				finalAnswer.Append(" ");
			}
			else
			{
				finalAnswer.Append(_answer[i].character);
			}
		}

		return finalAnswer.ToString().Trim();
	}

	public void AddLetter(DraggableObject letter)
	{
		_answer[_earliestFreeSlot] = letter;
		
		for(int i=_earliestFreeSlot; i<_answer.Length; ++i)
		{
			if(_answer[_earliestFreeSlot] == null)
				break;
			
			_earliestFreeSlot++;
		}
	}
	
	public void RemoveLetter(DraggableObject letter)
	{
		for(int i=0; i<_answer.Length; ++i)
		{
			if(_answer[i] == letter)
			{
				_answer[i] = null;
				
				if(i<_earliestFreeSlot)
					_earliestFreeSlot = i;
			}
		}
	}
}
