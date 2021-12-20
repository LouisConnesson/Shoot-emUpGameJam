using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int[] currentReserve;
    private int[] maxReserve;
    private int[] cost;
    private int[] nbAdd;

    public TMP_Text[] reserves;
    public TMP_Text[] costs;
    public TMP_Text points;

    void Start()
    {
        PlayerPrefs.SetInt("MaxWeapon1", 50); //pen
        PlayerPrefs.SetInt("MaxWeapon2", 100); //multi
        PlayerPrefs.SetInt("MaxWeapon3", 50); //frag
        PlayerPrefs.SetInt("MaxWeapon4", 20); //aoe
        PlayerPrefs.SetInt("MaxWeapon5", 10); //aoe2

        PlayerPrefs.SetInt("Current1", 0); //aoe2
        PlayerPrefs.SetInt("Current2", 0); //aoe2
        PlayerPrefs.SetInt("Current3", 0); //aoe2
        PlayerPrefs.SetInt("Current4", 0); //aoe2
        PlayerPrefs.SetInt("Current5", 0); //aoe2


        currentReserve = new int[5] {0,0,0,0,0};
        maxReserve = new int[5] {50,100,50,20,10};
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

    public void BuyWeapon0()
    {
        if(currentReserve[0] < maxReserve[0])
        {
            currentReserve[0] += nbAdd[0];
            int pts = PlayerPrefs.GetInt("Points")-cost[0];
            PlayerPrefs.SetInt("Points", pts);
            PlayerPrefs.SetInt("Current1", currentReserve[0]);

        }
    }
    public void BuyWeapon1()
    {
        if (currentReserve[1] < maxReserve[1])
        {
            currentReserve[1] += nbAdd[1];
            int pts = PlayerPrefs.GetInt("Points") - cost[1];
            PlayerPrefs.SetInt("Points", pts);
            PlayerPrefs.SetInt("Current2", currentReserve[1]);

        }
    }
    public void BuyWeapon2()
    {
        if (currentReserve[2] < maxReserve[2])
        {
            currentReserve[2] += nbAdd[2];
            int pts = PlayerPrefs.GetInt("Points") - cost[2];
            PlayerPrefs.SetInt("Points", pts);
            PlayerPrefs.SetInt("Current3", currentReserve[2]);

        }
    }
    public void BuyWeapon3()
    {
        if (currentReserve[3] < maxReserve[3])
        {
            currentReserve[3] += nbAdd[3];
            int pts = PlayerPrefs.GetInt("Points") - cost[3];
            PlayerPrefs.SetInt("Points", pts);
            PlayerPrefs.SetInt("Current4", currentReserve[3]);

        }
    }
    public void BuyWeapon4()
    {
        if (currentReserve[4] < maxReserve[4])
        {
            currentReserve[4] += nbAdd[4];
            int pts = PlayerPrefs.GetInt("Points") - cost[4];
            PlayerPrefs.SetInt("Points", pts);
            PlayerPrefs.SetInt("Current5", currentReserve[4]);

        }
    }
    
}
