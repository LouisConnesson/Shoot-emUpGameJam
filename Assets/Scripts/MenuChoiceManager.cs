using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool isMenu = true;
    public SkillTree skillTree;
    // Start is called before the first frame update
    void Start()
    {
        //starship
        starships = GameObject.FindGameObjectsWithTag("Starship");
        isStarshipEnable = new bool[starships.Length];
        id_starship = 0;
        PlayerPrefs.SetInt("Starship", 0);

        //main weapon
        isMainWeaponEnable = new bool[2];
        id_mainWeapon = 0;
        PlayerPrefs.SetInt("MainWeapon", 0);
        //second weapon
        isSecondWeaponEnable = new bool[5];
        id_secondWeapon = 0;
        PlayerPrefs.SetInt("SecondWeapon", 0);

        isMainWeaponAvailable = new bool[2] { true, false };
        isisSecondWeaponAvailable = new bool[5] { true, false, false, false, false };
    }

    // Update is called once per frame
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
    }
    public void setMenu()
    {
        isMenu = !isMenu;
        isStarshipEnable[id_starship] = !isMenu;
    }
    public void SelectSecondWeapon()
    {
        for (int i = 0; i < isSecondWeaponEnable.Length; i++)
        {
            if (isSecondWeaponEnable[i])
                currentSecondWeapon.sprite = secondWeaponSprites[i];

        }

    }
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
                }
                else
                {
                    starships[i].GetComponent<MeshRenderer>().enabled = false;

                }
            }
            foreach (GameObject starship in starships)
            {
                starship.transform.RotateAround(starship.transform.position, new Vector3(0, 1, 0), 1f);
            }
        }
        
    }
    public void RightClickStarship()
    {
        isStarshipEnable[id_starship] = false;
        id_starship = (id_starship + 1) % (isStarshipEnable.Length);
        isStarshipEnable[id_starship] = true;
        PlayerPrefs.SetInt("Starship", id_starship);

    }
    public void LeftClickStarship()
    {
        isStarshipEnable[id_starship] = false;
        id_starship = (id_starship - 1) % (isStarshipEnable.Length);
        if (id_starship < 0)
            id_starship = isStarshipEnable.Length-1;
        isStarshipEnable[id_starship] = true;
        PlayerPrefs.SetInt("Starship", id_starship);

    }

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

        //isMainWeaponEnable[id_mainWeapon] = false;
        //isMainWeaponEnable[id_mainWeapon] = true;
    }

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
        /* isMainWeaponEnable[id_mainWeapon] = false;
         id_mainWeapon = (id_mainWeapon -1) % (isMainWeaponEnable.Length);
         if (id_mainWeapon < 0)
             id_mainWeapon = isMainWeaponEnable.Length - 1;
         isMainWeaponEnable[id_mainWeapon] = true;
         PlayerPrefs.SetInt("MainWeapon", id_mainWeapon);*/
  
    }

    //second weapon
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
        /*isSecondWeaponEnable[id_secondWeapon] = false;
        id_secondWeapon = (id_secondWeapon + 1) % (isSecondWeaponEnable.Length);
        isSecondWeaponEnable[id_secondWeapon] = true;*/
    }

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
        /*isSecondWeaponEnable[id_secondWeapon] = false;
        id_secondWeapon = (id_secondWeapon - 1) % (isSecondWeaponEnable.Length);
        if (id_secondWeapon < 0)
            id_secondWeapon = isSecondWeaponEnable.Length - 1;
        isSecondWeaponEnable[id_secondWeapon] = true;*/
    }
}

