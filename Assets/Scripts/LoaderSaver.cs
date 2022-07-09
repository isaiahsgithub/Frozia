using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;



[System.Serializable]
public class playerSpecifics
{

    public float curHP;
    public float maxHP;
    public float curEXP;
    public float maxEXP;
    public int level;
    public int atk;
    public int def;

    public playerSpecifics(float cHP, float mHP, float cEXP, float mEXP, int l, int a, int d)
    {
        this.curHP = cHP;
        this.maxHP = mHP;
        this.curEXP = cEXP;
        this.maxEXP = mEXP;
        this.level = l;


        this.atk = a;
        this.def = d;

    }

    public float getCurHP()
    {
        return this.curHP;
    }
    public float getMaxHP()
    {
        return this.maxHP;
    }

    public float getCurEXP()
    {
        return this.curEXP;
    }
    public float getMaxEXP()
    {
        return this.maxEXP;
    }

    public int getLevel()
    {
        return this.level;
    }

    public int getAtk()
    {
        return this.atk;
    }

    public int getDef()
    {
        return this.def;
    }

}




public class LoaderSaver : MonoBehaviour
{

    private float cHP;
    private float mHP;
    private float cEXP;
    private float mEXP;
    private int l;
    private GameObject inv;
    private equippedSlots playerEquips;

    private levelManager myManager;

    private Slider healthSlider;
    private Slider expSlider;
    private TextMeshProUGUI levelTMP;
    private TextMeshProUGUI hpText;

    private playerScript myPlayerScript;

    private TextMeshProUGUI informText;
    private void Awake()
    {
        myManager = GameObject.FindGameObjectWithTag("HUDTag").GetComponent<levelManager>();

        inv = GameObject.FindGameObjectWithTag("mainInventory");
        levelTMP = GameObject.FindGameObjectWithTag("actualLevelTag").GetComponent<TextMeshProUGUI>();
        healthSlider = GameObject.FindGameObjectWithTag("sliderTag").GetComponent<Slider>();
        expSlider = GameObject.FindGameObjectWithTag("expSliderTag").GetComponent<Slider>();
        hpText = GameObject.FindGameObjectWithTag("HPTextTag").GetComponent<TextMeshProUGUI>();

        informText = GameObject.FindGameObjectWithTag("informativeTextTag").GetComponent<TextMeshProUGUI>();
        playerEquips = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<equippedSlots>();

        myPlayerScript = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<playerScript>();
    }
    private void Start()
    {
        refreshVariables();
    }


    public void refreshObjects()
    {
        inv = GameObject.FindGameObjectWithTag("mainInventory");
        levelTMP = GameObject.FindGameObjectWithTag("actualLevelTag").GetComponent<TextMeshProUGUI>();
        healthSlider = GameObject.FindGameObjectWithTag("sliderTag").GetComponent<Slider>();
        expSlider = GameObject.FindGameObjectWithTag("expSliderTag").GetComponent<Slider>();
        myManager = GameObject.FindGameObjectWithTag("HUDTag").GetComponent<levelManager>();
        hpText = GameObject.FindGameObjectWithTag("HPTextTag").GetComponent<TextMeshProUGUI>();

    }

    public void refreshVariables()
    {
        cHP = healthSlider.value;
        mHP = healthSlider.maxValue;
        cEXP = expSlider.value;
        mEXP = expSlider.maxValue;
        l = int.Parse(levelTMP.text);
    }

    private void Update()
    {
        //If S is pressed, save the game.
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("Saving!");
            refreshObjects();
            refreshVariables();
            //Get all the appropriate information needed for a save.
            int theAtk = myManager.getMPS().getPlayerStats().getAtk();
            int theDef= myManager.getMPS().getPlayerStats().getDef();
            
            //Attack should be the same as the level.
            //However when you die, you lose your items. 
            //So, if these 2 are not equal, you lose the stat that the weapon gave you.
            if(l != theAtk)
            {
                theAtk -= 3;
            }

            //Defense should be 1 unless if you have something equipped.
            //Since you lose your items when you die, your defense becomes 1 again.
            if(theDef != 1)
            {
                theDef = 1;
            }
            //Create a playerSpecifics that holds the information that will be saved
            playerSpecifics myPlayerInfo = new playerSpecifics(cHP, mHP, cEXP, mEXP, l, theAtk, theDef);
            saveAsJSON(Application.persistentDataPath + "/thePlayerSpecifics.json", myPlayerInfo);
            Debug.Log("Saved!");

            //Show that the game has been saved
            informText.enabled = true;
            informText.text = "Saved!";
        }
        //If L is pressed, load the save
        if (Input.GetKeyDown("l"))
        {
            refreshObjects();
            refreshVariables();
            loadJSON(Application.persistentDataPath + "/thePlayerSpecifics.json", myPlayerScript, healthSlider, expSlider, hpText, informText, playerEquips);
        }

    }



    //Save function
    public static void saveAsJSON(string savePath, playerSpecifics playerInfo)
    {
        
        string json = JsonUtility.ToJson(playerInfo);
        File.WriteAllText(savePath, json);
        Debug.Log(savePath);
    }

    //Load function
    public static void loadJSON(string savePath, playerScript myPlayerScript, Slider hpBar, Slider expBar, TextMeshProUGUI hpTEXT, TextMeshProUGUI inform, equippedSlots playersEquips)
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            playerSpecifics p = JsonUtility.FromJson<playerSpecifics>(json);
            
            //Loads the save and sets all the UI to be equal to the loaded in data.
            levelSystem myNewLVL = new levelSystem(p.getLevel(), (int)p.getCurEXP(), (int)p.getMaxEXP());

            //Bug fix, if you have gear equipped before loading the save, it will now properly assign the stats when loading your save. 
            playersEquips = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<equippedSlots>();
            int myAttackValue = p.getAtk();
            int myDefValue = p.getDef();

            //If a weapon is equipped before loading the save
            if(playersEquips.isEquipped[0] == true)
            {
                //This is how much attack gets increased via the strong sword
                myAttackValue += 3;
            }

            //If armor is equipped before loading the save
            if(playersEquips.isEquipped[1] == true)
            {
                //This is the armor defense stat
                myDefValue += 3;
            }

            playerStats myStats = new playerStats(myAttackValue, myDefValue);
            myPlayersStat myPlayerS = new myPlayersStat(myStats);
            myPlayerScript.updateLevelSystem(myNewLVL, myPlayerS);
            hpBar.value = p.getCurHP();
            hpBar.maxValue = p.getMaxHP();
            hpTEXT.text = p.getCurHP() + "/" + p.getMaxHP();

            expBar.value = p.getCurEXP();
            expBar.maxValue = p.getMaxEXP();

            inform.enabled = true;
            inform.text = "Loaded!";
        }
        else
        {
            //If unable to load the save
            Debug.LogError("Unable to load the file: " + savePath);
            inform.enabled = true;
            inform.text = "Unable to load file.";
        }
    }

}
