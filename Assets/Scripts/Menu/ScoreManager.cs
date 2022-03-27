using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text[] scores;

    void Start()
    {
        /*PlayerPrefs.SetInt("Score1", 0);
        PlayerPrefs.SetInt("Score2", 0);
        PlayerPrefs.SetInt("Score3", 0);
        PlayerPrefs.SetInt("Score4", 0);
        PlayerPrefs.SetInt("Score5", 0);
        PlayerPrefs.SetInt("Score6", 0);
        PlayerPrefs.SetInt("Score7", 0);
        PlayerPrefs.SetInt("Score8", 0);
        PlayerPrefs.SetInt("Score9", 0);
        PlayerPrefs.SetInt("Score10", 0);*/

        //on recupere les scores et on les affiche
        scores[9].text = PlayerPrefs.GetInt("Score1").ToString();
        scores[8].text = PlayerPrefs.GetInt("Score2").ToString();
        scores[7].text = PlayerPrefs.GetInt("Score3").ToString();
        scores[6].text = PlayerPrefs.GetInt("Score4").ToString();
        scores[5].text = PlayerPrefs.GetInt("Score5").ToString();
        scores[4].text = PlayerPrefs.GetInt("Score6").ToString();
        scores[3].text = PlayerPrefs.GetInt("Score7").ToString();
        scores[2].text = PlayerPrefs.GetInt("Score8").ToString();
        scores[1].text = PlayerPrefs.GetInt("Score9").ToString();
        scores[0].text = PlayerPrefs.GetInt("Score10").ToString();
    }


}
