using System.Collections;
using System.Collections.Generic;
using Sellswords;
using UnityEngine;
using OldSellswords;

[RequireComponent(typeof(MiniEnemyAI))]
public class EnemyDrop : MonoBehaviour {

    public ChestController chestPrefab;
    private MiniEnemyAI _ai;

    private void Awake()
    {
        _ai = GetComponent<MiniEnemyAI>();
    }

    //  спавн сундука с предметами с определенным шансом
    public void dropItems() {
        if(chestPrefab != null) {
            // получаем лут выпадаемый с монстра по его шансу
            List<Item> dropItems = new List<Item>();
            for (int i = 0; i < _ai.EnemyDate.Items.Length; i++) {
                if (_ai.EnemyDate.Items[i].Chance > 0 && Random.Range(0f, 100f) <= _ai.EnemyDate.Items[i].Chance)
                {
                    dropItems.Add(_ai.EnemyDate.Items[i]);
                }
            }
            // спавним сундук с лутом
            if (dropItems.Count > 0) {
                var chest = Instantiate(chestPrefab, transform.position, chestPrefab.transform.rotation);
                chest.addItems(dropItems.ToArray());
            }
        }
    }

    [System.Serializable]
    public class Drop
    {
        public LootItem.Stack item;
        public float chance;
    }    
}