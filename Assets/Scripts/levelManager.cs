using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelTMP;
    [SerializeField] TextMeshProUGUI levelUpText;

    private Slider expBar;
    private TextMeshProUGUI theEXPText;

    public levelSystem lvlSystem;
    public static myPlayersStat mPS;

    private void Awake()
    {
        expBar = GameObject.FindGameObjectWithTag("expSliderTag").GetComponent<Slider>();
        theEXPText = GameObject.FindGameObjectWithTag("EXPTextTag").GetComponent<TextMeshProUGUI>();
        
    }
    private void Start()
    {
        theEXPText.text = lvlSystem.getEXP() + "/" + lvlSystem.getEXPReq();
        
        levelUpText.enabled = false;
        //pS = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<playerStats>();
    }

    //For testing purposes or if the person marking this does not want to grind for EXP.
    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            lvlSystem.AddExperience(50);
        }
    }

    //Update EXP Bar and Text
    private void changeEXPBar(float expGained)
    {
        expBar.value = expGained;
        theEXPText.text = expGained + "/" + lvlSystem.getEXPReq();
    }

    //Change the green level text
    private void changeLevelText(int level)
    {
        levelTMP.text = level.ToString();
    }

    private void changeEXPMAX(float expREQ)
    {
        expBar.maxValue = expREQ;
    }

    public void SetLevelSystem(levelSystem myLevelSystem, myPlayersStat theMPS)
    {
        this.lvlSystem = myLevelSystem;
        changeLevelText(lvlSystem.getLevel());
        changeEXPBar(lvlSystem.getEXP());
        mPS = theMPS;

        //Source: CodeMonkey - https://www.youtube.com/watch?v=kKCLMvsgAR0
        lvlSystem.OnEXPChange += LevelSystem_OnExperienceChanged;
        lvlSystem.OnLevelChange += LevelSystem_OnLevelChanged;
        
    }

    //Returns the level system
    public levelSystem getLevelSystem()
    {
        return this.lvlSystem;
    }


    //Source: CodeMonkey - https://www.youtube.com/watch?v=kKCLMvsgAR0
    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        changeEXPBar(lvlSystem.getEXP());
    }

    //Source: CodeMonkey - https://www.youtube.com/watch?v=kKCLMvsgAR0
    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        changeLevelText(lvlSystem.getLevel());
        changeEXPMAX(lvlSystem.getEXPReq());
        changeEXPBar(lvlSystem.getEXP());

        //For every level, gain 1 attack
        mPS.getPlayerStats().setAtk(mPS.getPlayerStats().getAtk()+1);

        levelUpText.SetText("Congratulations! You are now level " + lvlSystem.getLevel() + "!");
        levelUpText.enabled = true;

    }
    //Gets the MPS, which is needed for when we do a save/load
    public myPlayersStat getMPS()
    {
        return mPS;
    }
    
}
