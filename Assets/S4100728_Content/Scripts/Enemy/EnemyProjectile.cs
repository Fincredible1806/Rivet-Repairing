using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("The Variables")]
    public int damage;
    [SerializeField] private int lifetime;
    [SerializeField] string playerTag;
    [Header("Reference")]
    private PlayerHealth health;
    float currentTime = 0;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            health = other.gameObject.GetComponent<PlayerHealth>();
            health.TakeDamage(damage);
        }    

    }

    private void Update()
    {
        LifeTime();
    }

    private void LifeTime()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
