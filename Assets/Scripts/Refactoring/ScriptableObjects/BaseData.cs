using UnityEngine;


namespace OldSellswords
{
    public abstract class BaseData : ScriptableObject
    {
        public string Name;
        public string FullDescription;
        public string ShortDescription;
        public Sprite Icon;
    }
}
