using System.Collections.Generic;
using Sellswords;
using UnityEngine;

namespace OldSellswords
{
    [CreateAssetMenu(fileName = "mission_generator", menuName = "Scriptable Object/Data/MissionGenerator")]
    public sealed class MissionGenerator : ScriptableObject
    {
        [SerializeField] private int _countEnemy;
        [SerializeField] private List<Arena> _arenas;
        [SerializeField] private bool _isComplited;

        public Dictionary<Arena, List<Enemy>> DataArens { get; } =
            new Dictionary<Arena, List<Enemy>>();

        public void Generator(Arena arena)
        {
            var listEnemies = new List<Enemy>();
            var rand = Random.Range(5, 20);
            for (int i = 0; i < rand; i++)
            {
                var enemy = Instantiate(arena.Enemies[Random.Range(0, arena.Enemies.Length)]);

                var color = Random.Range(0, 3);
                enemy.ColorGroup = (ColorGroup)color;
                listEnemies.Add(enemy);
            }

            if (!DataArens.ContainsKey(arena))
            {
                DataArens.Add(arena, listEnemies);
            }
        }

        public void Clear()
        {
            DataArens.Clear();
        }
    }
}
