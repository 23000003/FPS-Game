using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject boss;
    private Transform[] spawnPoints;
    private float spawnInterval = 4f;
    private int maxZombies = 15;
    private int currentZombieCount = 0;


    private void Start()
    {

        Instance = this;

        spawnPoints = new Transform[transform.childCount];

        for(int i = 0; i<transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        InvokeRepeating(nameof(SpawnZombie), spawnInterval, spawnInterval);
    }


    public void IncreaseDifficulty() // the InvokeR in start is also still active so this is just adding another InvokeR
    {
        maxZombies = 20;
        InvokeRepeating(nameof(SpawnZombie), spawnInterval - 1f, spawnInterval - 1f);
    }

    private void SpawnZombie()
    {
        if (currentZombieCount >= maxZombies)
        {
            return;
        }

        int randomID = Random.Range(0, spawnPoints.Length);
        Instantiate(zombie, spawnPoints[randomID].transform.position, spawnPoints[randomID].transform.rotation);
        currentZombieCount++;
    }

    public void SpawnBoss()
    {
        Instantiate(boss, spawnPoints[2].transform.position, spawnPoints[2].transform.rotation);
    }

    public void DecrementZCount()
    {
        if(currentZombieCount > 0){
            currentZombieCount--;
        }
    }
}
