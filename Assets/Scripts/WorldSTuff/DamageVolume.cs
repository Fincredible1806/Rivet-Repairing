using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    [Header("Variables")]
    public int damageToPlayer;
    public int damageToEnemy;
    private PlayerHealth health;
    [SerializeField] private string enemyTag;
    [SerializeField] private string playerTag;
    public float damageInterval = .5f;
    [SerializeField] private float timeBetweenHits;

    private void Start()
    {
        health = GameObject.FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.CompareTag(playerTag))
        {
            health.TakeDamage(damageToPlayer);
        }
        if (other.CompareTag(enemyTag))
        {
            other.GetComponent<EnemyAiController>().TakeDamage(damageToEnemy);
        }        
    }


    private void OnTriggerStay(Collider other)
    {
        if(timeBetweenHits <= damageInterval)
        {
            if (other.CompareTag(playerTag))
            {
                health.TakeDamage(damageToPlayer);
            }
            if( other.CompareTag(enemyTag))
            {
                Debug.Log("Dealing Enemy Damage");
                other.GetComponent<EnemyAiController>().TakeDamage(damageToEnemy);
            }
            timeBetweenHits = 0;
        }
    }

    private void Update()
    {
        timeBetweenHits += Time.deltaTime;
    }
}
