using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RoomMaster : MonoBehaviour
{
    [Header("Monitor States")]
    [SerializeField]private bool isEntered;
    [SerializeField]private bool isCleared;
    [SerializeField]private bool roomDone;
    [SerializeField]private bool waveCleared;
    [SerializeField]private bool waveSpawned;
    [SerializeField]public int enemyRem;
    [SerializeField]private int enemyWaves;
    [SerializeField]private int currentWave;

    [Header("Custom Stuff")]
    public Transform roomPosi;
    public List<Locker> lockers;
    //*public GameObject Dummy;
    public EnemyMatrix EM;

    [Header("Audio")]
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip lockdownSound;
    [SerializeField] AudioClip downSound;
    [SerializeField] AudioClip reenforceSound;
    [SerializeField] AudioClip unlockSound;
    private bool shouted;


    Vector2 perimeterX;
    Vector2 perimeterZ;
    Transform player;
    void Start() 
    {
        isEntered = false;
        isCleared = false;
        roomDone = false;

        waveCleared = true;
        waveSpawned = false;

        enemyWaves = EM.waves;
        currentWave = 1;

        shouted = false;

        foreach (Locker locker in lockers)
        {
            locker.Open();
        }
        perimeterX = new Vector2(transform.position.x, transform.position.x+roomPosi.position.x);
        perimeterZ = new Vector2(transform.position.z, transform.position.z+roomPosi.position.z);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update() 
    {
        if (!roomDone)
        {
            if (!isEntered)
            {
                //Debug.Log(1);
                //Detect player in perimeter or not, lock when does
                bool isInX = player.position.x>=perimeterX.x && player.position.x<=perimeterX.y;
                bool isInZ = player.position.z>=perimeterZ.x && player.position.z<=perimeterZ.y;
                if (isInX && isInZ)
                {
                    lockRoom();
                    isEntered = true;
                }
            }
            if (isEntered && !isCleared)
            {
                //Debug.Log(2);
                //Debug.Log("CR" + currentWave);
                //Player is in room and room is not cleared, spawn enemies
                if (currentWave <= enemyWaves)
                {
                    if (!waveSpawned && waveCleared)
                    {
                        if (currentWave==1)
                            AS.PlayOneShot(lockdownSound);
                        else
                            AS.PlayOneShot(reenforceSound);
                        //Debug.Log(3);
                        waveSpawned = true;
                        enemyRem = EM.enemyCount[currentWave-1];//tot enemies spawned
                        //spawn wave of enemies
                        doEnemyWave();
                        //*Instantiate(Dummy, new Vector3(85, currentWave, 13), Quaternion.identity, transform);
                        waveCleared = false;
                    }
                    if (waveSpawned && !waveCleared)
                    {
                        //Debug.Log(4);
                        GameObject[] remEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                        enemyRem = remEnemies.Length;
                        //Debug.Log("EnemyRem: " + enemyRem);

                        if (enemyRem==3 && !shouted)
                        {
                            AS.PlayOneShot(downSound);
                            shouted = true;
                        }

                        if (enemyRem == 0)
                        {
                            currentWave+=1;
                            waveCleared = true;
                            waveSpawned = false;

                            shouted = false;
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
                AS.PlayOneShot(unlockSound);
                //Debug.Log(69);
                //Player cleared the room
                freeRoom();
                roomDone = true;
            }
        }
    }
    void lockRoom()
    {
        foreach (Locker locker in lockers)
        {
            locker.Close();
        }
    }
    void freeRoom()
    {
        foreach (Locker locker in lockers)
        {
            locker.gameObject.SetActive(false);
        }
    }

    //Enemies Spawn and stuff
    void doEnemyWave()
    {
        EM.spawnWave(new Vector2(roomPosi.localPosition.x, roomPosi.localPosition.z));
    }
}
