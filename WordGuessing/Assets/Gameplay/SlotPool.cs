using System.Collections.Generic;
using UnityEngine;
using System;

public class SlotPool
{
	List<Slot> _slots = new List<Slot>();
	int _usedSlotCount = 0;

	public event Action<DraggableObject> OnTileAdded;
	public event Action<DraggableObject> OnTileRemoved;

	public bool IsFull()
	{
		return _usedSlotCount == _slots.Count;
	}

	public void AddSlot(Slot slot)
	{
		_slots.Add(slot);
	}

	public void AddToNextFreeSlot(DraggableObject draggableObject)
	{
		for(int i=0; i<_slots.Count; ++i)
		{
			if(_slots[i].draggableObject == null)
			{
				_slots[i].draggableObject = draggableObject;
				_usedSlotCount++;

				Vector3 newPos = _slots[i].transform.position;
				newPos.z = -1f;
				draggableObject.transform.position = newPos;

				Debug.Log("setting pos: " + draggableObject.character + " " + draggableObject.transform.position.ToString());

				if(OnTileAdded != null)
					OnTileAdded(draggableObject);

				break;
			}
		}
	}

	public void RemoveFromSlot(DraggableObject draggableObject)
	{
		for(int i=0; i<_slots.Count; ++i)
		{
			if(_slots[i].draggableObject == draggableObject)
			{
				_slots[i].draggableObject = null;
				_usedSlotCount--;

				if(OnTileRemoved != null)
					OnTileRemoved(draggableObject);

				break;
			}
		}
	}
}
