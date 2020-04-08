using UnityEngine.UI;
using TMPro;


namespace Sellswords
{
    public class CharacterAvatar : ButtonUi
    {
        public HpBar _hpBar;
        public Image _icon;
        TestHeroes _heroe;// класс героя
        public TextMeshProUGUI LvlText;
        public void AddAvatar(TestHeroes character)// добавляем аватар героя
        {
            
            _heroe = character;
            _icon.sprite = _heroe.icon;
            LvlText.transform.GetComponent<TextMeshProUGUI>().text = _heroe.LVL.ToString();
            _icon.enabled = true;
        }
    }
}
