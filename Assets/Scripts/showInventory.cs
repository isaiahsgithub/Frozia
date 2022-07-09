using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showInventory : MonoBehaviour
{
    private Transform mainInventoryTransform;
    private GameObject UI_Inventory;
    bool isEnabled = false;


    [SerializeField] TextMeshProUGUI textATK;
    [SerializeField] TextMeshProUGUI textDEF;

    private levelManager lM;
    private myPlayersStat mp;

    private GameObject thingE;

    private void Awake()
    {
        lM = GameObject.FindGameObjectWithTag("HUDTag").GetComponent<levelManager>();
        mp = lM.getMPS();
        
    }

    //Start with the inventory hidden
    private void Start()
    {
        mainInventoryTransform = GameObject.FindGameObjectWithTag("mainInventory").GetComponentInChildren<Transform>(true);
        UI_Inventory = mainInventoryTransform.GetChild(0).gameObject;
        UI_Inventory.SetActive(false);
    }

    private void Update()
    {

     if (Input.GetKeyDown(KeyCode.I))
        {
            //If the inventory is not currently up, show it
            if(UI_Inventory.activeInHierarchy == false)
            {
                lM = GameObject.FindGameObjectWithTag("HUDTag").GetComponent<levelManager>();
                mp = lM.getMPS();
                //Debug.Log(lM.getMPS().getPlayerStats().getAtk());
                isEnabled = true;
                UI_Inventory.SetActive(isEnabled);

                
                textATK.text = mp.getPlayerStats().getAtk().ToString();
                textDEF.text = mp.getPlayerStats().getDef().ToString();
            }
            //If the inventory is currently open, hide it
            else
            {
                isEnabled = false;
                UI_Inventory.SetActive(isEnabled);
            }
        }   
    }

}
