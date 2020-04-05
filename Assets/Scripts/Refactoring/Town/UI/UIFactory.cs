using UnityEngine;

namespace OldSellswords
{
    public sealed class UIFactory
    {
        private BuildingUIBehaviour _buildingUI;
        private Canvas _canvas;


        public UIFactory()
        {
            _canvas = Object.FindObjectOfType<Canvas>();
        }

        public BuildingUIBehaviour GetBuildingUI(BuildingData buildingData)
        {
            if (_buildingUI == null)
            {
                var resources = Resources.Load<BuildingUIBehaviour>(AssetsPathUI.Popups[StateUI.Building].Popup);
                _buildingUI = Object.Instantiate(resources, _canvas.transform.position, Quaternion.identity, _canvas.transform);
            }
            
            _buildingUI.Initialization(buildingData);
            return _buildingUI;
        }
    }
}
