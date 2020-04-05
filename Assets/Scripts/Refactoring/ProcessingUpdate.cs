using System;
using System.Collections.Generic;
using UnityEngine;

namespace OldSellswords
{
	public class ProcessingUpdate : IDisposable
	{
		private List<ITick> _ticks = new List<ITick>();
		private List<ITickFixed> _ticksFixed = new List<ITickFixed>();
		private List<ITickLate> _ticksLate = new List<ITickLate>();

		public static ProcessingUpdate Default;

		private int _countTicks;
		private int _countTicksFixed;
		private int _countTicksLate;

		public int GetTicksCount()
		{
			return _countTicks;
		}

		public ProcessingUpdate()
		{
			var componentUpdate = new GameObject(nameof(ComponentUpdate)).AddComponent<ComponentUpdate>();
			componentUpdate.Setup(this);
		}

		public void Add(object updateble)
		{
			switch (updateble)
			{
				case ITick _:
					_ticks.Add(updateble as ITick);
					break;
				case ITickFixed _:
					_ticksFixed.Add(updateble as ITickFixed);
					break;
				case ITickLate _:
					_ticksLate.Add(updateble as ITickLate);
					break;
			}

			_countTicks = _ticks.Count;
			_countTicksFixed = _ticksFixed.Count;
			_countTicksLate = _ticksLate.Count;
		}

		public void Remove(object updateble)
		{
			switch (updateble)
			{
				case ITick _:
					_ticks.Remove(updateble as ITick);
					break;
				case ITickFixed _:
					_ticksFixed.Remove(updateble as ITickFixed);
					break;
				case ITickLate _:
					_ticksLate.Remove(updateble as ITickLate);
					break;
			}

			_countTicks = _ticks.Count;
			_countTicksFixed = _ticksFixed.Count;
			_countTicksLate = _ticksLate.Count;
		}

		public void Tick()
		{
			for (var i = 0; i < _countTicks; i++)
			{
				_ticks[i].Tick();
			}
		}

		public void TickFixed()
		{
			for (var i = 0; i < _countTicksFixed; i++)
				_ticksFixed[i].TickFixed();
		}

		public void TickLate()
		{
			for (var i = 0; i < _countTicksLate; i++)
				_ticksLate[i].TickLate();
		}

		public void Dispose()
		{
			_countTicks = 0;
			_countTicksFixed = 0;
			_countTicksLate = 0;
			_ticks.RemoveAll(t => t is IKernel == false);
			_ticksFixed.Clear();
			_ticksLate.Clear();

			_countTicks = _ticks.Count;

		}
	}
}