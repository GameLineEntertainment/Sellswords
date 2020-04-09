using TMPro;
using UnityEngine;


namespace Sellswords
{
    public class StatPanel : MonoBehaviour
    {
        public TextMeshProUGUI PlaceNameCharacter;/// поле для вывода Name активного героя
        public TextMeshProUGUI PlaceLvLCharacter;/// поле для вывода LvL активного героя
        public TextMeshProUGUI MaxHp;
        public TextMeshProUGUI CurrentHpText;
        public TextMeshProUGUI Ability;
        public TextMeshProUGUI Damage; 
        public  void ShowStats(TestGrouPHeroes testGrouPHeroes)
        {            
            CurrentHpText.text = testGrouPHeroes.SelectedCharacter.HP.ToString();//вывод на панель стат
            Damage.text = testGrouPHeroes.SelectedCharacter._damage.ToString();//вывод на панель стат
            PlaceLvLCharacter.text = testGrouPHeroes.SelectedCharacter.LVL.ToString();
            PlaceNameCharacter.text = testGrouPHeroes.SelectedCharacter.name;
            Ability.text = testGrouPHeroes.SelectedCharacter._ability;//вывод на панель стат
        }    
    }
}
