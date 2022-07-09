using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogScript : MonoBehaviour
{
    [SerializeField] public GameObject dialogBox;

    [SerializeField] public TextMeshProUGUI dialogText;
    [SerializeField] public string[] dialogs;

    [SerializeField] public GameObject forwardButton;
    [SerializeField] public GameObject backButton;

    [SerializeField] public TextMeshProUGUI nextFin;
    [SerializeField] public TextMeshProUGUI closeBack;
    public int currentPosition = 0;
    // Start is called before the first frame update
    void Start()
    {
        startAllDialog();
        updateText();
    }


    public void updateText()
    {
        if (currentPosition == 0)
        {
            closeBack.text = "Close";
            //If there isn't only 1 page of dialog
            if (dialogs.Length -1 != 0)
            {
                nextFin.text = "Next";
            }
            else
            {
                nextFin.text = "Finish";
            }
        }
        else if (currentPosition == dialogs.Length -1)
        {
            nextFin.text = "Finish";
            //If there isn't only 1 page of dialog
            if(currentPosition != 0)
            {
                closeBack.text = "Back";
            }
            else
            {
                closeBack.text = "Close";
            }
        }
        else
        {
            nextFin.text = "Next";
            closeBack.text = "Back";
        }
    }


    //Hide previous message, show next message.
    public void nextButtonClick()
    {
        //If you finished the conversation
        if (currentPosition == dialogs.Length-1)
        {
            //Reset conversation for next time
            nextFin.text = "Next";
            closeBack.text = "Close";

            //If there was only one dialog message for the dialog box.
            if ((dialogs.Length - 1) == 0)
            {
                nextFin.text = "Finish";
            }
            
            currentPosition = 0;
            dialogText.text = dialogs[currentPosition];
            
            //You are done with the conversation
            dialogBox.SetActive(false);
            return;
        }
        else
        {
            currentPosition += 1;
            dialogText.text = dialogs[currentPosition];
            updateText();
        }
    }

    //Hide current message, show previous message
    public void backButtonClick()
    {
        if(currentPosition != 0)
        {
            currentPosition -= 1;
            dialogText.text = dialogs[currentPosition];
            updateText();
        }
        else
        {
            //You can't go back any further, close dialog box
            //Debug.Log("cannot go back further");
            nextFin.text = "Next";
            closeBack.text = "Close";

            //If there was only one dialog message for the dialog box.
            if ((dialogs.Length - 1) == 0)
            {
                nextFin.text = "Finish";
            }

            dialogBox.SetActive(false);


        }
    }

    public void startAllDialog()
    {
        dialogText.enabled = true;
        dialogText.text = dialogs[currentPosition];
    }


    // Update is called once per frame
    void Update()
    {
        //If you click, check to see if you are clicking on the forward/backward button
        //Source: https://kylewbanks.com/blog/unity-2d-detecting-gameobject-clicks-using-raycasts

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if(hit.collider.gameObject == forwardButton)
                {
                    nextButtonClick();
                }
                else if(hit.collider.gameObject == backButton)
                {
                    backButtonClick();
                }
            }
        }
   
    }
}
