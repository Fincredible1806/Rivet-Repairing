using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairManager : MonoBehaviour
{
    public PlayerHealth healthCheck;
    public int sitesDownToLose;
    public int currentSitesDown;
    public void SiteDown()
    {
        currentSitesDown++;
        if(currentSitesDown >= sitesDownToLose)
        {
            healthCheck.dead = true;

        }
    }

    public void SiteUp()
    {
        currentSitesDown--;
    }
}
