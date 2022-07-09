using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class disableText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI theTextToDisable;

    //Disables any text given
    void Start()
    {
        theTextToDisable.enabled = false;
    }
}
