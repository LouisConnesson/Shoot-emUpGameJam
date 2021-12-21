using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SuccesManager : MonoBehaviour
{

    private string[] names;
    public bool[] isOk;

    public TMP_Text[] namesText;
    public Image[] images;
    public Sprite green;
    public Sprite red;
    // Start is called before the first frame update
    void Start()
    {

        //liste des succes partages entre menu et scenes
        isOk = new bool[10];
        if (PlayerPrefs.GetInt("Success1") == 1)
            isOk[0] = true;
        else
            PlayerPrefs.SetInt("Success1", 0);


        if (PlayerPrefs.GetInt("Success2") == 1)
            isOk[1] = true;
        else
            PlayerPrefs.SetInt("Success2", 0);


        if (PlayerPrefs.GetInt("Success3") == 1)
            isOk[2] = true;
        else
            PlayerPrefs.SetInt("Success3", 0);


        if (PlayerPrefs.GetInt("Success4") == 1)
            isOk[3] = true;
        else
            PlayerPrefs.SetInt("Success4", 0);


        if (PlayerPrefs.GetInt("Success5") == 1)
            isOk[4] = true;
        else
            PlayerPrefs.SetInt("Success5", 0);


        if (PlayerPrefs.GetInt("Success6") == 1)
            isOk[5] = true;
        else
            PlayerPrefs.SetInt("Success6", 0);


        if (PlayerPrefs.GetInt("Success7") >= 100)
            isOk[6] = true;


        if (PlayerPrefs.GetInt("Success8") >= 100)
            isOk[7] = true;


        if (PlayerPrefs.GetInt("Success9") == 1)
            isOk[8] = true;
        else
            PlayerPrefs.SetInt("Success9", 0);


        if(PlayerPrefs.GetInt("Lv1WP1") == 1 && PlayerPrefs.GetInt("Lv1WP2") == 1 && PlayerPrefs.GetInt("Lv1WP3") == 1 && PlayerPrefs.GetInt("Lv1WP4") == 1)
            PlayerPrefs.SetInt("Success10", 1);

        if (PlayerPrefs.GetInt("Success10") == 1)
            isOk[9] = true;
        else
            PlayerPrefs.SetInt("Success10", 0);

        names = new string[10]{
            "Reussir le monde 1 sans se faire toucher",
            "Reussir le monde 2 sans se faire toucher",
            "Reussir le monde 3 sans se faire toucher",
            "Reussir le monde 4 avec plus de 80% de sa vie",
            "Atteindre un score de 10 000 en survie",
            "Debloquer toutes les armes",
            $"Tuer 100 kamikazes {PlayerPrefs.GetInt("Success7")}/100",
            $"Tuer 100 mobs a torpilles {PlayerPrefs.GetInt("Success8")}/100",
            "Vaincre le 1e boss en moins de 20sec",
            "Reussir tous les niveaux avec seulement l'arme de base"

        };
    }
    // mis a jour des succes
    void Update()
    {
        names[6] = $"Tuer 100 kamikazes {PlayerPrefs.GetInt("Success7")}/100";
        names[7] = $"Tuer 100 mobs a torpilles {PlayerPrefs.GetInt("Success8")}/100";
        

        for (int i =0;i<10;i++)
        {
            if (isOk[i])
            {
                images[i].GetComponent<Image>().sprite = green;
            }
            else
                images[i].GetComponent<Image>().sprite = red;

            namesText[i].text = names[i];
        }
    }
}
