using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{

    public GameObject enemy;
    float randX;
    Vector2 SpawnPoint;
    public float SpawnRate = 1000000f;
    float nextSpawn = 0.0f;
        
   
   // Update is called once per frame
    void Update()
    {
       if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + SpawnRate;
            randX = UnityEngine.Random.Range(-12.0f, 20.0f);
            SpawnPoint = new Vector2(randX, transform.position.y);
            //Instantiate(enemy, SpawnPoint, Quaternion.identity);
        }
        
    }
}
