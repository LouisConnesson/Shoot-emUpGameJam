using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    public int[] skillLevels;
    public int[] skillCaps;
    public int[] skillCost;
    public string[] skillNames;
    public string[] skillDescriptions;

    public List<Skill> skillList;
    public GameObject skillManager;

    public List<GameObject> connectorList;
    public GameObject connectorManager;

    public int SkillPoint;
    private float shieldTime;

    public TMP_Text skillPointsText;
    public TMP_Text[] bonusText;
    // Start is called before the first frame update
    void Start()
    {
        SkillPoint = 300;
        skillLevels = new int[7] { 0, 0, 0, 0, 0 ,0,0};
        skillCaps = new[] {4,5,5,2,10,10,4 };
        skillCost = new[] {1,3,5,10,15,20,30};
        skillNames = new[]
        {
            "Balles simples",
            "Balles p�n�trantes",
            "Multi tir",
            "Balles � fragmentation",
            "Balles � d�gats de zone",
            "Plasma",
            "Bouclier"
        };

        skillDescriptions = new[]
        {
            "Balles sans effets ayant de bon d�gats",
            "Balles pouvant travers plusieurs ennemis",
            "Tir plusieurs balles en m�me temps",
            "Cr�� une multitude de balles � la mort d'un ennemi",
            "Cr�� une onde de choc au contact d'un ennemi",
            "Cr�� une �norme explosion si touch�e par un projectile alli� ou ennemi",
            "Bouclier de plasma rendant invuln�rable pendant quelques secondes"

        };
        foreach(var skill in skillManager.GetComponentsInChildren<Skill>())
            skillList.Add(skill);

        foreach (var connector in connectorManager.GetComponentsInChildren<RectTransform>())
            connectorList.Add(connector.gameObject);


        for (int i = 0; i < skillList.Count; i++)
            skillList[i].id = i;

        skillList[0].connectedSkills = new[] { 1,2,6};
        //skillList[1].connectedSkills = new[] {5};
        skillList[2].connectedSkills = new[] {3};
        skillList[3].connectedSkills = new[]  {4};
        skillList[4].connectedSkills = new[] {5};
   

        UpdateAllSkillUI();
        PlayerPrefs.SetInt("MainWeaponWeaponLevel01", 0);
        PlayerPrefs.SetInt("MainWeaponWeaponLevel02", 0);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel01", 0);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel02", 0);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel03", 0);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel04", 0);
        PlayerPrefs.SetInt("Shield", 0);



    }

    // Update is called once per frame
    void Update()
    {
        skillPointsText.text = $"Points : {SkillPoint}"; 

        bonusText[0].text = $"+{skillLevels[0]} atq";
        bonusText[1].text = $"+{skillLevels[1]} atq";
        bonusText[2].text = $"+{skillLevels[2]} atq";
        bonusText[3].text = $"+{skillLevels[3]} atq";
        bonusText[4].text = $"+{skillLevels[4]} atq";
        bonusText[5].text = $"+{skillLevels[5]} atq";
        bonusText[6].text = $"+{skillLevels[6]/2}s";

        PlayerPrefs.SetInt("MainWeaponWeaponLevel01", skillLevels[0]);
        PlayerPrefs.SetInt("MainWeaponWeaponLevel02", skillLevels[1]);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel01", skillLevels[2]);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel02", skillLevels[3]);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel03", skillLevels[4]);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel04", skillLevels[5]);
        PlayerPrefs.SetInt("Shield", skillLevels[6]);

    }
    public void UpdateAllSkillUI()
    {
        foreach(var skill in skillList)
        {
            skill.UpdateUI();
        }
    }
}
