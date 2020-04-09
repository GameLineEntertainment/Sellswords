using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sellswords
{
    public class ScrollColorCharacters : MonoBehaviour
    {
        [SerializeField] private CharacterAvatar _selectCharacterAvatar;
        [SerializeField] private GameObject _charactersScroll;
        public void ShowCharacterPanel()/// переделать под пулобъектов по цвету
        {
            if (TestGrouPHeroes.testGrouPHeroes.SelectedCharacter/*.id ==_selectCharacterAvatar.Heroe.id*/)// активный герой сделать по цвету
            {
                _charactersScroll.SetActive(true);
                _selectCharacterAvatar.AddAvatar(TestGrouPHeroes.testGrouPHeroes.SelectedCharacter);
            }
            else
            {
                
            }

        }
    }
}