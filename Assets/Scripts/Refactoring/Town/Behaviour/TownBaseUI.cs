using UnityEngine;

namespace OldSellswords
{
    public abstract class TownBaseUI : TownBaseBehaviourUI
    {
        public abstract void Show();
        public abstract void Hide();
        public abstract void Initialization<T>(T data) where T : BaseData;  
    }
}
