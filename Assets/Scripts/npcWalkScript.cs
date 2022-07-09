using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcWalkScript : MonoBehaviour
{
    [SerializeField] private string animationBoolWalkName;
    [SerializeField] private float walkTimer = 5.0f;
    public float minMoveX = 2.5f;
    public float maxMoveX = 2.5f;
    private float startX;

    bool isMoving = false;
    public int whatHappens;
    float refToWalkTimer;

    bool isOutOfLeftBound = false;
    bool isOutOfRightBound = false;
    int changeDirection = 1;
    bool shouldChangeDirection = false;

    private Animator controller;

    private void Awake()
    {
       refToWalkTimer = walkTimer;
       controller = GetComponent<Animator>();
    }
    private void Start()
    {
        // So that if you leave the map and come back, it will not force them to switch directions and walk awkwardly.
        changeDirection = 1;
        shouldChangeDirection = false;
        startX = transform.position.x;
    }


    // Update is called once per frame
    void Update()
    {
        //Every 5.0f seconds (or however much time was set in the SerializeField)
        // The NPC has a 50% chance to walk.
        //If whatHappens = 0, they will do nothing
        //If whatHappens = 1, they will walk
        walkTimer -= Time.deltaTime;
        if(walkTimer <=0)
        {
            whatHappens = Random.Range(0, 2);
            //If the NPC was not already moving, allow them to move
            if (isMoving == false)
            {
                isMoving = true;
                
            }
            else
            {
                //If the NPC was moving, then 5 seconds passed again. Stop them from moving
                isMoving = false;
                controller.SetBool(animationBoolWalkName, isMoving);

            }
            //Reset the timer
            walkTimer = refToWalkTimer;
        }
        tryToMoveNPC();
        
    }


    void tryToMoveNPC()
    {
        //If the NPC is able to move
        if(isMoving == true)
        {
            //0,1 possible outcomes.
            //If 0, nothing happens
            //If 1, npc will move
            if (whatHappens == 1)
            {
                //If stuck at left
                if (gameObject.transform.position.x <= (startX - minMoveX) && isOutOfLeftBound == false)
                {
                    if(isOutOfLeftBound == false)
                    {
                        shouldChangeDirection = true;
                    }
                    isOutOfLeftBound = true;
                    changeDirection = 1;
                }
                //If stuck at right
                else if(gameObject.transform.position.x >= (startX + maxMoveX) && isOutOfRightBound == false)
                {
                    if (isOutOfRightBound == false)
                    {
                        shouldChangeDirection = true;
                    }
                    isOutOfRightBound = true;
                    shouldChangeDirection = true;
                    changeDirection = -1;
                }
                //If stuck at left and it was corrected
                if((gameObject.transform.position.x >= (startX - minMoveX) + 0.2) && isOutOfLeftBound == true)
                {
                    isOutOfLeftBound = false;
                }

                //If stuck at right and it was corrected
                if (gameObject.transform.position.x <= (startX + maxMoveX) + (0.2) && isOutOfRightBound == true)
                {
                    isOutOfRightBound = false;
                }
                //Move the NPC
                Vector3 movement = (Vector3.right * changeDirection) * 3.0f * Time.deltaTime;
                transform.Translate(movement);
                controller.SetBool(animationBoolWalkName, true);
                Vector3 moveDir = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                //If the NPC is switching directions, flip the sprite.
                if(shouldChangeDirection == true)
                {
                    moveDir.x = transform.localScale.x * -1;
                    shouldChangeDirection = false;
                }
                transform.localScale = moveDir;

            }
            else if (whatHappens == 0)
            {
                //If the NPC isn't moving, play the idle animation (idle triggers when walking bool = false)
                isMoving = false;

                controller.SetBool(animationBoolWalkName, false);
            }
        }

    }


}
