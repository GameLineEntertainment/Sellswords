using UnityEngine.UI;
using UnityEngine;


namespace Sellswords
{
    public class HpBar : ButtonUi
    {
        [Range( 0,  100)]
        public float _yellowLineCount;// количество после которого линия жизни меняет цвет
        [Range(0, 100)]
        public float _redLineCount;
        [HideInInspector]
        public float _hp;
        private Slider _slider;
        private void Start()
        {
            GetImage.fillAmount = _hp / 100;
            if (_hp > _yellowLineCount)
            {
                GetImage.color = Color.green;
            }
            else if (_hp > _redLineCount)
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
