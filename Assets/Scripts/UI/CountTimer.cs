using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountTimer : MonoBehaviour
{
    public bool outOfTime;
    public float currentTime = 0f;
    public float startingTime = 10f;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float resetTime;
    public bool isResetting;
    public float timePassed = 0;
    [SerializeField] string repairMessage = "Rebooting";

    private void Start()
    {
        currentTime = startingTime;
        isResetting = false;
        outOfTime = false;
    }

    private void Update()
    {
        if(isResetting == true)
        {
            ReEnableTimer();
        }
        
        if (currentTime >= 0 && !outOfTime)
        {
            currentTime -= Time.deltaTime;
                if (countdownText != null)
                    {
                        countdownText.text = currentTime.ToString("F1");
                    }
        }
    }

    private void ReEnableTimer()
    {
        outOfTime = true;

        if (outOfTime && timePassed <= resetTime)
        {
            Debug.Log("restarting");
            timePassed += Time.deltaTime;
            if(countdownText != null)
            {
                countdownText.text = repairMessage;
            }

        }
        else if(outOfTime && timePassed >= resetTime)
        {
            Debug.Log("all fixed!");
            currentTime = startingTime;
            outOfTime = false;
            isResetting = false;
            timePassed = 0;
        }
    }


}

