using UnityEngine;
using UnityEngine.UI;

namespace OldSellswords
{
	public class EnergyUI : MonoBehaviour
	{
		private Image _spriteEnergy;

		private void Awake()
		{
			_spriteEnergy = GetComponent<Image>();
		}

		public float Fill
		{
			set => _spriteEnergy.fillAmount = value;
		}

		public void SetActive(bool value)
		{
			_spriteEnergy.gameObject.SetActive(value);
		}

		public void SetColor(Color col)
		{
			_spriteEnergy.color = col;
		}
	}
}