using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SkillContainer
{
    public string Name;
    public bool isLock;
    public Sprite Icon;
    public string Info;
    public int Price;
    public LootItem.Stack[] LootPrice;
    public MonoBehaviour SkillScript;
}