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

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(healParticles, particleSpawnLocation);
        playerHealth.TakeDamage(-healValue);
    }

}
