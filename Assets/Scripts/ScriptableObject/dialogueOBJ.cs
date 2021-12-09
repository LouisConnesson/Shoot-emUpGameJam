using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/dialogueOBJ", order = 1)]
public class dialogueOBJ : ScriptableObject
{
    public string[] dialogue;
    public Sprite[] portrait;
}
