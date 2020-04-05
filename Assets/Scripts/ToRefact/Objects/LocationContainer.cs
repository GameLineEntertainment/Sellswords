using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct LocationContainer
{
    public string Name;
    public string ShortInfo;
    public string Info;
    public int SceneNumber;
    public LootItem[] Loot;
    public EnemyContainer[] Enemy;
}
