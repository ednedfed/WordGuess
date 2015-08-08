using System;
using UnityEngine;

public class AnswerManager : IDisposable
{
	public event Action<string> onWon = null;

	DraggableObject[] _answer;
	string _desiredAnswer;

	int _earliestFreeSlot;

	public AnswerManager(string answer)
	{
		_desiredAnswer = answer;

		_answer = new DraggableObject[answer.Length];

		_earliestFreeSlot = 0;
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

		if(CheckWin() == true)
		{
			if(onWon != null)
				onWon(_desiredAnswer);
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

	public bool CheckWin()
	{
		for(int i=0; i<_answer.Length; ++i)
		{
			if(_answer[i] == null)
				return false;

			if(_answer[i].character != _desiredAnswer[i])
				return false;
		}

		Debug.Log("WWWWWOOOOOOOONNNNNNNNN");

		return true;
	}

	public void Dispose()
	{
		onWon = null;
		
		_answer = null;
	}
}
