using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldSellswords
{
    [System.Serializable]
    public struct CharacterContainer //переделать в класс, с логикой save load
    {
        public string Name;
        public ColorGroup Color;
        public GameObject PrefabPlayable;
        public GameObject PrefabPreview;
        public Sprite Icon;
        public Sprite Portrait;
        public int Level;
        public int Health;
        public string InfoChar;
        public int SkillPoints, ActiveSkill;
        public SkillContainer[] Skill;
        // Должно быть поле, что данный чар выбран
    }
}

