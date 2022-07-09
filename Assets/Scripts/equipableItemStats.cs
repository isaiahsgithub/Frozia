using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipableItemStats : MonoBehaviour
{
    [SerializeField] private bool armor;
    [SerializeField] private int statIncrease;

    private pickUpItem PUI;


    public void Start()
    {
        PUI = this.GetComponent<pickUpItem>();
    }

    public bool isArmor()
    {
        return armor;
    }

    public int getStats()
    {
        return statIncrease;
    }

    //The inventory system determines if the equip goes in either the armor or weapon slot via 1 or 0 respectively.
    public int isArmorToInt()
    {
        if (isArmor())
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }


    private void OnMouseDown()
    {
        //Debug.Log((int)this.isArmor());
        //If the item is currently in the inventory
        if (this.tag.Contains("Inventory"))
        {
            //Move it to the Equipped tab, update stats
            PUI.findWhereToEquipItem(this.gameObject, getStats());

        }
        //If the item is currently equipped
        else if (this.tag.Contains("Equipped"))
        {
            //Move it to the inventory, update stats
            PUI.findWhereToPutItemInInventory(this.gameObject, this.isArmorToInt(), getStats());
        }
    }

}
