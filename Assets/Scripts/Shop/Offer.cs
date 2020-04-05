using System.Collections;
using System.Collections.Generic;
using OldSellswords;
using UnityEngine;

public class Offer : MonoBehaviour {
    
    Goods goods;
    List<Item> items;

    public UnityEngine.UI.Image sellItemImage;
    public UnityEngine.UI.Text sellItemName;
    public UnityEngine.UI.Text sellItemCount;
    public UnityEngine.UI.Image buyItemImage;
    public UnityEngine.UI.Text buyItemName;
    public UnityEngine.UI.Text buyItemCount;
    public UnityEngine.UI.Button offerButton;

    //private void Start() {
    //    offerButton.onClick.AddListener(clickOfferBtn);

    //    items = Variables.playerItems.items;
    //}

    //private void Update() {
    //    // включаем и выключаем кнопку обмена в зависимости от условий
    //    bool btnOn = false;
    //    for (int i = 0; i < items.Count; i++) {
    //        if (items[i].loot == goods.sellItem.loot && items[i].count >= goods.sellItem.count) {
    //            btnOn = true;
    //            break;
    //        }
    //    }
    //    if (btnOn != offerButton.interactable) {
    //        offerButton.interactable = btnOn;
    //    }
    //}

    public void setGoods(Goods goods) {
        this.goods = goods;
        sellItemImage.sprite = goods.sellItem.loot.sprite;
        sellItemName.text = goods.sellItem.loot.name;
        sellItemCount.text = goods.sellItem.count.ToString();
        buyItemImage.sprite = goods.buyItem.loot.sprite;
        buyItemName.text = goods.buyItem.loot.name;
        buyItemCount.text = goods.buyItem.count.ToString();
    }

    //public void clickOfferBtn() {
    //    // совершаем сделку
    //    Variables.playerItems.makeOffer(goods.sellItem, goods.buyItem);
    //}

    [System.Serializable]
    public struct Goods {
        public LootItem.Stack sellItem;
        public LootItem.Stack buyItem;
    }
}
