using UnityEngine.UI;
using UnityEngine;


namespace Sellswords
{
    public class HpBar : ButtonUi
    {
        private Slider _slider;
        public float _hp;
        private void Start()
        {
            GetImage.fillAmount = _hp / 100;
            if (_hp > 67)
            {
                GetImage.color = Color.green;
            }
            else if (_hp > 34)
            {
                GetImage.color = Color.yellow;
            }
            else
            {
                GetImage.color = Color.red;
            }
        }
    }
}
