using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject wolf;
    [SerializeField] private GameObject boss;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int maxZombies = 10;
    [SerializeField] private int maxWolf = 5;
    private Transform[] spawnPoints;
    private int currentZombieCount = 0;
    private int currentWolfCount = 0;


    private void Start(){
        spawnPoints = new Transform[transform.childCount];

        for(int i = 0; i<transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        InvokeRepeating(nameof(SpawnZombie), spawnInterval, spawnInterval);
        InvokeRepeating(nameof(SpawnWolf), spawnInterval, spawnInterval);
    }

    private void SpawnZombie()
    {
        if (currentWolfCount >= maxWolf)
        {
            return;
        }

        int randomID = Random.Range(0, spawnPoints.Length);
        Instantiate(wolf, spawnPoints[randomID].transform.position, spawnPoints[randomID].transform.rotation);
        currentWolfCount++;
    }

    private void SpawnWolf()
    {
        if (currentZombieCount >= maxZombies)
        {
            return;
        }

        int randomID = Random.Range(0, spawnPoints.Length);
        Instantiate(zombie, spawnPoints[randomID].transform.position, spawnPoints[randomID].transform.rotation);
        currentZombieCount++;
    }
    private void SpawnBoss()
    {
        Instantiate(boss, spawnPoints[2].transform.position, spawnPoints[2].transform.rotation);
    }

    public void decrementZCount()
    {
        if(currentZombieCount > 0){
            currentZombieCount--;
        }
    }
}
