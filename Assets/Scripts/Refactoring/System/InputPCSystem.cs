using System;
using UnityEngine;

namespace OldSellswords
{
	public class InputPCSystem : ITick
	{
		public event Action OnLeftChange;
		public event Action OnRightChange;

		public void Tick()
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				//if (!OverGame)
				OnLeftChange?.Invoke();
			}

			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				//if (!OverGame)
				OnRightChange?.Invoke();
			}
		}
	}
}