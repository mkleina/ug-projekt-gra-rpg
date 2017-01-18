using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalk : MonoBehaviour {
    public float talkDistance = 3.0f;
    public NPCDialog []dialogText;
    public int dialogID;

    private int dialogTextID;
    private bool talking = false;

	// Use this for initialization
	void Start () {
        GameObject.Find("DialogBar").GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    public void setDialog(int id)
    {
        if (id != dialogID)
        {
            dialogID = id;
            dialogTextID = 0;
        }
    }

    void dialogFade(float value)
    {
        CanvasGroup dialogCanvas = GameObject.Find("DialogBar").GetComponent<CanvasGroup>();
        dialogCanvas.alpha = Mathf.Clamp(dialogCanvas.alpha + value * Time.deltaTime, 0.0f, 1.0f);
    }

    void setText(string text)
    {
        GameObject.Find("DialogText").GetComponent<Text>().text = text;
    }

    // Update is called once per frame
    void Update () {
        if (dialogID >= dialogText.Length) return;

        // Search for nearby player
        bool nearby = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, talkDistance);
        foreach (var playerCollider in hitColliders)
        {
            if (playerCollider.tag == "Player")
            {
                nearby = true;
                break;
            }
        }

        // Start / end talking
        if (nearby) {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!talking)
                {
                    talking = true;
                    dialogTextID = 0;
                } else
                {
                    dialogTextID++;
                    if (dialogTextID == dialogText[dialogID].text.Length) talking = false;
                }

                // Start talking animation if still talking after key pressed
                if (talking)
                {
                    GetComponent<Animator>().SetTrigger("isTalking");
                }
            }
        } else {
            talking = false;
        }

        // Change dialog text field
        if (talking)
        {
            setText(dialogText[dialogID].text[dialogTextID]);
            dialogFade(2.0f);
        } else
        {
            dialogFade(-1.2f);
        }
    }
}
