using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBoxUI : MonoBehaviour
{
    [Header("Components")]
    public Image sprBalloon;
    public TextMeshProUGUI txtBalloon;
    public Transform balloonSpike;
    public Animator anim;

    [Header("Dialogue")]
    public bool canInteract = false;
    public bool firstText;
    public static float txtSpeed;
    public string currentDialogue;

    public string CurrentDialogue
    {
        get
        {
            return currentDialogue;
        }
        set
        {
            currentDialogue = value;
            canInteract = false;
            txtBalloon.text = currentDialogue;
            txtBalloon.color = Color.clear;
            if(currentDialogue == "")
            {
                anim.SetBool("isShown", false);
            }
            else
            {
                StartAutoText();
            }
        }
    }

    private void Awake()
    {
        txtSpeed = 0.01f;
        canInteract = true;
        firstText = true;
    }

    void StartAutoText()
    {
        StartCoroutine(IEAutoText());
    }

    IEnumerator IEAutoText()
    {
        if(!firstText)
        {
            anim.SetBool("isShown", false);
            yield return new WaitForSeconds(0.5f);
        }
       
        txtBalloon.color = Color.white;
        txtBalloon.text = "";

        if(currentDialogue != "")
        {
            anim.SetBool("isShown", true);
        }
        yield return new WaitForSeconds(0.5f);

        int spaces=0;
        WaitForSeconds wait = new WaitForSeconds(txtSpeed);
        for(int i = 0; i < currentDialogue.Length; i++)
        {
            txtBalloon.text += currentDialogue[i];
            txtBalloon.ForceMeshUpdate();
            if(currentDialogue[i]==' ')
            {
                spaces++;
                if(spaces==3)
                {
                    txtBalloon.text += "\n";
                    spaces = 0;
                }
            }

            yield return wait;

        }
        
        firstText = false;
    }

}
