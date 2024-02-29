using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepairSite : MonoBehaviour
{
    [Header("References")]

    [SerializeField] GameObject fixCanvas;
    [SerializeField] GameObject repairBar;
    [SerializeField] Slider repairSlider;
    public CountTimer countTimer;

    [Header("Keybinds")]

    [SerializeField] KeyCode fixKey;

    [Header("Variables")]

    [SerializeField] string playerTag;
    [SerializeField] float fixingTime;
    [SerializeField] float fullFixMultiplier;
    float halfFixMultiplier;
    float shortFixTime;
    float fixTimeTaken;
    bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {
        repairBar.SetActive(false);
        shortFixTime = fixingTime / 2;
        halfFixMultiplier = fullFixMultiplier * 2;
    }

    // Update is called once per frame
    void Update()
    {
        TimeOutChecker();
        if(playerInRange && !countTimer.isResetting && Input.GetKey(fixKey))
        {
            FixObject();
        }

        if(!playerInRange && repairBar.activeSelf == true)
        {
            if (countTimer.outOfTime)
            {
                repairSlider.value = fixTimeTaken * fullFixMultiplier;
            }
            else
            {
                repairSlider.value = fixTimeTaken * halfFixMultiplier;
            }
            fixTimeTaken -= Time.deltaTime / 3;
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
        
        if (countTimer.outOfTime)
        {
            repairBar.SetActive(true);
            FullTimeFix();
        }
        if (!countTimer.outOfTime)
        {
            repairBar.SetActive(true);
            HalfTimeFix();
        }    
    }

    private void FullTimeFix()
    {
        if (fixTimeTaken < fixingTime)
        {
            fixTimeTaken += Time.deltaTime;
            repairSlider.value = fixTimeTaken * fullFixMultiplier;
        }
        else if (fixTimeTaken >= fixingTime)
        {
            repairBar.SetActive(false);
            RepairsComplete();
        }
    }

    

    private void HalfTimeFix()
    {

        if (fixTimeTaken < shortFixTime)
        {
            fixTimeTaken += Time.deltaTime;
            repairSlider.value = fixTimeTaken * halfFixMultiplier;
        }
        else if (fixTimeTaken >= shortFixTime)
        {
            repairBar.SetActive(false);
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
