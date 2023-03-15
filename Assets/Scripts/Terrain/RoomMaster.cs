using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RoomMaster : MonoBehaviour
{
    [Header("Properties")]
    public Room theRoom;
    public Vector2 dimention;//room dimention (x,z)
    public List<Locker> lockers;//locks the room when entered
    public bool RandomWave;

    [Header("Audio")]
    public AudioSource AS;
    public AudioClip lockdown;
    public AudioClip clear;

    [Header("Room Status")]
    [SerializeField]private bool isEntered;
    [SerializeField]private bool isCleared;
    [SerializeField]private bool roomDone;
    [SerializeField]private bool waveCleared;
    [SerializeField]private bool waveSpawned;
    [SerializeField]public int enemyRem;
    [SerializeField]private int enemyWaves;
    [SerializeField]private int currentWave;

    [Header("Enemies")]
    public int[] enemyCount;//waves x how many enemies each wave
    public GameObject[] enemyList;//list of enemies (have to give each one a difficulty index)
    public Transform spawnParent;//the enemy spawn location will be dependent on this, so place it at 0,0

    [SerializeField] private Vector3[] enemyLoc; //list of enemy locations
    [SerializeField] private int difficulty;

    //Constants
    private float posiPadding = 5f; //padding for the parameter of the map
    private float spawnPadding = 2f; //padding between enemies spawned
    private float spawnY = 7f; //enemy spawn elivation to prevent clipping into the ground, 

    //Variables
    float xLoc;
    float zLoc;
    bool reserved;
    Vector2 perimeterX;//Global position without padding to detect player entry
    Vector2 perimeterZ;
    Transform player;
    void Start() 
    {
        isEntered = false;
        isCleared = false;
        roomDone = false;

        waveCleared = true;
        waveSpawned = false;

        currentWave = 1;

        perimeterX = new Vector2(transform.position.x-(dimention.x/2), transform.position.x+(dimention.x/2));
        perimeterZ = new Vector2(transform.position.z-(dimention.y/2), transform.position.z+(dimention.y/2));

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update() 
    {
        if (!roomDone)
        {
            if (!isEntered)
            {
                //Detect player in perimeter or not, lock when does
                bool isInX = player.position.x>=perimeterX.x && player.position.x<=perimeterX.y;
                bool isInZ = player.position.z>=perimeterZ.x && player.position.z<=perimeterZ.y;
                if (isInX && isInZ)
                {
                    lockRoom();
                    AS.PlayOneShot(lockdown);
                    isEntered = true;
                }
            }
            if (isEntered && !isCleared)
            {
                //Player is in room and room is not cleared, spawn enemies
                if (currentWave <= enemyWaves)
                {
                    if (!waveSpawned && waveCleared)
                    {
                        waveSpawned = true;
                        enemyRem = enemyCount[currentWave-1];//tot enemies spawned
                        //spawn wave of enemies
                        spawnWave();
                        waveCleared = false;
                    }
                    if (waveSpawned && !waveCleared)
                    {
                        GameObject[] remEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                        enemyRem = remEnemies.Length;
                        if (enemyRem == 0)
                        {
                            currentWave+=1;
                            waveCleared = true;
                            waveSpawned = false;
                        }
                    }
                }
                else
                {
                    isCleared = true;
                }

            }
            if (isEntered && isCleared)
            {
                //Player cleared the room
                freeRoom();
                AS.PlayOneShot(clear);
                roomDone = true;
            }
        }
    }
    public void lockRoom()
    {
        foreach (Locker locker in lockers)
        {
            locker.Close();
        }
    }
    public void freeRoom()
    {
        foreach (Locker locker in lockers)
        {
            locker.Open();
        }
    }

    //Enemy Spawn
    public void setDifficulty(int d)
    {
        difficulty = d;
        //! How much difficulty per wave (howmany waves is randomized or set)
        //! Allocate enemies per wave
        //! 1: fodder, add to fill the gaps, 3+: normal enemies 7+: eliete enemies
        //! Scale enemy health and damage to which stage so that the difficulty index is not inflated
    }

    public void spawnWave()
    {
        if (RandomWave)
        {
            randomLocation();
            foreach (Vector3 aLoc in enemyLoc)
            {
                int enemyType = Random.Range(0, enemyList.Length);
                GameObject spawnedEnemy = Instantiate(enemyList[enemyType], Vector3.zero, Quaternion.identity, spawnParent);
                spawnedEnemy.transform.localPosition = aLoc;
            }
        }else
        {
            GameObject spawnedEnemy = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity, spawnParent);
            spawnedEnemy.transform.localPosition = Vector3.zero;
        }
    }
    void randomLocation()
    {
        //List of enemy Locations
        enemyLoc = new Vector3[enemyCount[currentWave-1]];
        //make it all 0 for now
        for (int i = 0; i < enemyCount[currentWave-1]; i++)
        {
            enemyLoc[i] = Vector3.zero;
        }

        //Randomize x and z coords + check if reserved + add to reserved w/ padding
        for (int i = 0; i < enemyCount[currentWave-1]; i++)
        {
            reserved = true;
            //check if reserved
            while (reserved)
            {
                reserved = false;
                xLoc = Random.Range(-dimention.x/2 + posiPadding, dimention.x/2 - posiPadding);
                zLoc = Random.Range(-dimention.y/2 + posiPadding, dimention.y/2 - posiPadding);

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
