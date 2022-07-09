using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script for the main menu. Allows the 2 buttons (Start and Quit) to function
public class mainMenu : MonoBehaviour
{

    //Loads the first scene
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //Quits the game - note only works when in build and run mode
    public void QuitGame()
    {
        Application.Quit();
    }

}
