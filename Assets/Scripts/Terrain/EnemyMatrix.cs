using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMatrix : MonoBehaviour
{
    public int waves;
    public int cWave;
    public int[] enemyCount;
    public GameObject[] enemyList;
    public Transform spawnOrigion;

    private float posiPadding = 5;
    private float spawnPadding = 2; 
    private float spawnY = 5f;
    [SerializeField]private Vector3[] enemyLoc;

    float xLoc;
    float zLoc;
    bool reserved;
    void Awake() 
    {
        cWave = 1;
    }

    public void spawnWave(Vector2 perimeter)
    {
        Vector2 spawnPeri = new Vector2(perimeter.x-posiPadding, perimeter.y-posiPadding);
        //Debug.Log(spawnPeri);
        randomLocation(spawnPeri);
        foreach (Vector3 aLoc in enemyLoc)
        {
            //TODO Add enemy Variaty
            GameObject spawnedEnemy = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity, spawnOrigion);
            spawnedEnemy.transform.localPosition = aLoc;
        }
        cWave+=1;
    }
    void randomLocation(Vector2 spawnPeri)
    {
        //List of enemy Locations
        enemyLoc = new Vector3[enemyCount[cWave-1]];
        //make it all 0 for now
        for (int i = 0; i < enemyCount[cWave-1]; i++)
        {
            enemyLoc[i] = Vector3.zero;
        }

        //Randomize x and z coords + check if reserved + add to reserved w/ padding
        for (int i = 0; i < enemyCount[cWave-1]; i++)
        {
            reserved = true;
            //check if reserved
            while (reserved)
            {
                reserved = false;
                xLoc = Random.Range(posiPadding, spawnPeri.x);
                zLoc = Random.Range(posiPadding, spawnPeri.y);

                // Debug.Log("x" + xLoc);
                // Debug.Log("z" + zLoc);

                foreach (Vector3 rLoc in enemyLoc)
                {
                    float xMin = rLoc.x-spawnPadding;
                    float xMax = rLoc.x+spawnPadding;
                    float zMin = rLoc.z-spawnPadding;
                    float zMax = rLoc.z+spawnPadding;
                    if (xLoc>xMin && xLoc<xMax && zLoc>zMin && zLoc<zMax)
                    {
                        reserved = true;
                        //break;//this may decrease looping time and make the programm faster idk maybe
                    }
                }
            }
            //add to enemy loctation list
            enemyLoc[i] = new Vector3(xLoc, spawnY, zLoc);
        }
    }
}
