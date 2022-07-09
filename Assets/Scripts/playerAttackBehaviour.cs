using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerAttackBehaviour : MonoBehaviour
{
    private Animator controller;
    //Ensures that the attack object follows the player
    [SerializeField] GameObject thingToFollow;

    private void Awake()
    {
        controller = this.GetComponentInChildren<Animator>();
    }

    
    //VOA control logic
    private void Update()
    {
        transform.position = thingToFollow.transform.position;
        //If the attack is finished, hide the yellow outline (where the attack hit up to)
        if (controller.GetBool("attackIsOver2"))
        {
            this.gameObject.SetActive(false);
        }

    }

}
