using Sellswords;
using UnityEngine;


namespace OldSellswords
{
    [CreateAssetMenu(fileName = "item", menuName = "Scriptable Object/Data/Item")]
    public sealed class Item : BaseData
    {
        public float Cost;
        public float Chance;
        public TypeItem TypeItem;
        // тут должно быть что то придаст эввект артефакт
    }
}
