using System.Collections;
using System.Collections.Generic;
using OldSellswords;
using UnityEngine;

public class ChestController : MonoBehaviour {

    public Item[] items;
    public float autoGetTime;
    float liveTime = 0;

	// Update is called once per frame
	void Update () {
        // автоматическое подбирание предмета через autoGetTime
        liveTime += Time.deltaTime;
        if (liveTime >= autoGetTime) sendItemsToReward();
    }

    // подбирание предмета по клику
    private void OnMouseUpAsButton() {
        sendItemsToReward();
    }

    // добавление предметов в сундук
    public void addItems(Item[] items) {
        this.items = items;
    }

    // подбирание предмета
    public void sendItemsToReward() {
        if (items != null && items.Length > 0) FindObjectOfType<GameLevel>().addRewardItems(items);
        Destroy(gameObject);
    }
}