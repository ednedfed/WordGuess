using System;
using UnityEngine;

public class AnswerManager : IDisposable
{
	Action<string> _onWon;

	DraggableObject[] _currentAnswer;
	string _desiredAnswer;

	int _firstFreeSlotId;

	public AnswerManager(string answer, Action<string> onWon)
	{
		_desiredAnswer = answer;
		_onWon = onWon;

		_currentAnswer = new DraggableObject[answer.Length];

		_firstFreeSlotId = 0;
	}

	public void ListenTo(SlotPool slotPool)
	{
		slotPool.OnTileAdded += AddLetter;
		slotPool.OnTileRemoved += RemoveLetter;//need to unregister later
	}

	public void AddLetter(DraggableObject letter)
	{
		_currentAnswer[_firstFreeSlotId] = letter;

		for(int i=_firstFreeSlotId; i<_currentAnswer.Length; ++i)
		{
			if(_currentAnswer[_firstFreeSlotId] == null)
				break;

			_firstFreeSlotId++;
		}

		if(HasWon() == true)
			_onWon(_desiredAnswer);
	}

	public void RemoveLetter(DraggableObject letter)
	{
		for(int i=0; i<_currentAnswer.Length; ++i)
		{
			if(_currentAnswer[i] == letter)
			{
				_currentAnswer[i] = null;

				if(i<_firstFreeSlotId)
					_firstFreeSlotId = i;
			}
		}
	}

	public bool HasWon()
	{
		for(int i=0; i<_currentAnswer.Length; ++i)
		{
			if(_currentAnswer[i] == null)
				return false;

			if(_currentAnswer[i].character != _desiredAnswer[i])
				return false;
		}

		Debug.Log("WWWWWOOOOOOOONNNNNNNNN");

		return true;
	}

	public void Dispose()
	{
		_onWon = null;
		
		_currentAnswer = null;
	}
}
