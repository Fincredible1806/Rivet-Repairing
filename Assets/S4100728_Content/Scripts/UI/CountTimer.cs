using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountTimer : MonoBehaviour
{
    [Header("References")]

    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] Slider recoverySlider;
    [SerializeField] GameObject slider;
    [SerializeField] GameObject downGraphic;

    [Header("Variables")]

    public bool outOfTime;
    public float currentTime = 0f;
    public float startingTime = 10f;
    [SerializeField] float resetTime;
    [SerializeField] float resetBarMultiplier;
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
        if (outOfTime)
        {
            downGraphic.SetActive(true);
        }
        else
        {
            downGraphic.SetActive (false);
        }
    }

    private void ReEnableTimer()
    {
        slider.SetActive(true);
        recoverySlider = slider.GetComponent<Slider>();
        outOfTime = true;

        if (outOfTime && timePassed <= resetTime)
        {
            Debug.Log("restarting");
            timePassed += Time.deltaTime;
            if(countdownText != null)
            {
                countdownText.text = repairMessage;
            }
            recoverySlider.value = timePassed * resetBarMultiplier;

        }
        else if(outOfTime && timePassed >= resetTime)
        {
            slider.SetActive(false);
            Debug.Log("all fixed!");
            currentTime = startingTime;
            outOfTime = false;
            isResetting = false;
            timePassed = 0;
        }
    }


}

