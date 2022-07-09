using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class keepObject : MonoBehaviour
{


    private GameObject thePlayer;

    //If this script is on an object, when switching scenes this object won't be destroyed
    //This is done via the DontDestroyOnLoad function
    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        //The purpose of SpawnLoc (Empty) is to check when the player loads into the third scene, then goes back
        //to the second scene. It did not make sense that the player would spawn at the very left of scene2,
        //which was super far away from the scene 3 portal, as that is where you just came from.
        //This code makes it so that if you go from scene3 back to scene 2, you will spawn close to the scene3 portal.
        if(this.gameObject.name == "SpawnLoc (Empty)" && SceneManager.GetActiveScene().name == "Scene2")
        {
            thePlayer = GameObject.FindGameObjectWithTag("PlayerTag");

            float spawnX = 40.42f;
            float spawnY = 0.7f;
            float spawnZ = thePlayer.transform.position.z;
            thePlayer.transform.position = new Vector3(spawnX, spawnY, spawnZ);

            GameObject theCamera = GameObject.FindGameObjectWithTag("MainCamera");
            GameObject theInv = GameObject.FindGameObjectWithTag("mainInventory");
            GameObject theHUD = GameObject.FindGameObjectWithTag("HUDTag");

            theCamera.transform.position = new Vector3(33.79495f, 0.59f, theCamera.transform.position.z);
            theInv.transform.position = new Vector3(505.1689f, 236, theInv.transform.position.z);
            theHUD.transform.position = new Vector3(505.3449f, 235, theHUD.transform.position.z);

            Destroy(this.gameObject);
        }
    }

}
