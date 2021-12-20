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
    private bool flagGura = false;
    private bool flagChan = false;
    private int waveFlag = 0;
    public Transform target;
    private bool foundPlayer = false;

    public Dialogue dialogue;
    public Image imgFont;

    PlayerController player;
    public MusicManager zicManager;
    public Dialogue dialogueGura;
    public Dialogue dialogueChan;
    public Dialogue dialogueChanEsquive;
    public UserInterface userInterface;
    private int level = 1;
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
        if (level > 0)
            StartCoroutine(SpawnerWaveKamikaze());

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
            if (level == 0)
            {
                timer.Start();

                if (timer.ElapsedMilliseconds >= 30000 && flag == false) //////////////////////BOSS
                {
                    StopAllCoroutines();
                    Time.timeScale = 0;
                    dialogue.StartDialogue(); // On commence le dialogue du boss
                    EnemyBossBody m = Instantiate(BossBody) as EnemyBossBody;
                    m.Initalize(player, zicManager, userInterface);
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

                /*if (timer.ElapsedMilliseconds >= 50000 && flag == false && flagChan == false) //////////////////////BOSS
                {
                    StopAllCoroutines();
                    EnemyBossChan chan = Instantiate(BossChan) as EnemyBossChan;
                    chan.Initalize(player, dialogueChan, dialogueChanEsquive, imgFont, zicManager, userInterface);
                    chan.transform.position = new Vector3(2, 25, -5);
                    flagChan = true;
                }*/
            }
            else if (level == 1)
            {
                timer.Start();

                if (timer.ElapsedMilliseconds >= 0 && flag == false && flagGura == false) //////////////////////BOSS
                {
                    StopAllCoroutines();
                    GameObject guramob = Instantiate(gura) as GameObject;
                    guramob.transform.GetChild(0).gameObject.GetComponent<BossGura>().Initalize(player,dialogueGura, imgFont,userInterface);
                    //guramob.GetComponent<BossGura>().Initalize(player);
                    guramob.transform.position = new Vector3(-7, 19, -5);
                    flagGura = true;

                }
            }
            else if (level == 2)
            {
                timer.Start();

                if (timer.ElapsedMilliseconds >= 30000 && flagChan == false) //////////////////////BOSS
                {
                    StopAllCoroutines();
                    EnemyBossChan chan = Instantiate(BossChan) as EnemyBossChan;
                    chan.Initalize(player, dialogueChan, dialogueChanEsquive, imgFont, zicManager, userInterface);
                    chan.transform.position = new Vector3(2, 25, -5);
                    flagChan = true;
                }
            }
        }
        else if (flag == true && Input.GetKeyDown(KeyCode.Space)) //si le boss a pop ET qu'on est en dialogue
        {
            dialogue.NextLine();
            if (dialogue.currentDialogue == 0) //si le dialogue est finit on remet le temps
            {
                print("4");
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
        if ((level != 0 && Random.Range(0f, 100f) < 50) || level == 0) //Probabilité de faire apparaitre un type de mob
        {
            Enemy m = Instantiate(Mob) as Enemy;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);
        }
        else if ((level != 1 && Random.Range(0f, 100f) < 40 )|| level == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                EnemyKamikaze m = Instantiate(MobKamikaze) as EnemyKamikaze;
                m.Initalize(player);
                m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);
            }
        }
        else
        {  
            EnemyTorp m = Instantiate(MobTorp) as EnemyTorp;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);

        }
    }
    private void spawnWave()
    {
        if (waveFlag == 0)
        {
            if ((level == 2 && Random.Range(0f, 100f) < 50) || level < 2) //Probabilité de faire apparaitre un type de mob
            {
                for (int i = 0; i < 2; i++)
                {
                    Enemy m = Instantiate(Mob) as Enemy;
                    m.Initalize(player);
                    m.transform.position = new Vector3(-6 + i * 11, 20 , -2.8F);
                }
                for (int i = 0; i < 2; i++)
                {
                    Enemy m = Instantiate(Mob) as Enemy;
                    m.Initalize(player);
                    m.transform.position = new Vector3(-6 + i * 11, 25, -2.8F);
                }
                Enemy m2 = Instantiate(Mob) as Enemy;
                m2.Initalize(player);
                m2.transform.position = new Vector3(-0.5f, 22.5f, -2.8F);
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    EnemyTorp m = Instantiate(MobTorp) as EnemyTorp;
                    m.Initalize(player);
                    m.transform.position = new Vector3(-6 + i * 11, 20, -2.8F);
                }
                for (int i = 0; i < 2; i++)
                {
                    EnemyTorp m = Instantiate(MobTorp) as EnemyTorp;
                    m.Initalize(player);
                    m.transform.position = new Vector3(-6 + i * 11, 25, -2.8F);
                }
                EnemyTorp m2 = Instantiate(MobTorp) as EnemyTorp;
                m2.Initalize(player);
                m2.transform.position = new Vector3(-0.5f, 22.5f, -2.8F);
            }
            waveFlag++;
        }
        else if (waveFlag == 1)
        {
            if ((level == 2 && Random.Range(0f, 100f) < 50) || level < 2) //Probabilité de faire apparaitre un type de mob
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
        else if (waveFlag == 2)
        {
            if ((level == 2 && Random.Range(0f, 100f) < 50) || level <2) //Probabilité de faire apparaitre un type de mob
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

    private void spawnWaveKamikaze()
    {
        for (int i = 0; i < 5; i++)
        {
            EnemyKamikaze m = Instantiate(MobKamikaze) as EnemyKamikaze;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), Random.Range(20,25), -5);
        }
    }
        IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            spawnEnemy();
        }
    }
    IEnumerator SpawnerWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(12.0f);
            spawnWave();
        }
    }
    IEnumerator SpawnerWaveKamikaze()
    {
        while (true)
        {
            yield return new WaitForSeconds(6.0f);
            spawnWaveKamikaze();
        }
    }
}
