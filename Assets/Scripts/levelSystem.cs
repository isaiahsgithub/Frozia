using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSystem
{
    public event EventHandler OnEXPChange;
    public event EventHandler OnLevelChange;


    private int level;
    private int exp;
    private int expRequired;

    public levelSystem()
    {
        level = 1;
        exp = 0;
        expRequired = 100;
    }

    public levelSystem(int l, int e, int eR)
    {
        this.level = l;
        this.exp = e;
        this.expRequired = eR;
    }

    //Adds experience to the player
    public void AddExperience(int amount)
    {
        exp += amount;
        //If the player levels up
        while (exp >= expRequired)
        {
            //Reduce the EXP back to 0, increase the level by 1 and the new 
            //Required EXP is 50 more
            exp -= expRequired;
            level += 1;
            expRequired += 50;

            //Source: CodeMonkey - https://www.youtube.com/watch?v=kKCLMvsgAR0
            if (OnLevelChange != null)
            {
                OnLevelChange(this, EventArgs.Empty);
            }
        }

        //Source: CodeMonkey - https://www.youtube.com/watch?v=kKCLMvsgAR0
        if (OnEXPChange != null)
        {
            OnEXPChange(this, EventArgs.Empty);
        }
    }

    public int getLevel()
    {
        return level;
    }

    public int getEXP()
    {
        return exp;
    }

    public int getEXPReq()
    {
        return expRequired;
    }


}
