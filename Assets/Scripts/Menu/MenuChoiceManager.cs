using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuChoiceManager : MonoBehaviour
{
    //starships
    public GameObject[] starships;
    public bool[] isStarshipEnable;
    public int id_starship;

    //main weapons
    public int mainWeaponsNumber = 2;
    public Sprite[] mainWeaponSprites = new Sprite[2];
    public Image currentMainWeapon;
    public bool[] isMainWeaponEnable;
    public bool[] isMainWeaponAvailable;
    public int id_mainWeapon;

    //secondary weapon
    public int secondaryWeaponsNumber = 5;
    public Sprite[] secondWeaponSprites = new Sprite[4];
    public Image currentSecondWeapon;
    public bool[] isSecondWeaponEnable;
    public int id_secondWeapon;
    public bool[] isisSecondWeaponAvailable;

    //arbre de talent
    public bool isMenu = true;
    public SkillTree skillTree;

    //textes, images
    public string[] weaponsNamePr;
    public string[] weaponsNameSd;

    public TMP_Text weaponNamePrTMP;
    public TMP_Text weaponNameSdTMP;

    public Image currentPilote;
    public Sprite[] pilotes;
    void Start()
    {
        //on recuperer le joueur choisi a travers le menu et les scenes
        //starship
        isStarshipEnable = new bool[starships.Length];
        id_starship = PlayerPrefs.GetInt("Starship");

        //main weapon
        isMainWeaponEnable = new bool[2];
        id_mainWeapon = PlayerPrefs.GetInt("MainWeapon");
        //second weapon
        isSecondWeaponEnable = new bool[5];
        id_secondWeapon = PlayerPrefs.GetInt("SecondWeapon");

        //permet de savoir si les armes sont debloquees ou non 
        isMainWeaponAvailable = new bool[2] { true, false };
        isisSecondWeaponAvailable = new bool[5] { true, false, false, false, false };

        weaponsNamePr = new string[2] { "Balles simples (Munitions infinies)", "Balles penetrantes" };
        weaponsNameSd = new string[5] {"", "Multi tir", 
            "Balles a fragmentation",
            "Balles a degats de zone",
            "Plasma",
            };

        //sprite des images a afficher
        currentMainWeapon.sprite = mainWeaponSprites[id_mainWeapon];
        currentSecondWeapon.sprite = secondWeaponSprites[id_secondWeapon];
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
            if (skillTree.skillLevels[i] > 0)
                isMainWeaponAvailable[i] = true;

        for (int i = 1; i < 5; i++)
            if (skillTree.skillLevels[i + 1] > 0)
                isisSecondWeaponAvailable[i] = true;

        RotateStarship();
        SelectMainWeapon();
        SelectSecondWeapon();

        weaponNamePrTMP.text = weaponsNamePr[id_mainWeapon];
        weaponNameSdTMP.text = weaponsNameSd[id_secondWeapon];
    }
    //affiche ou non les vaisseaux si on est dans le menu equipement
    public void setMenu()
    {
        isMenu = !isMenu;
  
        for (int i = 0; i < 3; i++)
            if(isMenu)
                starships[i].SetActive(false);
            else
                starships[i].SetActive(true);

        isStarshipEnable[id_starship] = !isMenu;
    }
    //selection de l'arme secondaire
    public void SelectSecondWeapon()
    {
        for (int i = 0; i < isSecondWeaponEnable.Length; i++)
        {
            if (isSecondWeaponEnable[i])
                currentSecondWeapon.sprite = secondWeaponSprites[i];

        }

    }
    //selection de l'arme principale
    public void SelectMainWeapon()
    {
        for (int i = 0; i < isMainWeaponEnable.Length; i++)
        {
            if (isMainWeaponEnable[i])
            {
                currentMainWeapon.sprite = mainWeaponSprites[i];
            }

        }

    }
    //tourne les vaisseaux sur eux memes
    public void RotateStarship()
    {
        if(isMenu)
            for (int i = 0; i < isStarshipEnable.Length; i++)
                starships[i].GetComponent<MeshRenderer>().enabled = false;
        else
        {
            for (int i = 0; i < isStarshipEnable.Length; i++)
            {
                if (isStarshipEnable[i])
                {
                    starships[i].GetComponent<MeshRenderer>().enabled = true;
                    currentPilote.sprite = pilotes[i];
                }
                else
                {
                    starships[i].GetComponent<MeshRenderer>().enabled = false;

                }
            }
            foreach (GameObject starship in starships)
            {
                starship.transform.RotateAround(starship.transform.position, new Vector3(0, 1, 0), 0.3f);
            }
        }
        
    }
    //boutton de selection des vaisseaux
    public void RightClickStarship()
    {
        isStarshipEnable[id_starship] = false;
        id_starship = (id_starship + 1) % (isStarshipEnable.Length);
        isStarshipEnable[id_starship] = true;
        PlayerPrefs.SetInt("Starship", id_starship);

    }
    //boutton de selection des vaisseaux
    public void LeftClickStarship()
    {
        isStarshipEnable[id_starship] = false;
        id_starship = (id_starship - 1) % (isStarshipEnable.Length);
        if (id_starship < 0)
            id_starship = isStarshipEnable.Length-1;
        isStarshipEnable[id_starship] = true;
        PlayerPrefs.SetInt("Starship", id_starship);

    }
    //boutton de selection de l'arme principale
    public void mainWeaponRightClick()
    {
        
        int old_id = id_mainWeapon;
        id_mainWeapon = (id_mainWeapon + 1) % (isMainWeaponEnable.Length);
        if(isMainWeaponAvailable[id_mainWeapon])
        {
            isMainWeaponEnable[old_id] = false;
            isMainWeaponEnable[id_mainWeapon] = true;
        }
        else
        {
            id_mainWeapon = old_id;
        }
        PlayerPrefs.SetInt("MainWeapon", id_mainWeapon);
    }

    //boutton de selection de l'arme principale
    public void mainWeaponleftClick()
    {

        int old_id = id_mainWeapon;
        id_mainWeapon = (id_mainWeapon + 1) % (isMainWeaponEnable.Length);
        if (id_mainWeapon < 0)
            id_mainWeapon = isMainWeaponEnable.Length - 1;

        if (isMainWeaponAvailable[id_mainWeapon])
        {
            isMainWeaponEnable[old_id] = false;
            isMainWeaponEnable[id_mainWeapon] = true;
        }
        else
        {
            id_mainWeapon = old_id;
        }
        PlayerPrefs.SetInt("MainWeapon", id_mainWeapon);  
    }

    //boutton de selection de l'arme secondaire
    public void secondWeaponRightClick()
    {
        int old_id = id_secondWeapon;
        id_secondWeapon = (id_secondWeapon + 1) % (isisSecondWeaponAvailable.Length);
        if (isisSecondWeaponAvailable[id_secondWeapon])
        {
            isSecondWeaponEnable[old_id] = false;
            isSecondWeaponEnable[id_secondWeapon] = true;
        }
        else
        {
            print("ca marche pas");

            id_secondWeapon = old_id;
        }

        PlayerPrefs.SetInt("SecondWeapon", id_secondWeapon);
       
    }

    //boutton de selection de l'arme secondaire
    public void secondWeaponleftClick()
    {
        int old_id = id_secondWeapon;
        id_secondWeapon = (id_secondWeapon - 1) % (isisSecondWeaponAvailable.Length);
        if (id_secondWeapon < 0)
            id_secondWeapon = isisSecondWeaponAvailable.Length - 1;

        if (isisSecondWeaponAvailable[id_secondWeapon])
        {
            isSecondWeaponEnable[old_id] = false;
            isSecondWeaponEnable[id_secondWeapon] = true;
        }
        else
        {
            print("ca marche pas");
            id_secondWeapon = old_id;
        }

        PlayerPrefs.SetInt("SecondWeapon", id_secondWeapon);
       
    }
}

