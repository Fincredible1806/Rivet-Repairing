using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RepairManager : MonoBehaviour
{
    public PlayerHealth healthCheck;
    public int sitesDownToLose;
    public int currentSitesDown;
    public TextMeshProUGUI noDown;

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
}
