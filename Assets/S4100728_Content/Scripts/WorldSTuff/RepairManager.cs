using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class RepairManager : MonoBehaviour
{
    [SerializeField] private CountTimer[] timers;
    public PlayerHealth healthCheck;
    public int sitesDownToLose;
    public int currentSitesDown;
    public TextMeshProUGUI noDown;
    private int sitesDown; 

    private void Awake()
    {
        currentSitesDown = 0;
        SiteTextDisplay();
    }
    public void SiteDown()
    {
        currentSitesDown++;
        SiteTextDisplay();
        if (currentSitesDown >= sitesDownToLose)
        {
            healthCheck.Dead();

        }
    }

    private void SiteTextDisplay()
    {
        noDown.text = "Sites Down:\n" + currentSitesDown.ToString();
    }

    public void SiteUp()
    {
        currentSitesDown = currentSitesDown - 1;
        SiteTextDisplay();
    
    }

    private void Update()
    {
        sitesDown = 0;
        foreach(CountTimer timer in timers)
        {
            if(timer.currentTime == 0)
            {
                sitesDown++;
            }
        }
        currentSitesDown = sitesDown;
    }
}
