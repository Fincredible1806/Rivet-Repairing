using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShower : MonoBehaviour
{
    [Header("Vars and Refs")]
    [SerializeField] GameObject healParticles;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Transform particleSpawnLocation;
    [SerializeField] int healValue;
    private GameObject spawnedParticles;
    [SerializeField] private float useTime;
    [SerializeField] private float timePassed;
    private bool isRebooting;

    private void Update()
    {
        if(isRebooting)
        {
            timePassed += Time.deltaTime;
            Debug.Log("Time Passed " + timePassed);
            if(timePassed >= useTime)
            {
                timePassed = 0;
                isRebooting = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isRebooting)
        spawnedParticles = Instantiate(healParticles, particleSpawnLocation);
        playerHealth.TakeDamage(-healValue);
        Destroy(spawnedParticles, 100 * Time.deltaTime);
        isRebooting = true;
    }

}
