using System.Collections.Generic;


namespace OldSellswords
{
    public sealed class AssetsPathUI
    {
        public struct UIPath
        {
            public string Popup;
            public Dictionary<ScreenElementType, string> elements;
        }



        #region Fields

        public static readonly Dictionary<StateUI, UIPath> Popups = new Dictionary<StateUI, UIPath>
        {
            {
                StateUI.Building, new UIPath
                {
                    Popup = "GUI/GUI_Building",
                    elements = new Dictionary<ScreenElementType, string>
                    {
                        { ScreenElementType.HeroBuildingInfo, "GUI/GUI_Building_Hero_Slots" }
                    }
                }
            },
        };

        #endregion
    }
}
