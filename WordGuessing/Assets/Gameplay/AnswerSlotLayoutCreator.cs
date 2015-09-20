using UnityEngine;

class AnswerSlotLayoutCreator
{
	float _tileWidth;
	int _tilesPerRow;
	float _startHeight;
	SlotFactory _slotFactory;

	public AnswerSlotLayoutCreator(float tileWidth, int tilesPerRow, float startHeight, SlotFactory slotFactory)
	{
		_tileWidth = tileWidth;
		_tilesPerRow = tilesPerRow;
		_startHeight = startHeight;
		_slotFactory = slotFactory;
	}

	public void CreateAnswerSlotLayout(int answerLength)
	{
		Vector3 startPos = TileLayoutUtilities.GetCenteredTileStartPosX(_tileWidth, Mathf.Min(_tilesPerRow, answerLength));
		startPos.y = _startHeight;
		
		int index = 0;
		Vector3 position = Vector3.zero;
		while(index < answerLength)
		{
			for(int x=0; x<_tilesPerRow; ++x)
			{
				position.x = startPos.x + _tileWidth*x;
				position.y = startPos.y - _tileWidth*Mathf.FloorToInt(index/_tilesPerRow);
				
				_slotFactory.CreateAnswerSlot(position);
				
				index++;
				if(index >= answerLength)
					break;
			}
		}
	}
}