using UnityEngine;

namespace OldSellswords
{
    [CreateAssetMenu(fileName = "enemy", menuName = "Scriptable Object/Data/Enemy")]
    public sealed class Enemy : BaseData
    {
        public TypeEnemy Type;
        public ColorGroup ColorGroup;
        public Item[] Items;
    }
}