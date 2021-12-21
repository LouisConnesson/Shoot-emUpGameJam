using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private int[] currentReserve; //reserves de munitions
    private int[] maxReserve; //max de munition 
    private int[] cost; //cout d'achat
    private int[] nbAdd; //paquet par achat

    public TMP_Text[] reserves;
    public TMP_Text[] costs;
    public TMP_Text points;

    void Start()
    {
        PlayerPrefs.SetInt("MaxWeapon1", 200); //pen
        PlayerPrefs.SetInt("MaxWeapon2", 200); //multi
        PlayerPrefs.SetInt("MaxWeapon3", 100); //frag
        PlayerPrefs.SetInt("MaxWeapon4", 30); //aoe
        PlayerPrefs.SetInt("MaxWeapon5", 20); //aoe2

        /*PlayerPrefs.SetInt("Current1", 0); 
        PlayerPrefs.SetInt("Current2", 0);
        PlayerPrefs.SetInt("Current3", 0); 
        PlayerPrefs.SetInt("Current4", 0); 
        PlayerPrefs.SetInt("Current5", 0); */


        currentReserve = new int[5];
        currentReserve[0] = PlayerPrefs.GetInt("Current1");
        currentReserve[1] = PlayerPrefs.GetInt("Current2");
        currentReserve[2] = PlayerPrefs.GetInt("Current3");
        currentReserve[3] = PlayerPrefs.GetInt("Current4");
        currentReserve[4] = PlayerPrefs.GetInt("Current5");


        maxReserve = new int[5];
        maxReserve[0] = PlayerPrefs.GetInt("MaxWeapon1");
        maxReserve[1] = PlayerPrefs.GetInt("MaxWeapon2");
        maxReserve[2] = PlayerPrefs.GetInt("MaxWeapon3");
        maxReserve[3] = PlayerPrefs.GetInt("MaxWeapon4");
        maxReserve[4] = PlayerPrefs.GetInt("MaxWeapon5");


        cost = new int[5] {1,1,1,1,1};
        nbAdd = new int[5] {10,20,10,4,1};
        for (int i = 0; i < 5; i++)
        {
            costs[i].text = $"{cost[i]}";

        }
    
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            reserves[i].text = $"{currentReserve[i]}/{maxReserve[i]}";
        }
        points.text = PlayerPrefs.GetInt("Points").ToString();

    }

    //achat arme 0
    public void BuyWeapon0()
    {
        //on verifie qu'on a assez et qu'on est pas deja au max de munitions
        if(currentReserve[0] < maxReserve[0] && PlayerPrefs.GetInt("Points") >= cost[0])
        {
            currentReserve[0] += nbAdd[0];
            int pts = PlayerPrefs.GetInt("Points")-cost[0];
            PlayerPrefs.SetInt("Points", pts);

            if (currentReserve[0] > maxReserve[0])
                currentReserve[0] = maxReserve[0];

            //on sauvegarde
            PlayerPrefs.SetInt("Current1", currentReserve[0]);

        }
    }
    //achat arme 1
    public void BuyWeapon1()
    {
        if (currentReserve[1] < maxReserve[1] && PlayerPrefs.GetInt("Points") >= cost[1])
        {
            currentReserve[1] += nbAdd[1];
            int pts = PlayerPrefs.GetInt("Points") - cost[1];
            PlayerPrefs.SetInt("Points", pts);
            if (currentReserve[1] > maxReserve[1])
                currentReserve[1] = maxReserve[1];

            PlayerPrefs.SetInt("Current2", currentReserve[1]);

        }
    }
    //achat arme 2
    public void BuyWeapon2()
    {
        if (currentReserve[2] < maxReserve[2] && PlayerPrefs.GetInt("Points") >= cost[2])
        {
            currentReserve[2] += nbAdd[2];
            int pts = PlayerPrefs.GetInt("Points") - cost[2];
            PlayerPrefs.SetInt("Points", pts);
            if (currentReserve[2] > maxReserve[2])
                currentReserve[2] = maxReserve[2];

            PlayerPrefs.SetInt("Current3", currentReserve[2]);

        }
    }
    //achat arme 3
    public void BuyWeapon3()
    {
        if (currentReserve[3] < maxReserve[3] && PlayerPrefs.GetInt("Points") >=cost[3])
        {
            currentReserve[3] += nbAdd[3];
            int pts = PlayerPrefs.GetInt("Points") - cost[3];
            PlayerPrefs.SetInt("Points", pts);
            if (currentReserve[3] > maxReserve[3])
                currentReserve[3] = maxReserve[3];

            PlayerPrefs.SetInt("Current4", currentReserve[3]);

        }
    }
    //achat arme 4
    public void BuyWeapon4()
    {
        if (currentReserve[4] < maxReserve[4] && PlayerPrefs.GetInt("Points") >= cost[4])
        {
            currentReserve[4] += nbAdd[4];
            int pts = PlayerPrefs.GetInt("Points") - cost[4];
            PlayerPrefs.SetInt("Points", pts);

            if (currentReserve[4] > maxReserve[4])
                currentReserve[4] = maxReserve[4];

            PlayerPrefs.SetInt("Current5", currentReserve[4]);

        }
    }
    
}
