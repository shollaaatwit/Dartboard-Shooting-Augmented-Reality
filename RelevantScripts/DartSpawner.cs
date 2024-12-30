using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartSpawner : MonoBehaviour
{
    public DartLevelManager dartLevelManager;
    public GameObject dart1;
    public GameObject dart2;
    public GameObject dart3;
    public Transform cup;
    void Start()
    {
        dartLevelManager = GameObject.Find("GameManager").GetComponent<DartLevelManager>();
    }
    void Update()
    {
        if(dartLevelManager.gameStarted == true)
        {
            if(CountObjectsWithTag("Dart1") < 1)
            {
                Instantiate(dart1, cup);
            }
            if(CountObjectsWithTag("Dart2") < 1)
            {
                Instantiate(dart2, cup);
            }
            if(CountObjectsWithTag("Dart3") < 1)
            {
                Instantiate(dart3, cup);
            }
        }

    }
    int CountObjectsWithTag(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).Length;
    }
}
