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
        public TextMeshProUGUI Count;
        public void AddItem(TestItems newItem)// добавляем инвентарь
        {
            _item = newItem;
            _icon.sprite = _item.icon;
            Count.transform.GetComponent<TextMeshProUGUI>().text = _item.count.ToString();
            _icon.enabled = true;
        }

    }
}
