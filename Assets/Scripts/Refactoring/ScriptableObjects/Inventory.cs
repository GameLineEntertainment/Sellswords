using System.Collections.Generic;
using UnityEngine;

namespace OldSellswords
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Scriptable Object/Level Settings")]
    public class Inventory : ScriptableObject
    {
        public List<Item> dropItems = new List<Item>();
    }
}
