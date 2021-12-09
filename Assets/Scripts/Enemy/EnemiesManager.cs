using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    public Enemy Mob;
    public EnemyKamikaze MobKamikaze;
    public EnemyTorp MobTorp;
    public EnemyBossBody BossBody;
    public EnemyBossShield BossShield;
    public EnemyBossShieldMaker BossShieldMaker;
    public EnemyBossShieldMaker BossShieldMakerRight;
    private Stopwatch timer;
    private bool flag = false;
    private int waveFlag = 0;
    public Transform target;

    public Dialogue dialogue;
    public Image imgFont;

    [SerializeField]
    PlayerController player;
    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
<<<<<<< HEAD
        if (Time.timeScale == 1)
        {
            timer.Start();
            if (timer.ElapsedMilliseconds >= 5000 & flag == false)
            {
                StopAllCoroutines();
                Time.timeScale = 0;
                dialogue.StartDialogue(); // On commence le dialogue du boss
=======
        if (timer.ElapsedMilliseconds >= 30000 & flag == false)
        {
            StopAllCoroutines();
            EnemyBossBody m = Instantiate(BossBody) as EnemyBossBody;
            m.Initalize(player);
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
>>>>>>> origin/main

                EnemyBossBody m = Instantiate(BossBody) as EnemyBossBody;
                m.Initalize(player);
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
                o2.transform.position = new Vector3(-7, 19, -5);
                flag = true;
                imgFont.enabled = true;
            }
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
        if (Random.Range(0f, 100f) < 90) //Probabilité de faire apparaitre un type de mob
        {
            Enemy m = Instantiate(Mob) as Enemy;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);
        }
        else
        {
            /*EnemyTorp m = Instantiate(MobTorp) as EnemyTorp;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);*/

            EnemyKamikaze m = Instantiate(MobKamikaze) as EnemyKamikaze;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);
        }
    }
    private void spawnWave()
    {
        if (waveFlag == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Enemy m = Instantiate(Mob) as Enemy;
                m.Initalize(player);
                m.transform.position = new Vector3(-5 + i * 4, 20 + i * 2, -2.8F);
            }
            waveFlag++;
        }
        else if (waveFlag == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                Enemy m = Instantiate(Mob) as Enemy;
                m.Initalize(player);
                m.transform.position = new Vector3(-8.5f + i * 4, 20, -5);
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
