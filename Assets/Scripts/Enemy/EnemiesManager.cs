using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;


public class EnemiesManager : MonoBehaviour
{
    public GameObject gura;
    public Enemy Mob;
    public EnemyKamikaze MobKamikaze;
    public EnemyTorp MobTorp;
    public EnemyBossBody BossBody;
    public EnemyBossShield BossShield;
    public EnemyBossShieldMaker BossShieldMaker;
    public EnemyBossShieldMaker BossShieldMakerRight;
    public EnemyBossChan BossChan;

    private Stopwatch timer;
    private bool flag = false;
    private bool flagChan = false;
    private int waveFlag = 0;
    public Transform target;
    private bool foundPlayer = false;

    public Dialogue dialogue;
    public Image imgFont;

    PlayerController player;
    public MusicManager zicManager;
    public Dialogue dialogueChan;
    public Dialogue dialogueChanEsquive;
    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
        StartCoroutine(SpawnerWave());

    }

    // Update is called once per frame
    void Update()
    {
        if (!player && foundPlayer == false)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (player)
                foundPlayer = true;
        }
        if (Time.timeScale == 1 && player) 
        {
            timer.Start();
            if (Random.Range(0f, 100f) < 0) //Probabilité de faire apparaitre un type de boss
            {
                if (timer.ElapsedMilliseconds >= 50000 && flag == false && flagChan == false) //////////////////////BOSS
                {
                    StopAllCoroutines();
                    Time.timeScale = 0;
                    dialogue.StartDialogue(); // On commence le dialogue du boss
                    EnemyBossBody m = Instantiate(BossBody) as EnemyBossBody;
                    m.Initalize(player, zicManager);
                    m.transform.position = new Vector3(0, 25, -5);
                    EnemyBossShield n = Instantiate(BossShield) as EnemyBossShield;
                    n.Initalize(player);
                    n.transform.position = new Vector3(0, 25, -5);
                    EnemyBossShieldMaker o = Instantiate(BossShieldMaker) as EnemyBossShieldMaker;
                    o.Initalize(player);
                    o.ShieldEvent(n);
                    o.transform.position = new Vector3(7, 19, -5);
                    EnemyBossShieldMaker o2 = Instantiate(BossShieldMakerRight) as EnemyBossShieldMaker;
                    o2.Initalize(player);
                    o2.ShieldEvent(n);
                    n.NoShieldEvent(m);
                    o2.transform.position = new Vector3(-7, 19, -5);
                    flag = true;
                    imgFont.enabled = true;
                }
                /*
                GameObject guramob = Instantiate(gura) as GameObject;
                guramob.transform.GetChild(0).gameObject.GetComponent<BossGura>().Initalize(player);
                //guramob.GetComponent<BossGura>().Initalize(player);
                guramob.transform.position = new Vector3(-7, 19, -5);*/
            }
            else if (flagChan == false && flag == false) //////////////////////BOSS CHAN
            {
                if (timer.ElapsedMilliseconds >= 1000 && flag == false && flagChan == false) //////////////////////BOSS
                {
                    StopAllCoroutines();
                    /*EnemyBossChan chan = Instantiate(BossChan) as EnemyBossChan;
                    chan.Initalize(player, dialogueChan, dialogueChanEsquive, imgFont, zicManager);
                    chan.transform.position = new Vector3(2, 25, -5);*/
                    flagChan = true; 
                    GameObject guramob = Instantiate(gura) as GameObject;
                    guramob.transform.GetChild(0).gameObject.GetComponent<BossGura>().Initalize(player);
                    //guramob.GetComponent<BossGura>().Initalize(player);
                    guramob.transform.position = new Vector3(-7, 19, -5);
                }
            }//////////////////////////BOSS CHAN
        }
        else if (flag == true && Input.GetKeyDown(KeyCode.Space)) //si le boss a pop ET qu'on est en dialogue
        {
            dialogue.NextLine();
            if (dialogue.currentDialogue == 0) //si le dialogue est finit on remet le temps
            {
                Time.timeScale = 1;
                imgFont.enabled = false;
            }
        }
        else //si le temps est arrêté on stop le temps du boss
        {
            timer.Stop();
        }
    }
    private void spawnEnemy()
    {
        if (Random.Range(0f, 100f) < 10) //Probabilité de faire apparaitre un type de mob
        {
            Enemy m = Instantiate(Mob) as Enemy;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);
        }
        else
        {
            /*EnemyTorp m = Instantiate(MobTorp) as EnemyTorp;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5); */

           EnemyKamikaze m = Instantiate(MobKamikaze) as EnemyKamikaze;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);
        }
    }
    private void spawnWave()
    {
        if (waveFlag == 0)
        {
            if (Random.Range(0f, 100f) < 50) //Probabilité de faire apparaitre un type de mob
            {
                for (int i = 0; i < 3; i++)
                {
                    Enemy m = Instantiate(Mob) as Enemy;
                    m.Initalize(player);
                    m.transform.position = new Vector3(-5 + i * 4, 20 + i * 2, -2.8F);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    EnemyTorp m = Instantiate(MobTorp) as EnemyTorp;
                    m.Initalize(player);
                    m.transform.position = new Vector3(-5 + i * 4, 20 + i * 2, -2.8F);
                }
            }
            waveFlag++;
        }
        else if (waveFlag == 1)
        {
            if (Random.Range(0f, 100f) < 50) //Probabilité de faire apparaitre un type de mob
            {
                for (int i = 0; i < 5; i++)
                {
                    Enemy m = Instantiate(Mob) as Enemy;
                    m.Initalize(player);
                    m.transform.position = new Vector3(-8.5f + i * 4, 20, -5);
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    EnemyTorp m = Instantiate(MobTorp) as EnemyTorp;
                    m.Initalize(player);
                    m.transform.position = new Vector3(-8.5f + i * 4, 20, -5);
                }
            }
            waveFlag = 0;
        }
    }
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.0f);
            spawnEnemy();
        }
    }
    IEnumerator SpawnerWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            spawnWave();
        }
    }
}
