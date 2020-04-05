using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    // масив валют для для определения типа сделки
    public List<LootItem> currencies = new List<LootItem>();
    
    public RectTransform offersRect;
    public GameObject offerPrefab;
    public GameObject titlePrefab;

    Vector2 offset;

    public Offer.Goods[] goodsFromData;

    // Use this for initialization
    void Start () {
        offset = offersRect.rect.center + new Vector2(0, offersRect.rect.height / 2);
        // загрузка сделок
        loadOffers();
    }
	
    void loadOffers() {
        // удаляем старые обьекты деток
        for (int i = offersRect.childCount; i > 0; i--) Destroy(offersRect.GetChild(i - 1).gameObject);
        // загружаем сделки
        List<Offer.Goods> buyGoods = new List<Offer.Goods>();
        List<Offer.Goods> sellGoods = new List<Offer.Goods>();
        List<Offer.Goods> offerGoods = new List<Offer.Goods>();
        for (int i = 0; i < goodsFromData.Length; i++) {
            if(currencies.Contains(goodsFromData[i].sellItem.loot)) {
                if (currencies.Contains(goodsFromData[i].buyItem.loot)) {
                    offerGoods.Add(goodsFromData[i]);
                } else {
                    buyGoods.Add(goodsFromData[i]);
                }
            } else if (currencies.Contains(goodsFromData[i].buyItem.loot)) {
                sellGoods.Add(goodsFromData[i]);
            } else {
                offerGoods.Add(goodsFromData[i]);
            }
        }
        if (buyGoods.Count > 0) addTitle("Купить");
        for (int i = 0; i < buyGoods.Count; i++) addOffer(buyGoods[i]);
        if (sellGoods.Count > 0) addTitle("Продать");
        for (int i = 0; i < sellGoods.Count; i++) addOffer(sellGoods[i]);
        if (offerGoods.Count > 0) addTitle("Обменять");
        for (int i = 0; i < offerGoods.Count; i++) addOffer(offerGoods[i]);
    }

    void addTitle(string title) {
        GameObject go = Instantiate(titlePrefab, offersRect.transform);
        go.GetComponent<UnityEngine.UI.Text>().text = title;
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.localPosition = offset - new Vector2(0, rect.rect.height / 2);
        offset -= new Vector2(0, rect.rect.height);
    }
    void addOffer(Offer.Goods goods) {
        GameObject go = Instantiate(offerPrefab, offersRect.transform);
        go.GetComponent<Offer>().setGoods(goods);
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.localPosition = offset - new Vector2(0, rect.rect.height / 2);
        offset -= new Vector2(0, rect.rect.height);
    }
}
