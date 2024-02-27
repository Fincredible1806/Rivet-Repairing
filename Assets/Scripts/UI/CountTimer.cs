using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountTimer : MonoBehaviour
{
    public float currentTime = 0f;
    public float startingTime = 10f;
    [SerializeField] TextMeshProUGUI theCountdownText;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        
        if (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;
                if (theCountdownText != null)
                    {
                        theCountdownText.text = currentTime.ToString("F0");
                    }
        }
    }
}

