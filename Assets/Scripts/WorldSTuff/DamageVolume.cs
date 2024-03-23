using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int damageToPlayer;
    [SerializeField] private int damageToEnemy;
    private PlayerHealth health;
    [SerializeField] private string enemyTag;
    [SerializeField] private string playerTag;

    private void Start()
    {
        health = GameObject.FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            health.TakeDamage(damageToPlayer);
        }
        if(other.CompareTag(enemyTag)) 
        { 
            other.GetComponent<EnemyAiController>().TakeDamage(damageToEnemy);
        }
    }
}
