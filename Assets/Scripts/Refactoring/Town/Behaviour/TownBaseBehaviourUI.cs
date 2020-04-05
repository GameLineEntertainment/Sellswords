using UnityEngine;


namespace OldSellswords
{
    public abstract class TownBaseBehaviourUI : MonoBehaviour
    {
        protected TownUI _townUi;
        private void Awake()
        {
            _townUi = new TownUI();
        }
    }
}
