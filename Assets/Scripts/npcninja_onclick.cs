using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcninja_onclick : MonoBehaviour
{
    //Gets the dialog box required
    [SerializeField] GameObject correctDialogBox;

    //Set the dialog box to be inactive
    private void Start()
    {
        correctDialogBox.SetActive(false);
    }

    //When the NPC is clicked, the dialog box will appear.
    private void OnMouseDown()
    {
        correctDialogBox.SetActive(true);
    }
}
