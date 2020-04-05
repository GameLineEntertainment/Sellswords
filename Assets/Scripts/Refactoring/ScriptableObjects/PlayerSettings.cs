using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldSellswords
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Object/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField]
        private CharacterContainer RedCharacter;
        [SerializeField]
        private CharacterContainer GreenCharacter;
        [SerializeField]
        private CharacterContainer BlueCharacter;

        public Characters RefToCharacters;

        public int PlayerMoney;

        public int Money,
            Experience,
            Level,
            KilledRedMonsters,
            KilledGreenMonsters,
            KilledBlueMonsters,
            MissionsComplete;

        public Loot[] PlayerLoot;

        public void CheckEmptyCharacters()
        {
            CharacterContainer localChar;

            for (int i = 0; i <= RefToCharacters.GameCharacters.Length; i++)
            {
                if (i == RefToCharacters.GameCharacters.Length)
                {
                    localChar = GetRandomCharacter(ColorGroup.Red);
                    RedCharacter = localChar;
                    break;
                }
                if (RedCharacter.Name == RefToCharacters.GameCharacters[i].Name) break;
            }

            for (int i = 0; i <= RefToCharacters.GameCharacters.Length; i++)
            {
                if (i == RefToCharacters.GameCharacters.Length)
                {
                    localChar = GetRandomCharacter(ColorGroup.Green);
                    GreenCharacter = localChar;
                    break;
                }
                if (GreenCharacter.Name == RefToCharacters.GameCharacters[i].Name) break;
            }

            for (int i = 0; i <= RefToCharacters.GameCharacters.Length; i++)
            {
                if (i == RefToCharacters.GameCharacters.Length)
                {
                    localChar = GetRandomCharacter(ColorGroup.Blue);
                    BlueCharacter = localChar;
                    break;
                }
                if (BlueCharacter.Name == RefToCharacters.GameCharacters[i].Name) break;
            }
        }

        /// <summary>
        /// Возвращает случайного персонажа из всех существующих в Characters заданного цвета (charColor).
        /// </summary>
        /// <param name="charColor">Цвет случайного персонажа</param>
        /// <returns></returns>
        public CharacterContainer GetRandomCharacter(ColorGroup charColor)
        {
            if (RefToCharacters == null)
            {
                Debug.LogError("Не указан Characters" + RefToCharacters);
            }

            var allChars = RefToCharacters.GameCharacters;
            CharacterContainer[] localChars = GetAllCharsByColor(charColor, RefToCharacters.GameCharacters);

            int rnd = Random.Range(0, localChars.Length);

            return localChars[rnd];
        }

        public CharacterContainer[] GetAllCharsByColor(ColorGroup charColor, CharacterContainer[] allChars)
        {
            List<CharacterContainer> sortedChars = new List<CharacterContainer>();

            switch (charColor)
            {
                case ColorGroup.Red:
                    foreach (CharacterContainer newChar in allChars)
                    {
                        if (newChar.Color == ColorGroup.Red) sortedChars.Add(newChar);
                    }
                    break;

                case ColorGroup.Green:
                    foreach (CharacterContainer newChar in allChars)
                    {
                        if (newChar.Color == ColorGroup.Green) sortedChars.Add(newChar);
                    }
                    break;

                case ColorGroup.Blue:
                    foreach (CharacterContainer newChar in allChars)
                    {
                        if (newChar.Color == ColorGroup.Blue) sortedChars.Add(newChar);
                    }
                    break;
            }

            return sortedChars.ToArray();
        }

        public void SetHero(CharacterContainer character)
        {
            switch (character.Color)
            {
                case ColorGroup.Red:
                    RedCharacter = character;
                    break;

                case ColorGroup.Green:
                    BlueCharacter = character;
                    break;

                case ColorGroup.Blue:
                    GreenCharacter = character;
                    break;
            }

            CheckEmptyCharacters();
        }

        public CharacterContainer GetRedHero()
        {
            return GetRefreshedHero(RedCharacter);
        }

        public CharacterContainer GetGreenHero()
        {
            return GetRefreshedHero(GreenCharacter);
        }

        public CharacterContainer GetBlueHero()
        {
            return GetRefreshedHero(BlueCharacter);
        }

        // Возвращает персонажа с обновлёнными данными
        CharacterContainer GetRefreshedHero(CharacterContainer hero)
        {
            foreach (CharacterContainer newChar in RefToCharacters.GameCharacters)
            {
                if (newChar.Name == hero.Name) return newChar; // если находим совпадение возвращаем перса с новыми данными
            }

            Debug.LogError("Возникла ошибка активных персонажей, проблемный персонаж: " + hero.Name);
            return GetRandomCharacter(hero.Color); // Если совпадений нет, возвращаем случайного перса 
        }
    }
}
