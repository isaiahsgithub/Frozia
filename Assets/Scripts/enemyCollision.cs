using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemyCollision : MonoBehaviour
{
    private Slider healthBar;
    private TextMeshProUGUI hpText;
    private Animator controller;
    private levelManager lM;
    private myPlayersStat mp;
    
    private GameObject gameOver;
    private GameObject gameOverText;
    private GameObject blackBG;

    [Header("Sound Effects")]
    [SerializeField] AudioSource playerHitSound;
    [SerializeField] AudioSource playerDeathSound;

    //Gets all the proper values and initializes values.
    private void Awake()
    {
        healthBar = GameObject.FindGameObjectWithTag("sliderTag").GetComponent<Slider>();
        hpText = GameObject.FindGameObjectWithTag("HPTextTag").GetComponent<TextMeshProUGUI>();
        controller = GetComponent<Animator>();
        lM = GameObject.FindGameObjectWithTag("HUDTag").GetComponent<levelManager>();
        mp = lM.getMPS();
        hpText.text = "100/100";
        gameOver = GameObject.FindGameObjectWithTag("gameOverButtonTag");
        gameOverText = GameObject.FindGameObjectWithTag("gameOverText");
        blackBG = GameObject.FindGameObjectWithTag("blackBackgroundTag");
        Time.timeScale = 1.0f;
        gameOver.SetActive(false);
        gameOverText.SetActive(false);
        blackBG.SetActive(false);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player collides with an enemy
        if (collision.gameObject.CompareTag("EnemiesTag")){
            //Decrease the health bar and update the health text
            healthBar.value -= getDamageDealt();
            hpText.text = healthBar.value + "/" + healthBar.maxValue;
            //Play the hit animation
            controller.SetTrigger("isHit");
            //If the play dies
            if(healthBar.value <= 0)
            {
                //Play the dead animation and sound effect
                controller.SetTrigger("isDead");
                playerDeathSound.Play();

                //Activate the game over UI.
                gameOver.SetActive(true);
                gameOverText.SetActive(true);
                blackBG.SetActive(true);
            }
            else
            {
                //If the player doesn't die, play the hit sound effect
                playerHitSound.Play();
            }
        }
    }

    private int getDamageDealt()
    {
        //Damage is calculated via base damage minus the players defense
        int baseDamage = 11;
        //Refresh MPS incase user loaded a save file
        mp = lM.getMPS();
        return baseDamage - mp.getPlayerStats().getDef();
    }

}
