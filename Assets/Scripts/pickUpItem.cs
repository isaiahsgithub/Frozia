using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pickUpItem : MonoBehaviour
{
    private inventory theInventory;
    private equippedSlots theEquips;
    private equipableItemStats itemInfo;
    private levelManager lM;
    private myPlayersStat mp;

    private TextMeshProUGUI atkStatText;
    private TextMeshProUGUI defStatText;

    int potionNumber;

    private void Start()
    {

        theInventory = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<inventory>();
        Transform theUI_Inventory = GameObject.FindGameObjectWithTag("mainInventory").GetComponentInChildren<Transform>(true);
        GameObject gOinventory = theUI_Inventory.GetChild(0).gameObject;
        bool isActive = gOinventory.activeInHierarchy;
        if (!isActive)
        {
            gOinventory.SetActive(true);
        }
            

        //Find the attack and defense stat tags.
        foreach (TextMeshProUGUI e in GameObject.FindGameObjectWithTag("inventoryTag").GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            if(e.name == "actualAttackText (TMP)")
            {
                atkStatText = e;
                
            }
            if(e.name == "actualDefText (TMP)")
            {
                defStatText = e;
            }
        }
        if (!isActive)
        {
            gOinventory.SetActive(false);
        }

        theEquips = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<equippedSlots>();
        lM = GameObject.FindGameObjectWithTag("HUDTag").GetComponent<levelManager>();
        mp = lM.getMPS();
        

        atkStatText.text = mp.getPlayerStats().getAtk().ToString();
        defStatText.text = mp.getPlayerStats().getDef().ToString();
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player object collides with an item (and the item is not in inventory)
        if (collision.gameObject.CompareTag("PlayerTag") && this.gameObject.tag == "itemTag")
        {
            findWhereToPutItemInInventory(this.gameObject, -2, 0);
        }
    }


    public void findWhereToPutItemInInventory(GameObject theItem, int unEquipIdentifier, int statDecrease)
    {
        //If the item is being moved from equip window -> inventory
        if(unEquipIdentifier != -2)
        {
            //Free up the equip slot
            theEquips.isEquipped[unEquipIdentifier] = false;

            //Lose the stats
            //If unequiping armor, lose DEF
            if(unEquipIdentifier == 1)
            {
                //Refresh MP (incase if you are loading your save)
                mp = lM.getMPS();

                //Decrease the def stats based on the item you equip
                mp.getPlayerStats().setDef(mp.getPlayerStats().getDef() - statDecrease);
                updateText();
            }

            //If unequiping weapon, lose ATK
            else if(unEquipIdentifier == 0)
            {
                //Refresh MP (incase if you are loading your save)
                mp = lM.getMPS();

                //Decrease the atk stats based on the item you equip
                mp.getPlayerStats().setAtk(mp.getPlayerStats().getAtk() - statDecrease);
                updateText();
            }

        }
        for (int i = 0; i < theInventory.slots.Length; i++)
        {
            if (theInventory.isFull[i] == false)
            {
                //Each image is cropped strangely, to reposition the image correctly we have to scale the Y accordingly.
                float yPosition = 0.0f;
                if (theItem.name.Contains("Sword"))
                {
                    yPosition = 0.75f;
                }
                else if (theItem.name.Contains("Armor"))
                {
                    yPosition = 0.5f;
                }
                else if (theItem.name.Contains("Potion"))
                {
                    yPosition = 0.0f;
                }
                
                //theItem.transform.position = new Vector3(0.0f, yPosition, 0.0f);
                if (unEquipIdentifier == -2)
                {
                    theItem.transform.position = new Vector3(0.0f, yPosition, -1.0f);

                }


                //Debug.Log(theInventory.slots[i].transform);
                theInventory.isFull[i] = true;
                GameObject myClone = Instantiate(theItem, theInventory.slots[i].transform, false);
                myClone.tag = "itemInInventoryTag";
                theInventory.itemName[i] = theItem.name;

                if (theItem.name.Contains("Potion"))
                {
                    
                    myClone.name = theInventory.itemName[i] + i;
                }

                /*if(unEquipIdentifier == -2)
                {
                    myClone.transform.position = new Vector3(4.0f, (3.25f+yPosition), (-8.1f));
                }*/
                Destroy(theItem);
                break;
            }
        }
        
    }


    public void findWhereToEquipItem(GameObject theItem, int statIncrease)
    {
        //Armor goes in the 1 slot
        if (theItem.name.Contains("Armor"))
        {
            //If there isn't armor already equipped
            if(theEquips.isEquipped[1] == false)
            {

                //Remove the selected item from the inventory
                string equippedItemName = (theItem.name);
                equippedItemName = equippedItemName.Replace("(Clone)", "");
                for(int i=0; i<theInventory.slots.Length; i++)
                {
                    string currentItemName = theInventory.itemName[i];
                    currentItemName = currentItemName.Replace("(Clone)", "");
                    if(equippedItemName == currentItemName)
                    {
                        theInventory.itemName[i] = "";
                        theInventory.isFull[i] = false;
                    }
                }

                //Equip the item
                theEquips.isEquipped[1] = true;
                GameObject myClone = Instantiate(theItem, theEquips.equipSlots[1].transform, false);


                //Refresh MP (incase if you are loading your save)
                mp = lM.getMPS();

                //Increase the def stats based on the item you equip
                mp.getPlayerStats().setDef(mp.getPlayerStats().getDef() + statIncrease);
                updateText();

                myClone.tag = "isEquippedTag";
                Destroy(theItem);
            }
        }

        //Weapons go in the 0 slot
        else if (theItem.name.Contains("Sword"))
        {
            //If there isn't a weapon already equipped
            if (theEquips.isEquipped[0] == false)
            {
            
                //Remove the selected item from the inventory
                string equippedItemName = (theItem.name);
                equippedItemName = equippedItemName.Replace("(Clone)", "");
                for (int i = 0; i < theInventory.slots.Length; i++)
                {
                    string currentItemName = theInventory.itemName[i];
                    currentItemName = currentItemName.Replace("(Clone)", "");
                    if (equippedItemName == currentItemName)
                    {
                        theInventory.itemName[i] = "";
                        theInventory.isFull[i] = false;
                    }
                }

                //Equip the item
                theEquips.isEquipped[0] = true;
                GameObject myClone = Instantiate(theItem, theEquips.equipSlots[0].transform, false);

                //Refresh MP (incase if you are loading your save)
                mp = lM.getMPS();

                //Increase the atk stats based on the item you equip
                mp.getPlayerStats().setAtk(mp.getPlayerStats().getAtk() + statIncrease);
                updateText();
                myClone.tag = "isEquippedTag";
                Destroy(theItem);
            }
        }


    }

    //When picking up a potion, the name of the potion will become Potion 0/1/2/3 based on the inventory slot
    //It gets put in. When a user uses a potion, to free up the inventory slot, we can remove the Potion(Clone)
    //Part of the potions name to determine what slot to free up
    public void freeUpPotionSlot(string potionName)
    {
        string newPotion = potionName.Replace("(Clone)", "");
        newPotion = newPotion.Replace("healthPotion", "");
        int positionOfPotion = int.Parse(newPotion);
        theInventory.itemName[positionOfPotion] = "";
        theInventory.isFull[positionOfPotion] = false;
    }



    public void updateText()
    {
        atkStatText.text = mp.getPlayerStats().getAtk().ToString();
        defStatText.text = mp.getPlayerStats().getDef().ToString();
    }



}
