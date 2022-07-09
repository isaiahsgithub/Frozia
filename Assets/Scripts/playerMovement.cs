using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class playerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float jumpForce = 450f;
    [SerializeField] private Transform footPosition;
    [SerializeField] private bool isGrounded = false;

    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float closeEnough = 0.1f;
    private Rigidbody2D rb;
    private Animator controller;
    
    [SerializeField] GameObject AttackObject;

    [Header("Sound Effects")]
    [SerializeField] AudioSource playerAttackSound;

    bool isCrouching = false;
    int movementDirection = 1;


    private void Start()
    {
        AttackObject.SetActive(false);
    }
    private void Awake()
    {
       
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Animator>();
    }

    private void Update()
    {

        //You cannot be crouching if you want to attack
        if(Input.GetButtonDown("Fire1") && !isCrouching)
        {
            controller.SetTrigger("isAttacking");
            
            if(AttackObject.activeInHierarchy == false)
            {

                AttackObject.SetActive(true);
                playerAttackSound.Play();
            }

        }

        //Lay down
        if (Input.GetKeyDown("down"))
        {
            isCrouching = true;
            controller.SetBool("crouching", isCrouching);
        }
        else if (Input.GetKeyUp("down"))
        {
            isCrouching = false;
            controller.SetBool("crouching", isCrouching);
        }
        else if (!isCrouching)
        {
            //If Not Crouching Down

            //Jumping Features
            RaycastHit2D hit = Physics2D.Raycast(footPosition.position, Vector2.down, closeEnough, groundLayers);

            //Checks if the player is on the ground - You need to be on the ground in order to jump 
            //(i.e. no double jump)
            if (hit.collider)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                //If able to jump, then jump
                rb.AddForce(Vector3.up * jumpForce);
                controller.SetTrigger("jumping");
            }

            //Left/Right Movement
            float movementX = Input.GetAxis("Horizontal") * movementSpeed;
            Vector3 movement = Vector3.right * movementX * Time.deltaTime;
            transform.Translate(movement);
            controller.SetFloat("moveSpeed", Mathf.Abs(movementX));


            if (movementX < 0f)
            {
                //If moving left
                movementDirection = -1;
            }
            else if (movementX > 0f)
            {
                //If moving right
                movementDirection = 1;
            }
            //Depending on the way that you last moved, update the player to be facing that way
            transform.localScale = new Vector3(0.5f * movementDirection, 0.5f, 0.5f);
            AttackObject.transform.localScale = new Vector3(0.5f * movementDirection, 0.5f, 0.5f);
        }
        

    }

}
