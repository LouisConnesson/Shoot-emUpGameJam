using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public dialogueOBJ dialogueOBJ;
    public bool isOnDial = false;
    public int currentDialogue;
    public TMP_Text textBox;
    public Image image;

    public void StartDialogue()
    {
        textBox.text = dialogueOBJ.dialogue[currentDialogue];
        image.enabled = true;
        image.sprite = dialogueOBJ.portrait[currentDialogue];
        isOnDial = true;
    }

    public void NextLine()
    {
        if(isOnDial && currentDialogue != dialogueOBJ.dialogue.Length - 1)
        {
            currentDialogue++;
            textBox.text = dialogueOBJ.dialogue[currentDialogue];
            image.sprite = dialogueOBJ.portrait[currentDialogue];
        }
        else if(isOnDial && currentDialogue == dialogueOBJ.dialogue.Length - 1)
        {
            isOnDial = false;
            currentDialogue = 0;
            textBox.text = "";
            image.enabled = false;
        }
    }
}
