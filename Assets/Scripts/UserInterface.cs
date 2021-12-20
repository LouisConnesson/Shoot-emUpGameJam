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
    private int score;
    [SerializeField]
    private PlayerController scriptPlayer;
    private int maxHealth;
    private int currentHealth;
    public Slider lifeBar;
    public Slider ammoPR;
    public Slider ammoSD;

    public float test;
    // Start is called before the first frame update

    private void Start()
    {
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
    }
    // Update is called once per frame
    void Update()
    {
        // on ne l'appelle qu'une seul fois
        if (player)
        {
            if (player.GetComponent<PlayerController>().main_id == 0)
                ammoPR.value = 1;
            if (player.GetComponent<PlayerController>().main_id == 1)
                ammoPR.value = (float)(PlayerPrefs.GetInt("Current1") / (float)PlayerPrefs.GetInt("MaxWeapon1"));

            if (player.GetComponent<PlayerController>().second_id == 0)
                ammoSD.value = 0;
            if (player.GetComponent<PlayerController>().second_id == 1)
                ammoSD.value = (float)(PlayerPrefs.GetInt("Current2") / (float)PlayerPrefs.GetInt("MaxWeapon2"));
            if (player.GetComponent<PlayerController>().second_id == 2)
                ammoSD.value = (float)(PlayerPrefs.GetInt("Current3") / (float)PlayerPrefs.GetInt("MaxWeapon3"));
            if (player.GetComponent<PlayerController>().second_id == 3)
                ammoSD.value = (float)(PlayerPrefs.GetInt("Current4") / (float)PlayerPrefs.GetInt("MaxWeapon4"));
            if (player.GetComponent<PlayerController>().second_id == 4)
                ammoSD.value = (float)(PlayerPrefs.GetInt("Current5") / (float)PlayerPrefs.GetInt("MaxWeapon5"));

        }



        score = scriptPlayer.getPlayerScore();
        scoreTxt.text = $"Score : {score} ";
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
