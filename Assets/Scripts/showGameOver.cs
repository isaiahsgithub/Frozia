using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showGameOver : MonoBehaviour
{
    float timeUntilGameOver;
    private void Start()
    {
        timeUntilGameOver = 2.0f;
    }


    //After 2.0 seconds, prevent the player from moving.
    //This is to ensure that the player death animation and sound finish
    //Before locking the game
    void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            timeUntilGameOver -= Time.deltaTime;
            if(timeUntilGameOver <= 0)
            {
                Time.timeScale = 0.0f;
            }
        }
    }
}
