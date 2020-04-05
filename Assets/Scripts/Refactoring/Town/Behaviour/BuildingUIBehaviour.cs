using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OldSellswords
{
    public sealed class BuildingUIBehaviour : TownBaseUI
    {
        [SerializeField] private Button _exit;
        [SerializeField] private Button _improve;
        [SerializeField] private Button _comeIn;
        [SerializeField] private TextMeshProUGUI _name;
        private BuildingData _buildingData;
          

        private void OnEnable()
        {
            _exit.onClick.AddListener(Hide);
        }
        
        private void OnDisable()
        {
            _exit.onClick.RemoveListener(Hide);
        }
        
        
        public override void Initialization<T>(T data)
        {
            _buildingData = data as BuildingData;
            _name.text = _buildingData.Name;
        }

        public override void Show()
        {   
            gameObject.SetActive(true);         
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
