using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour {
    
    public string description;
    public Sprite sprite;
    public Stack price;

    [System.Serializable]
    public class Stack
    {
        public LootItem loot;
        public int count;

        public Stack copy() {
            Stack copy = new Stack();
            copy.loot = loot;
            copy.count = count;
            return copy;
        }
    }
}
