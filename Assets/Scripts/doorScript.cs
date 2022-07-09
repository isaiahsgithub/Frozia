using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorScript : MonoBehaviour
{
    //Takes in the scene to go to as a serialized field
    [SerializeField] string sceneName;
    private Transform mainInventoryTransform;
    private GameObject UI_Inventory;

    bool wasActive;

    // Start is called before the first frame update
    void Start()
    {
        //Obtains the proper objects
        mainInventoryTransform = GameObject.FindGameObjectWithTag("mainInventory").GetComponentInChildren<Transform>(true);
        UI_Inventory = mainInventoryTransform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If a player enters a door
        if(collision.gameObject.tag == "PlayerTag")
        {
            //Activate the inventory, so nothing is lost. If it was originally
            //disabled, after loading the scene we deactivate it
            wasActive = UI_Inventory.activeInHierarchy;
            if (wasActive == false)
            {
                UI_Inventory.SetActive(true);
            }
            SceneManager.LoadScene(sceneName);

            if(wasActive == false)
            {
                UI_Inventory.SetActive(false);
            }

        }
    }

}
