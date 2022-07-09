using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class followPlayer : MonoBehaviour
{
    private GameObject thePlayer;

    public float offsetX = 8.45f;
    public float properZ = -10.0f;
    private void Awake()
    {
        thePlayer = GameObject.FindGameObjectWithTag("PlayerTag");
    }
   
    // Update is called once per frame
    void Update()
    {
        //Inventory and HUD also need to follow the player. They have their own specific coords
        if (this.gameObject.name == "HUD")
        {
            offsetX = 479.824f;
            properZ = 0.0f;
        }
        if(this.gameObject.name == "inventorySystem")
        {
            offsetX = 480f;
            properZ = 0.0f;
        }

        Vector3 playerLocation = thePlayer.transform.position;
        if(playerLocation.x > -8.370601f && playerLocation.x < 25.3489 && SceneManager.GetActiveScene().name == "Scene2")
        {
            //We only want the object to move in the X direction.
            playerLocation.x = playerLocation.x + offsetX;

            playerLocation.y = transform.position.y;
            playerLocation.z = properZ;
            transform.position = playerLocation;
        }
        else if (playerLocation.x > -8.370601f && playerLocation.x < 15.67442 && SceneManager.GetActiveScene().name == "Scene3")
        {
            //We only want the object to move in the X direction.
            playerLocation.x = playerLocation.x + offsetX;

            playerLocation.y = transform.position.y;
            playerLocation.z = properZ;
            transform.position = playerLocation;
        }


    }
}
