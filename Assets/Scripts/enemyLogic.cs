using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyLogic : MonoBehaviour
{

    [Header("Enemy Specifics")]
    [SerializeField] private Slider enemyHealthBar;
    [SerializeField] private Canvas enemyCanvas;
    private Animator controller;
    int damage = 1;
    bool isDead = false;

    [Header("Move Area")]
    public float minMoveX = 1.4f;
    public float maxMoveX = 1.4f;

    [Header("Possible drops")]
    [SerializeField] GameObject potionDrop;

    private GameObject[] currentInventorySlots;

    private levelManager lM;
    private myPlayersStat mp;

    float moveTime;
    bool isMoving = false;
    int changeDirection = 1;
    float movementSpeed = 1.0f;
    float canFlipTime = 1.5f;
    bool isOutOfBoundsLeft = false;
    bool isOutOfBoundsRight = false;
    bool shouldFlipDirections = false;

    private float startX;

    private float makeSoundEffectTimer = 5.0f;
    private float randomSoundTimeInterval = 0.0f;

    private playerScript thePlayer;
    [SerializeField] GameObject playerAttack;

    private AudioSource zombieSound;
    private AudioSource zombieDeath;

    private void Awake()
    {
        thePlayer = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<playerScript>();
        playerAttack = GameObject.FindGameObjectWithTag("VisualizationAttackTag");

        zombieSound = GameObject.FindGameObjectWithTag("zombieSoundTag").GetComponent<AudioSource>();
        zombieDeath = GameObject.FindGameObjectWithTag("zombieDeathTag").GetComponent<AudioSource>();

        //zombieSound.Play();

        randomSoundTimeInterval = determineTimeInterval();
        makeSoundEffectTimer = randomSoundTimeInterval;

        controller = GetComponent<Animator>();
        isDead = false;
        moveTime = getRandomFloat(1.0f, 5.0f);

        lM = GameObject.FindGameObjectWithTag("HUDTag").GetComponent<levelManager>();
        mp = lM.getMPS();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "VisualizationAttackTag")
        {
            hitEnemy();
        }
        
    }

    private void Start()
    {
        startX = transform.position.x;
    }


    // Update is called once per frame
    void Update()
    {
       
        tryToMove();

        //Enemy have a chance to make a sound every makeSoundEffectTimer seconds
        makeSoundEffectTimer -= Time.deltaTime;
        if(makeSoundEffectTimer <= 0)
        {
            //See if the enemy will make a sound (decided randomly)
            playEnemySound();
            makeSoundEffectTimer = randomSoundTimeInterval;
        }

    }

    void tryToMove()
    {
        //This is the move enemy logic, only works if the enemy is set to move and if the enemy is not dead.
        if (isMoving == true && isDead == false)
        {
            canFlipTime -= Time.deltaTime;

            //If the enemy tries to move past where it is allowed, change its direction
            if(gameObject.transform.position.x <= (startX - minMoveX) && isOutOfBoundsLeft == false)
            {
                if(isOutOfBoundsLeft == false)
                {
                    shouldFlipDirections = true;
                }
                isOutOfBoundsLeft = true;
                changeDirection = 1;
            }
            else if (gameObject.transform.position.x >= (startX + maxMoveX) && isOutOfBoundsRight == false)
            {
                if(isOutOfBoundsRight == false)
                {
                    shouldFlipDirections = true;
                }
                isOutOfBoundsRight = true;
                changeDirection = -1;
            }


            //If it was stuck at the left, and it is now fixed
            if (gameObject.transform.position.x >= (startX - minMoveX) + (0.2) && isOutOfBoundsLeft == true)
            {
                isOutOfBoundsLeft = false;
            }

            //If it was stuck at the right, and it is now fixed
            if (gameObject.transform.position.x <= (startX + maxMoveX) + (0.2) && isOutOfBoundsRight == true)
            {
                isOutOfBoundsRight = false;
            }


            //To make sure the enemy doesn't infinitely flip around 
            if (isOutOfBoundsLeft == false && isOutOfBoundsRight == false)
            {
                //Every 1.5 seconds the enemy can flip directions.
                if (canFlipTime <= 0)
                {
                    canFlipTime = 1.5f;
                    //Randomly decide if the enemy flips around
                    if (getRandomInt(0, 100) % 2 == 0)
                    {
                        changeDirection = changeDirection * -1;
                        shouldFlipDirections = true;
                    }
                }
            }


            //Move the enemy, change direction decides the direction (-1 = left, 1 = right
            Vector3 movement = (Vector3.right * changeDirection) * movementSpeed * Time.deltaTime;
            transform.Translate(movement);
            
            //Animate the enemy to be moving
            controller.SetBool("zombieIsMoving", true);

            //If the enemy moves to the left, flip the sprite.
            transform.localScale = new Vector3(0.5f * changeDirection, 0.5f, 0.572f);
            if (shouldFlipDirections == true)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                shouldFlipDirections = false;
            }
            
            //BUG FIX: If the enemy is facing left, the health bar will no longer be flipped.
            if (transform.localScale.x <= 0)
            {
                enemyHealthBar.transform.localScale = new Vector3(0.02f * -1, 0.02f, 1.0f);
            }
            else
            {
                enemyHealthBar.transform.localScale = new Vector3(0.02f, 0.02f, 1.0f);
            }

        }
        //If the move timer is up, stop the enemy from moving (if it is already moving)
        //Then, randomly decide if it is able to move.
        //Randomly generate how long the enemy can move for (>1s and <=5s). 
        //This is also the timer for how long until the enemy can move again (i.e. if the enemy isn't moving, after this timer, decide if it can move)
        moveTime -= Time.deltaTime;
        if (moveTime <= 0)
        {
            if (isMoving == true)
            {
                controller.SetBool("zombieIsMoving", false);
                isMoving = false;
            }
            if (getRandomInt(0, 100) % 2 == 0)
            {
                isMoving = true;
            }
            moveTime = getRandomFloat(1.0f, 5.0f);
        }
    }


    int getRandomInt(int theMin, int theMax)
    {
        return Random.Range(theMin, theMax);
    }

    float getRandomFloat(float theMin, float theMax)
    {
        return Random.Range(theMin, theMax);
    }

    //Enemy Being Hit Logic
    void hitEnemy()
    {
        if(isDead == false)
        {
                //Damage dealt is equal to the players ATK stat.
                lM = GameObject.FindGameObjectWithTag("HUDTag").GetComponent<levelManager>();
                mp = lM.getMPS();
                damage = mp.getPlayerStats().getAtk();
                
                //Reduce HP and kill the enemy
                enemyHealthBar.value -= damage;
                if (enemyHealthBar.value <= 0)
                {
                    killAndDespawn(1.0f);
                    if (isDead == false)
                    {

                        giveItemReward();
                        giveEXP();
                    }
                }
            //}
        }
        
    }




    //Wait for the enemy animation to finish, then after deathDelay time, destroy the zombie
    void killAndDespawn(float deathDelay)
    {
        zombieDeath.Play();
        //Destroy collider so you can't take damage when the enemy dies
        Destroy(gameObject.GetComponent<Collider2D>());
        controller.SetTrigger("zombieIsDead");
        Destroy(gameObject, controller.GetCurrentAnimatorStateInfo(0).length + deathDelay);
    }

    void giveItemReward()
    {
        int receivesReward = getRandomInt(0,100);
        Debug.Log("Random number: " + receivesReward + " is less than 30: " + (receivesReward<30));
        //30% chance to get an item as a drop
        if(receivesReward < 30)
        {
            
            //You can only get potions from drops
            GameObject myClone = Instantiate(potionDrop, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            myClone.transform.position = this.gameObject.transform.position;
            myClone.tag = "itemTag";
        }

        

    }

    //Reward the player EXP, but only once for defeating this enemy.
    void giveEXP()
    {
        thePlayer.getLevelSystem().AddExperience(25);
        isDead = true;
    }

    float determineTimeInterval()
    {
        return getRandomFloat(5.0f, 20.0f);
    }

    //50% chance to randomly play the zombie sound effect.
    void playEnemySound()
    {
        int fiftyChanceForSound = getRandomInt(0,100);
        if(fiftyChanceForSound < 50)
        {
            zombieSound.Play();
        }
    }

}
