using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] public GameObject enemyPrefab;
    
    [Header("Generation Specifics")]
    [SerializeField] static private float respawnTimer = 5.0f;
    [SerializeField] private int numMobs = 5;

    [Header("Generation Location")]
    [SerializeField] private float[] possibleSpawnX;
    [SerializeField] private float[] possibleSpawnY;

    private float decayingRespawnTimer = respawnTimer;
    int generationLoc;


    void Update()
    {
        decayingRespawnTimer -= Time.deltaTime;
        //If its time for the monsters to spawn
        if(decayingRespawnTimer <= 0)
        {
            int currentChildCount = transform.childCount;
            //Generate the number of mobs.
            for (int i =0; i<(numMobs- currentChildCount); i++)
            {
                //Randomly determine where the mobs will spawn, based on the information passed in via the serializefield
                generationLoc = getRandomInt();
                //Debug.Log("Spawned " + i + " at: " + generationLoc + "which has Y: " + possibleSpawnY[generationLoc]);
                Vector2 spawnPos = new Vector2(possibleSpawnX[generationLoc], possibleSpawnY[generationLoc]);
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                newEnemy.transform.SetParent(transform);



            }
            //Reset respawn timer
            decayingRespawnTimer = respawnTimer;
        }
    }

    int getRandomInt()
    {
        return Random.Range(0, possibleSpawnX.Length);
    }

}
