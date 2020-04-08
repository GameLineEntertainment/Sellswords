using UnityEngine;
using UnityEngine.UI;


namespace Sellswords
{    
    public class ButtonUi : MonoBehaviour, IControlText, IControlImage
    {
        private Text _text;
        private Button _control;
        private Image _image;
        public Text GetText
        {
            get
            {
                if (!_text)
                {
                    _text = transform.GetComponentInChildren<Text>();
                }
                return _text;
            }
        }
        public Button GetControl
        {
            get
            {
                if (!_control)
                {
                    _control = transform.GetComponentInChildren<Button>();
                }
                return _control;
            }
        }
        public void SetInteractable(bool value)
        {
            GetControl.interactable = value;
        }
        public GameObject Instance => gameObject;
        public Selectable Control => GetControl;
        public Image GetImage
        {
            get
            {
                if (!_image)
                {
                    _image = transform.GetComponentInChildren<Image>();
                }
                return _image;
            }
        }        
    }
}
