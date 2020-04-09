using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "TestHeroes/Item")]
public class TestHeroes : ScriptableObject
{
    public int id;
    public GameObject _prefab;
    public int LVL;
    new public string name = "New Item";    // Name of the item
    public Sprite icon = null;  
    public int HP;
    public string _ability;
    public int _damage;

}
