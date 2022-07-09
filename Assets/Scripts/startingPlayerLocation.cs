using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingPlayerLocation : MonoBehaviour
{
    [Header("Player Spawn Point")]
    [SerializeField] float startingX;
    [SerializeField] float startingY;
    public float startingZ;
    private GameObject thePlayer;

    //Get reference to where the player is 
    private void Awake()
    {
        thePlayer = GameObject.FindGameObjectWithTag("PlayerTag");
    }
    
    //Based on the spawn locations set in the serialized fields, spawn the player in that location
    void Start()
    {
        startingZ = thePlayer.transform.position.z;

        thePlayer.transform.position = new Vector3(startingX, startingY, startingZ);

    }
}
