using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to save player specific attack and defense stat.
public class playerStats
{
    public int atk;
    public int def;

    public playerStats()
    {
        atk = 1;
        def = 1;
    }

    public playerStats(int a, int d)
    {
        this.atk = a;
        this.def = d;
    }

    public void setDef(int d)
    {
        def = d;
    }
    public void setAtk(int a)
    {
        atk = a;
    }

    public int getDef()
    {
        return def;
    }
    public int getAtk()
    {
        return atk;
    }

}
