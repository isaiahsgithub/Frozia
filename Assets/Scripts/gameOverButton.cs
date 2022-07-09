using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverButton : MonoBehaviour
{

    private void OnMouseDown()
    {
        PlayAgainAfterDeath();
    }

    public void PlayAgainAfterDeath()
    {
        //These objects were all under the "Don't Destroy On Load"
        //So since I'm going back to main menu, I do not need these anymore
        //So I am destroying them.
        Destroy(GameObject.FindGameObjectWithTag("VOATag"));
        Destroy(GameObject.FindGameObjectWithTag("PlayerTag"));
        Destroy(GameObject.FindGameObjectWithTag("HUDTag"));
        Destroy(GameObject.FindGameObjectWithTag("mainInventory"));
        Destroy(GameObject.FindGameObjectWithTag("soundsTag"));
        SceneManager.LoadScene("MainMenu");
    }
}
