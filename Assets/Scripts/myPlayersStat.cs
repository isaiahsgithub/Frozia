using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPlayersStat : MonoBehaviour
{
    private playerStats stats;

    public myPlayersStat()
    {

    }

    public myPlayersStat(playerStats s)
    {
        this.stats = s;
    }

    public void setPlayerStats(playerStats theStats)
    {
        this.stats = theStats;
    }


    public playerStats getPlayerStats()
    {
        return stats;
    }
    
}
