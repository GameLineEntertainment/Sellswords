using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CombatScene
{
    public sealed class Manager : MonoBehaviour
    {
        public UIManager UIElementSettings;
        public SpecialFuncManager SpecialFuncSettings;
        public EnemySpawnManager EnemySpawnSettings;
        public CharManager CharactersSettings;
        public EnemyManager EnemiesSettings;
        public SpellManager SpellsSettings;
    }
}
