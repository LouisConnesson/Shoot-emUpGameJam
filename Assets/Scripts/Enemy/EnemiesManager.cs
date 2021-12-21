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
    public EnemyDestroyer MobDestroyer;
    public EnemyBossAlterChan BossAlterChan;

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
    public Dialogue dialogueAlterChan;
    public Dialogue dialogueChanEsquive;
    public UserInterface userInterface;
    private int level = 0;
    private int difficultyFactor = 0;
    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();

        level =  PlayerPrefs.GetInt("Level");
        PlayerPrefs.SetInt("BossDead", 0);


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
        if (Time.timeScale != 0 && player) 
        {
            if (level == 0) // les boss apparaissent en fonction du niveau, ici c'est pour le niveau 1 donc on fait apparaitre le 1er boss
            {
                timer.Start();

                if (timer.ElapsedMilliseconds >= 35000 && flag == false) //////////////////////BOSS
                {
                    StopAllCoroutines(); //Il est important d'arrêter les coroutines pour le boss sinon il y aura pleins d'enemis à coté
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
            }
            else if (level == 1) // les boss apparaissent en fonction du niveau, ici c'est pour le niveau 2 donc on fait apparaitre le 2eme boss
            {
                timer.Start();

                if (timer.ElapsedMilliseconds >= 35000 && flag == false && flagGura == false) //////////////////////BOSS
                {
                    StopAllCoroutines();//Il est important d'arrêter les coroutines pour le boss sinon il y aura pleins d'enemis à coté
                    GameObject guramob = Instantiate(gura) as GameObject;
                    guramob.transform.GetChild(0).gameObject.GetComponent<BossGura>().Initalize(player,dialogueGura, imgFont, zicManager, userInterface);
                    //guramob.GetComponent<BossGura>().Initalize(player);
                    guramob.transform.position = new Vector3(-7, 19, -5);
                    flagGura = true;

                }
            }
            else if (level == 2)// les boss apparaissent en fonction du niveau, ici c'est pour le niveau 3 donc on fait apparaitre le 3eme boss
            {
                timer.Start();

                if (timer.ElapsedMilliseconds >= 35000 && flagChan == false) //////////////////////BOSS
                {
                    StopAllCoroutines();//Il est important d'arrêter les coroutines pour le boss sinon il y aura pleins d'enemis à coté
                    EnemyBossChan chan = Instantiate(BossChan) as EnemyBossChan;
                    chan.Initalize(player, dialogueChan, dialogueChanEsquive, imgFont, zicManager, userInterface);
                    chan.transform.position = new Vector3(2, 25, -5);
                    flagChan = true;
                }
            }
            else if (level == 3)// les boss apparaissent en fonction du niveau, ici c'est pour le niveau 3 donc on fait apparaitre le 3eme boss
            {
                timer.Start();

                if (timer.ElapsedMilliseconds >= 20000 && flagChan == false) //////////////////////BOSS
                {
                    StopAllCoroutines();//Il est important d'arrêter les coroutines pour le boss sinon il y aura pleins d'enemis à coté
                    EnemyBossAlterChan chan = Instantiate(BossAlterChan) as EnemyBossAlterChan;
                    chan.Initalize(player, dialogueAlterChan, dialogueChanEsquive, imgFont, zicManager, userInterface);
                    chan.transform.position = new Vector3(2, 25, -5);
                    flagChan = true;
                }
            }
            else if (level == 4)// Ici c'est le mode survie, toutes les 3 secondes, on incrémente une valeur qui influe sur la quantité de mobs qui peuvent apparaitre
            {
                timer.Start();

                if (timer.ElapsedMilliseconds >= 3000 && flagChan == false) //////////////////////BOSS
                {
                    difficultyFactor += 1;
                    print(difficultyFactor);
                    timer.Restart();
                }
            }
        }
        else if (flag == true && Input.GetKeyDown(KeyCode.Space)) //si le boss 1 a pop ET qu'on est en dialogue
        {
            dialogue.NextLine();
            if (dialogue.currentDialogue == 0) //si le dialogue est finit on remet le temps
            {
                print("4");
                Time.timeScale = 1;
                imgFont.enabled = false;
            }
        }
        else //si le temps est arrêté on stop le temps du 1er boss
        {
            timer.Stop();
        }
    }
    private void spawnEnemy() // fonction pour faire apparaitre des enemis à intervale régulié et aléatoirement en fonction du niveau ou de la durée passé ds le mode survie
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
            //
        }
    }
    private void spawnWave() // fonction pour gérer les apparitions des waves, elles s'enchainent les unes après les autres et varie en fonction du niveau avec des monstres aléatoires
    {
        if (waveFlag == 0 && (level == 1 || level > 2))
        {
            EnemyDestroyer m = Instantiate(MobDestroyer) as EnemyDestroyer;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);
            m.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            waveFlag++;
        }
        else if (waveFlag == 0)
        {
            if ((level >= 2 && Random.Range(0f, 100f) < 50) || level < 2) //Probabilité de faire apparaitre un type de mob
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
            if ((level >= 2 && Random.Range(0f, 100f) < 50) || level < 2) //Probabilité de faire apparaitre un type de mob
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
            if ((level >= 2 && Random.Range(0f, 100f) < 50) || level <2) //Probabilité de faire apparaitre un type de mob
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

    private void spawnWaveKamikaze() //fonction pour gérer les apparitions des enemis kamikazes
    {
        if (level >= 2)//Si on est au niveau 3 ou en mode survie, les torpilles qui spawn sont plus nombreuses
        { 
            for (int i = 0; i < 4; i++)
            {
                EnemyKamikaze m = Instantiate(MobKamikaze) as EnemyKamikaze;
                m.Initalize(player);
                m.transform.position = new Vector3(Random.Range(-8, 9), Random.Range(20, 25), -5);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                EnemyKamikaze m = Instantiate(MobKamikaze) as EnemyKamikaze;
                m.Initalize(player);
                m.transform.position = new Vector3(Random.Range(-8, 9), Random.Range(20, 25), -5);
            }
        }
    }

    //////////////////////////COROUTINES POUR GERER LES INTERVALLES DES APPARITIONS DES MONSTRES
        IEnumerator Spawner()
    {
        while (true)
        {
            if (level == 4)
                yield return new WaitForSeconds(4.0f - ((float)difficultyFactor * 0.05f));
            else
                yield return new WaitForSeconds(3.0f);
            spawnEnemy();
        }
    }
    IEnumerator SpawnerWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(14.0f);
            spawnWave();
        }
    }
    IEnumerator SpawnerWaveKamikaze()
    {
        while (true)
        {
            if (level == 4)
                yield return new WaitForSeconds(15.0f - ((float)difficultyFactor * 0.1f));
            else if (level >= 2) //Si on est au niveau 3 ou + ou en mode survie, les torpilles spawn plus fréquement
                yield return new WaitForSeconds(8.0f);
            else
                yield return new WaitForSeconds(14.0f);
            spawnWaveKamikaze();
        }
    }
}
