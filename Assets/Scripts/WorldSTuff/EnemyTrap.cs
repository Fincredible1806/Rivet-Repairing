using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrap : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float timeBetweenUses;
    [SerializeField] float timePassed;
    [SerializeField] bool readyToUse;
    [SerializeField] KeyCode trapButton;

    [Header("References")]
    [SerializeField] GameObject infoPanel;
    [SerializeField] AudioClip soundClip;
    [SerializeField] string playerTag;
    [SerializeField] Animator trapAnimator;
    [SerializeField] string animClip;
    [SerializeField] GameObject resetParticles;


    private void Start()
    {
        timePassed = timeBetweenUses;
    }
    void Update()
    {
        if(timePassed <  timeBetweenUses && !readyToUse)
        {
            timePassed += Time.deltaTime;
            if(!resetParticles.activeInHierarchy)
            {
                resetParticles.SetActive(true);
            }
        }

        if (timePassed >= timeBetweenUses)
        {
            readyToUse = true;
            resetParticles.SetActive(false);
        }

        if (Input.GetKeyDown(trapButton) && infoPanel.activeInHierarchy && readyToUse) 
        {
            Debug.Log("Yippee");
            ActivateTrap();
        }
    }

    private void ActivateTrap()
    {
        Debug.Log("Activated");
        readyToUse = false;
        if(soundClip != null)
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position); 
        }
        trapAnimator.SetTrigger(animClip);
        timePassed = 0f;
        infoPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(readyToUse && other.CompareTag(playerTag))
        {
            infoPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            infoPanel.SetActive(false);
        }
    }
}
