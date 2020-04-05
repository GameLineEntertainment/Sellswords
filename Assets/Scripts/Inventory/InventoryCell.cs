using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : MonoBehaviour {
    
    public UnityEngine.UI.Image uiImage;
    public UnityEngine.UI.Button uiButton;
    public UnityEngine.UI.Text uiCount;

    public LootItem.Stack item;
    Inventory inventory;
    int lastCount;

    private void Update() {
        // обновление колличества эллементов
        if (item != null && item.count != lastCount) {
            uiCount.text = item.count.ToString();
            lastCount = item.count;
        }
    }

    public void setItem(LootItem.Stack item, Inventory inventory) {
        this.item = item;
        this.inventory = inventory;
        uiImage.sprite = item.loot.sprite;
        uiButton.onClick.AddListener(setFocus);

    }

    void setFocus() {
        inventory.cellOnFocus(this);
    }
    
}
