using UnityEngine.UI;
using UnityEngine;
using TMPro;


namespace Sellswords
{
    public class CharacterAvatar : ButtonUi
    {
        public TextMeshProUGUI LvlText;
        public HpBar HpBar;
        public Image Icon;
        public TestHeroes Heroe;// класс героя ФЕктивный       
        public void AddAvatar(TestHeroes character)// добавляем аватар героя
        {             
            Heroe = character;
            Icon.sprite = Heroe.icon;
            LvlText.transform.GetComponent<TextMeshProUGUI>().text = Heroe.LVL.ToString();
            Icon.enabled = true;
        }        
        public void ShowCharacter()/// переделать под пулобъектов
        {           
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            TestGrouPHeroes.testGrouPHeroes.SelectedCharacter = Heroe;            
            Instantiate(Heroe._prefab, CampScene.Intance.CharacterRotation).SetActive(true);            
        }       
    }
}
