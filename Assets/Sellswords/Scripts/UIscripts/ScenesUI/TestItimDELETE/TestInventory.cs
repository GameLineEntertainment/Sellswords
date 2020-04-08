using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestInventory : MonoBehaviour
{
    
    public static TestInventory instance;
    

    void Awake()
    {
       
            
        instance = this;
    }
    public List<TestItems> testItems = new List<TestItems>();

}
