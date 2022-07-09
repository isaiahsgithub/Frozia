using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleLogic : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject speechBubble;

    bool speechActive = true;
    public float timeUntilDisplay = 5.0f;

    //Initially start being shown
    private void Start()
    {
        speechBubble.SetActive(speechActive);
    }

    private void Update()
    {
        //If the dialog box isn't up, then show the speech bubble
        // Don't want to have the speech bubble up 24/7, have it up for
        //limited time
        if (!dialogBox.activeInHierarchy)
        {
            timeUntilDisplay -= Time.deltaTime;
            if (timeUntilDisplay < 0)
            {
                speechActive = !speechActive;
                speechBubble.SetActive(speechActive);
                timeUntilDisplay = 5.0f;
            }
        }
        else
        {
            speechActive = false;
            timeUntilDisplay = 5.0f;
            speechBubble.SetActive(speechActive);
        }
    }

}
