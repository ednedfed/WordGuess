using UnityEngine;

public static class TileLayoutUtilities
{
	public static Vector3 GetTileXStartCentered(float tileWidth, int numTiles)
	{
		float screenHorizontalCenter = Screen.width * 0.5f;
		Vector3 screenCenter = Camera.main.ScreenToWorldPoint (new Vector3 (screenHorizontalCenter, 0f, 0f));
		float offset = screenCenter.x - (tileWidth * (numTiles-1) * 0.5f);
		Vector3 startPos = new Vector3(offset, 0, 0);
		
		return startPos;
	}
}