using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static SkillTree;

public class Skill : MonoBehaviour
{
    public int id;
    public TMP_Text pointsText;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text costText;
    public Image filtre;

    public int[] connectedSkills;
    // Start is called before the first frame update
    public void UpdateUI()
    {
        pointsText.text = $"{skillTree.skillLevels[id]}/{skillTree.skillCaps[id]}";
        titleText.text = $"{skillTree.skillNames[id]}";
        descriptionText.text = $"{skillTree.skillDescriptions[id]}";
        costText.text = $"-{skillTree.skillCost[id]}";

        //couleur des cases selon le niveau d'achat
        Color colorBlue;
        Color colorWhite;
        Color colorGreen;
        ColorUtility.TryParseHtmlString("#363196", out colorBlue);
        ColorUtility.TryParseHtmlString("#A09FBA", out colorWhite);
        ColorUtility.TryParseHtmlString("#52A952", out colorGreen);
        colorBlue.a = 0.8f;
        colorWhite.a = 0.8f;
        colorGreen.a = 0.8f;

        GetComponent<Image>().color = skillTree.skillLevels[id] >= skillTree.skillCaps[id] ? colorBlue :
            PlayerPrefs.GetInt("Points") > skillTree.skillCost[id] ? colorGreen : colorWhite;

        //couleur connecteurs
        Color blackColor;
        ColorUtility.TryParseHtmlString("#131313", out blackColor);
        blackColor.a = 0.8f;

        Color whiteColor;
        ColorUtility.TryParseHtmlString("#FFFFFF", out whiteColor);
        whiteColor.a = 0.8f;

        foreach (var connectedSkill in connectedSkills)
        {
            if(skillTree.skillLevels[id] > 0)
            {
                skillTree.skillList[connectedSkill].filtre.enabled = false;
                skillTree.connectorList[connectedSkill].GetComponent<Image>().color = whiteColor;
            }
            else
                skillTree.connectorList[connectedSkill].GetComponent<Image>().color = blackColor;

        }
    }
    //achat d'un skill
    public void Buy()
    {
        //si pas assez
        if (skillTree.skillLevels[id] >= skillTree.skillCaps[id] || PlayerPrefs.GetInt("Points") < skillTree.skillCost[id])
            return;

        int pts = PlayerPrefs.GetInt("Points") - skillTree.skillCost[id];
        PlayerPrefs.SetInt("Points", pts);
        skillTree.skillLevels[id] += 1;
        skillTree.UpdateAllSkillUI();
    }
}
