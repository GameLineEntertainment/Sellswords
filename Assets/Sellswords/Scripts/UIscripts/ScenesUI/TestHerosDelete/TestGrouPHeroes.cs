using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrouPHeroes : MonoBehaviour
{
    public TestHeroes SelectedCharacter;
    public static TestGrouPHeroes testGrouPHeroes;

    void Awake()
    {
        testGrouPHeroes = this;
    }
    public List<TestHeroes> testHeroes = new List<TestHeroes>();
}
