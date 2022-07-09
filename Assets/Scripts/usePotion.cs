using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class usePotion : MonoBehaviour
{

    private GameObject theHPBarGameObject;
    private Slider thePlayerHPBar;

    private TextMeshProUGUI hpText;
    private TextMeshProUGUI informText;
    //Potions will heal the user for 25 HP max.
    int potionHeal = 25;
    int overFlow;

    private pickUpItem PUI;

    public void Awake() 
    {
        theHPBarGameObject = GameObject.FindGameObjectWithTag("sliderTag");
        hpText = GameObject.FindGameObjectWithTag("HPTextTag").GetComponent<TextMeshProUGUI>();
        thePlayerHPBar = theHPBarGameObject.GetComponent<Slider>();
        informText = GameObject.FindGameObjectWithTag("informativeTextTag").GetComponent<TextMeshProUGUI>();
    }
    public void Start()
    {
        PUI = this.GetComponent<pickUpItem>();

    }


    private void OnMouseDown()
    {
        //Check to make sure that the potion is in the inventory. 
        //You don't want to heal off a potion that is on the ground.
        //If the potion is in the inventory, its name will contain 0/1/2/3 based on the inventory 
        //slot it is in.
        string potionName = this.gameObject.name;
        if(potionName.Contains("0") || potionName.Contains("1") || potionName.Contains("2") ||potionName.Contains("3")){

            //If the potion will heal the user over 100 HP, adjust how much the potion will heal the user
            overFlow = potionHeal;
            if(thePlayerHPBar.value + potionHeal > 100)
            {
                overFlow = 100 - ((int)(thePlayerHPBar.value));
            }
            //Heal the user
            thePlayerHPBar.value += overFlow;
            hpText.text = thePlayerHPBar.value + "/" + thePlayerHPBar.maxValue;

            //Display that the potion has healed the user
            Debug.Log("Recovered " + overFlow + " HP!");
            informText.enabled = true;
            informText.text = "Recovered " + overFlow + " HP!";

            //Free up the potion slot that the potion was in
            PUI.freeUpPotionSlot(potionName);

            //Destroy the potion
            Destroy(this.gameObject);
        }
    }
}
