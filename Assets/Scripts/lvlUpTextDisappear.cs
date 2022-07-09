using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lvlUpTextDisappear : MonoBehaviour
{
    //After 5 seconds, the text passed in through the serializefield will disappear
    [SerializeField] TextMeshProUGUI thisText;
    float disappearTime = 5.0f;
    void Update()
    {
        if (thisText.isActiveAndEnabled)
        {

            disappearTime -= Time.deltaTime;
            if(disappearTime <= 0)
            {
                thisText.enabled = false;
                disappearTime = 5.0f;
            }
        }       
    }
}
