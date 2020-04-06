using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Sellswords
{    // скрипт должен быть на слоте
    public class SlotUI : ButtonUi
    {
        public Image _icon;
        TestItems _item; //класс ивентарь
        public TextMeshProUGUI _lvlText;
        public void AddItem(TestItems _newItem)// добавляем инвентарь
        {
            _item = _newItem;
            _icon.sprite = _item.icon;
            _lvlText.transform.GetComponent<TextMeshProUGUI>().text = _item.count.ToString();
            _icon.enabled = true;
        }

    }
}
