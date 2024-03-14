using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy References")]
    [SerializeField] GameObject baseEnemy;
    [SerializeField] GameObject strongerEnemy;
    [SerializeField] GameObject strongestEnemy;
    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float chooser;
    [SerializeField] private float lowEnemyNum;
    [SerializeField] private float strongerEnemyNum;
    [SerializeField] private float strongestEnemyNum;
    [SerializeField] private float spawnChance;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(timePassed >= timeBetweenSpawns)
        {
            SpawnChance();
        }


    }

    private void SpawnChance()
    {
        float willSpawn = Random.Range(0f, 1f);
        if (willSpawn < spawnChance)
        {
            SpawnEnemy();
            timePassed = 0;
        }
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
    }


    private void SpawnEnemy()
    {
        chooser = Random.Range(0f, 1f);
        if (chooser >= strongestEnemyNum)
        {
            Instantiate(strongestEnemy, spawnPoint);
        }
        else if (chooser >= strongerEnemyNum)
        {
            Instantiate(strongerEnemy, spawnPoint);
        }
        else
        {
            Instantiate(baseEnemy, spawnPoint);
        }

    }
}
