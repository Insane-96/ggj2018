using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    public GameObject key;
    public Transform[] spawnPoints;

    void Start()
    {
        int random = Random.Range(0, spawnPoints.Length - 1);
        Instantiate(key, spawnPoints[random]);
    }
}
