namespace OldSellswords
{
    public sealed class TownUI
    {
        private TownBaseUI _currentWindow;
        private readonly UIFactory _uiFactory;

        public TownUI()
        {
            _uiFactory = new UIFactory();
        }

        public void Execute(StateUI stateUI, BaseData baseData)
        {
            if (_currentWindow != null)
            {
                _currentWindow.Hide();
            }

            switch (stateUI)
            {
                case StateUI.None:
                    break;

                case StateUI.City:
                    break;

                case StateUI.Building:
                    _currentWindow = _uiFactory.GetBuildingUI(baseData as BuildingData);
                    break;

                default:
                    break;
            }

            _currentWindow.Show();
           
        }
    }
}
