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

    public TMP_Text skillPointsText;
    // Start is called before the first frame update
    void Start()
    {
        SkillPoint = 100;
        skillLevels = new int[6] { 0, 0, 0, 0, 0 ,0};
        skillCaps = new[] {4,5,5,2,10,10 };
        skillCost = new[] {1,3,5,10,15,20};

        skillNames = new[]
        {
            "Balles simples",
            "Balles pénétrantes",
            "Multi tir",
            "Balles à fragmentation",
            "Balles à dégats de zone",
            "Plasma",
        };

        skillDescriptions = new[]
        {
            "Balles sans effets ayant de bon dégats",
            "Balles pouvant travers plusieurs ennemis",
            "Tir plusieurs balles en même temps",
            "Créé une multitude de balles à la mort d'un ennemi",
            "Créé une onde de choc au contact d'un ennemi",
            "Créé une énorme explosion si touchée par un projectile allié ou ennemi",

        };
        foreach(var skill in skillManager.GetComponentsInChildren<Skill>())
            skillList.Add(skill);

        foreach (var connector in connectorManager.GetComponentsInChildren<RectTransform>())
            connectorList.Add(connector.gameObject);


        for (int i = 0; i < skillList.Count; i++)
            skillList[i].id = i;

        skillList[0].connectedSkills = new[] { 1,2};
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



    }

    // Update is called once per frame
    void Update()
    {
        string points = "Points : " + SkillPoint.ToString();
        skillPointsText.text = $"Points : {SkillPoint}";

        PlayerPrefs.SetInt("MainWeaponWeaponLevel01", skillLevels[0]);
        PlayerPrefs.SetInt("MainWeaponWeaponLevel02", skillLevels[1]);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel01", skillLevels[2]);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel02", skillLevels[3]);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel03", skillLevels[4]);
        PlayerPrefs.SetInt("SecondWeaponWeaponLevel04", skillLevels[5]);

    }
    public void UpdateAllSkillUI()
    {
        foreach(var skill in skillList)
        {
            skill.UpdateUI();
        }
    }
}
