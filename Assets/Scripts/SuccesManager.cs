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
        PlayerPrefs.SetInt("Success1",0);
        PlayerPrefs.SetInt("Success2",0);
        PlayerPrefs.SetInt("Success3",0);
        PlayerPrefs.SetInt("Success4",0);
        PlayerPrefs.SetInt("Success5",0);
        PlayerPrefs.SetInt("Success6", 0);
        PlayerPrefs.SetInt("Success7", 0);
        PlayerPrefs.SetInt("Success8", 0);
        PlayerPrefs.SetInt("Success9", 0);


        isOk = new bool[10] { false, false, false, false, false, false, false,false,false,false };
        names = new string[10]{
            "Reussir le monde 1 sans se faire toucher",
            "Reussir le monde 2 sans se faire toucher",
            "Reussir le monde 3 sans se faire toucher",
            "Reussir le monde 4 avec plus de 80% de sa vie",
            "Atteindre un score de 10 000 en survie",
            "Debloquer toutes les armes",
            "Tuer 100 kamikazes",
            "Tuer 100 mobs a torpilles",
            "Vaincre le 1e boss en moins de 20sec",
            "Reussir tous les niveaux avec seulement l'arme de base"

        };

    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0;i<10;i++)
        {
            if (isOk[i])
            {
                print("ok");
                images[i].GetComponent<Image>().sprite = green;
            }
            else
                images[i].GetComponent<Image>().sprite = red;

            namesText[i].text = names[i];
        }
    }
}
