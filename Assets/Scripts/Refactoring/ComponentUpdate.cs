using UnityEngine;

namespace OldSellswords
{
	public class ComponentUpdate : MonoBehaviour
	{
		private ProcessingUpdate _mngUpdate;

		public void Setup(ProcessingUpdate mngUpdate)
		{
			_mngUpdate = mngUpdate;
		}

		private void Update()
		{
			_mngUpdate.Tick();
		}

		private void FixedUpdate()
		{
			_mngUpdate.TickFixed();
		}

		private void LateUpdate()
		{
			_mngUpdate.TickLate();
		}

		private void OnDestroy()
		{
			_mngUpdate = null;
		}
	}
}