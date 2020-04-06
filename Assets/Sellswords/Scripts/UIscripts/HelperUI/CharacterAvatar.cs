using UnityEngine.UI;
using TMPro;


namespace Sellswords
{
    public class CharacterAvatar : ButtonUi
    {
        public HpBar _hpBar;
        public Image _icon;
        TestHeroes _heroe;// класс героя
        public TextMeshProUGUI _lvlText;
        public void AddAvatar(TestHeroes _character)// добавляем аватар героя
        {
            
            _heroe = _character;
            _icon.sprite = _heroe.icon;
            _lvlText.transform.GetComponent<TextMeshProUGUI>().text = _heroe.LVL.ToString();
            _icon.enabled = true;
        }
    }
}
