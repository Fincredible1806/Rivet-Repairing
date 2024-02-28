using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RepairSite : MonoBehaviour
{
    public CountTimer countTimer;
    [SerializeField] bool countingDown;
    [SerializeField] string playerTag;
    [SerializeField] GameObject fixCanvas;
    [SerializeField] float fixingTime;
    [SerializeField] float shortFixTime;
    [SerializeField] float fixTimeTaken;
    [SerializeField] bool playerInRange;
    [SerializeField] KeyCode fixKey;
    // Start is called before the first frame update
    void Start()
    {
        shortFixTime = fixingTime / 2;
    }

    // Update is called once per frame
    void Update()
    {
        TimeOutChecker();
        if(playerInRange && Input.GetKey(fixKey))
        {
            FixObject();
        }
    }

    private void TimeOutChecker()
    {
        if (countTimer.currentTime <= 0)
        {
            countTimer.outOfTime = true;
        }
    }

    public void FixObject()
    {
        if (countTimer.outOfTime == true)
        {
            FullTimeFix();
        }
        if (countTimer.outOfTime == false)
        {
            HalfTimeFix();
        }    
    }

    private void FullTimeFix()
    {
        if (fixTimeTaken < fixingTime)
        {
            fixTimeTaken += Time.deltaTime;
        }
        else if (fixTimeTaken >= fixingTime)
        {
            RepairsComplete();
        }
    }

    

    private void HalfTimeFix()
    {
        if (fixTimeTaken < shortFixTime)
        {
            fixTimeTaken += Time.deltaTime;
        }
        else if (fixTimeTaken >= shortFixTime)
        {
            RepairsComplete();
        }
    }

    private void RepairsComplete()
    {
        fixCanvas.SetActive(false);
        fixTimeTaken = 0;
        countTimer.isResetting = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            fixCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
            fixCanvas.SetActive(false);
        }
    }
}
