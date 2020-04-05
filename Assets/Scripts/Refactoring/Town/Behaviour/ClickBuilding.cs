using UnityEngine;
using UnityEngine.EventSystems;


namespace OldSellswords
{
    public sealed class ClickBuilding : TownBaseBehaviourUI, IPointerClickHandler
    {
        [SerializeField] private BuildingData _buildingData;

        public void OnPointerClick(PointerEventData eventData)
        {
            _townUi.Execute(StateUI.Building, _buildingData);
        }
    }
}
