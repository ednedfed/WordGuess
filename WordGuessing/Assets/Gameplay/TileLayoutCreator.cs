using UnityEngine;
using System;

class TileLayoutCreator
{
	float _tileWidth;
	int _tilesPerRow;
	float _startHeight;
	SlotFactory _slotFactory;

	public TileLayoutCreator(float tileWidth, int tilesPerRow, float startHeight, SlotFactory slotFactory)
	{
		_tileWidth = tileWidth;
		_tilesPerRow = tilesPerRow;
		_startHeight = startHeight;
		_slotFactory = slotFactory;
	}

	public void CreateTileLayout(string tiles)
	{
		Vector3 startPos = TileLayoutUtilities.GetCenteredTileStartPosX(_tileWidth, _tilesPerRow);
		startPos.y = _startHeight;

		Vector3 position = Vector3.zero;
		for (int iTile = 0; iTile < tiles.Length; ++iTile)
		{
			position.x = startPos.x + _tileWidth * (iTile % _tilesPerRow);
			position.y = startPos.y - _tileWidth * Mathf.FloorToInt (iTile / _tilesPerRow);

			_slotFactory.CreateSlot(position);
			_slotFactory.CreateTile(tiles[iTile], position);
		}
	}
}
