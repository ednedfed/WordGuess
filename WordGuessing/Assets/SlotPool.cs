using System.Collections.Generic;

public class SlotPool
{
	List<Slot> _slots = new List<Slot>();
	HashSet<int> _usedSlots = new HashSet<int>();

	public bool IsFull()
	{
		return _usedSlots.Count == _slots.Count;
	}

	public void AddSlot(Slot slot)
	{
		_slots.Add(slot);
	}

	public void SetUsed(Slot slot)
	{
		for(int i=0; i<_slots.Count; ++i)
		{
			if(_slots[i] == slot)
				_usedSlots.Add(i);
		}
	}

	public void SetFree(Slot slot)
	{
		for(int i=0; i<_slots.Count; ++i)
		{
			if(_slots[i] == slot)
				_usedSlots.Remove(i);
		}
	}

	public Slot GetNextFreeSlot()
	{
		for(int i=0; i<_slots.Count; ++i)
		{
			if(_usedSlots.Contains(i) == false)
				return _slots[i];
		}

		return null;
	}
}
