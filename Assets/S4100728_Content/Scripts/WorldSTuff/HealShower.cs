using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShower : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] string playerTag;
    [SerializeField] int healValue;

    [Header("Heal Pad References and Variables")]
    [SerializeField] GameObject healParticles;
    [SerializeField] GameObject healLights;
    [SerializeField] Transform particleSpawnLocation;
    private GameObject spawnedParticles;
    [SerializeField] private float useTime;
    [SerializeField] private float timePassed;
    private bool isRebooting;
    [SerializeField] AudioClip healSprayAudio;
    [SerializeField] AudioClip fullHealthAudio;

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
                healLights.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isRebooting && playerHealth.health < playerHealth.fullHealth && other.CompareTag(playerTag))
        {
            spawnedParticles = Instantiate(healParticles, particleSpawnLocation);
            AudioSource.PlayClipAtPoint(healSprayAudio, transform.position);
            playerHealth.TakeDamage(-healValue);
            Destroy(spawnedParticles, 100 * Time.deltaTime);
            isRebooting = true;
            healLights.SetActive(false);
        }
        
        if(!isRebooting && playerHealth.health >= playerHealth.fullHealth && other.CompareTag(playerTag))
        {
            AudioSource.PlayClipAtPoint(fullHealthAudio, transform.position);
        }
    }

}
