using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int maxZombies = 10;
    private Transform[] spawnPoints;
    private int currentZombieCount = 0;


    private void Start(){
        spawnPoints = new Transform[transform.childCount];

        for(int i = 0; i<transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        InvokeRepeating(nameof(SpawnZombie), spawnInterval, spawnInterval);
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


    public void decrementZCount()
    {
        if(currentZombieCount > 0){
            currentZombieCount--;
        }
    }
}
