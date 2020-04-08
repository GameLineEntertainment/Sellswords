using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "TestHeroes/Item")]
public class TestHeroes : ScriptableObject
{

    public int LVL;
    new public string name = "New Item";    // Name of the item
    public Sprite icon = null;
    // Item icon
    public int HP;
}
