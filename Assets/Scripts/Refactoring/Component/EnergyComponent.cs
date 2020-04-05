using UnityEngine;

namespace OldSellswords
{
	public class EnergyComponent : MonoBehaviour
	{
		public float RestoreSpeed;
		public float TurnCost;
		public int CooldownTime;
		public StateEnergy StateEnergy;
		public CharSpeed[] Speed;

		[Range(0, 10)] public float CurrentEnergy;

		public const float MaxEnergy = 10;

		public bool CDprogress; // Отдыхаем ли в данный момент
		public bool IsRest; // Отдыхаем ли в данный момент

		private void Awake()
		{
			CurrentEnergy = MaxEnergy;
		}
	}

	public enum StateEnergy
	{
		Normal,
		Easy,
		Medium,
		Hard
	}

	[System.Serializable]
	public struct CharSpeed
	{
		//[SerializeField]
		//string Name;
		public float MoveSpeed, CastSpeed;
	}
}