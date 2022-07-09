using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [SerializeField] private levelManager lvlManager;
    private myPlayersStat mPS;
    levelSystem lvlSystem;
    playerStats pS;

    /*public void Awake()
    {
        mPS = this.GetComponentInChildren<myPlayersStat>();
    }*/

    //Creates a new player that has stats and a level/exp/expREQ
    private void Awake()
    {
        mPS = new myPlayersStat();
        pS = new playerStats();
        lvlSystem = new levelSystem();
        /*lvlSystem.AddExperience(50);
        Debug.Log(lvlSystem.getEXP());
        lvlSystem.AddExperience(50);
        Debug.Log(lvlSystem.getEXP());
        Debug.Log(lvlSystem.getLevel());

        lvlSystem.AddExperience(50);

        lvlSystem.AddExperience(50);
        Debug.Log(lvlSystem.getEXP());
        Debug.Log(lvlSystem.getEXPReq());
        Debug.Log(lvlSystem.getLevel());*/
        mPS.setPlayerStats(pS);
        lvlManager.SetLevelSystem(lvlSystem, mPS);

    }

    public levelSystem getLevelSystem()
    {
        return lvlSystem;
    }

    public void updateLevelSystem(levelSystem L, myPlayersStat mp)
    {
        this.lvlSystem = L;
        this.mPS = mp;
        lvlManager.SetLevelSystem(lvlSystem, mPS);
    }

}
