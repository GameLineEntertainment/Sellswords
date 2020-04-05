using UnityEngine;


namespace OldSellswords
{
    [CreateAssetMenu(fileName = "Building", menuName = "Scriptable Object/Data/Building/Building")]
    public sealed class BuildingData : BaseData
    {       
        public ColorGroup Color;
        public Item[] Items;
        public Item[] AdditionalItems;
        public long Timer;
        public float BloodLustCoeficient;     
    }
}
