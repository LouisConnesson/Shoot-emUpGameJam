using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public GameObject pause;
    public GameObject gameOver;
    public GameObject victoryScene;
    private bool flag = false;
    [SerializeField]
    private GameObject player;
    public TMP_Text scoreTxt;
    public TMP_Text powerTxt;
    private int score;
    private int power;
    [SerializeField]
    private PlayerController scriptPlayer;
    private int maxHealth;
    private int currentHealth;
    public Slider lifeBar;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        // on ne l'appelle qu'une seul fois
        if (!player && Time.timeScale != 0)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player)
            {
                scriptPlayer = player.GetComponent<PlayerController>();
                maxHealth = scriptPlayer.getmaxHealth();
                lifeBar.value = 1;
            }
        }


        score = scriptPlayer.getPlayerScore();
        scoreTxt.text = $"Score : {score} ";
        power = scriptPlayer.getPlayerPower();
        if (power == 10)
            powerTxt.text = $"Power : MAX ";
        else
            powerTxt.text = $"Power : {power} ";
        currentHealth = scriptPlayer.getmcurrentHealth();
        lifeBar.value = ((float)currentHealth / (float)maxHealth);

        if (lifeBar.value <= 0) //SI LE JOUEUR EST MORT
        {
            gameOver.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && flag == false)
        {
            pause.SetActive(true);
            Time.timeScale = 0;
            flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && flag == true)
        {
            pause.SetActive(false);
            Time.timeScale = 1;
            flag = false;
        }
    }

    public void setVictoryScene()
    {
        victoryScene.SetActive(true);
    }

    public void rerunTime()
    {
        Time.timeScale = 1;
    }
}
