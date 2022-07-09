using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class npcHeal : MonoBehaviour
{
    //NPC will heal a player fully.
    int healAmount = 100;
    private Slider thePlayerHPBar;
    private TextMeshProUGUI hpText;
    private TextMeshProUGUI informText;

    private void Awake()
    {
        thePlayerHPBar = GameObject.FindGameObjectWithTag("sliderTag").GetComponent<Slider>();
        hpText = GameObject.FindGameObjectWithTag("HPTextTag").GetComponent<TextMeshProUGUI>();
        informText = GameObject.FindGameObjectWithTag("informativeTextTag").GetComponent<TextMeshProUGUI>();
    }

    private void OnMouseDown()
    {
        //To prevent overflow, heal the player up to 100 HP
        healAmount = (int)thePlayerHPBar.maxValue - (int)thePlayerHPBar.value;
        thePlayerHPBar.value += healAmount;

        //Print out that HP has been recovered.
        Debug.Log("Recovered " + healAmount + " HP!");
        hpText.text = thePlayerHPBar.value + "/" + thePlayerHPBar.maxValue;
        informText.enabled = true;
        informText.text = "Recovered " + healAmount + " HP!";
    }

}
