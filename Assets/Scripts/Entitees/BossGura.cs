using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class BossGura : Entity
{
    public AudioClip explosion;
    static Camera m_mainCamera;
    public Transform target;

    //prefab du jouer et des balles
    private GameObject m_player;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;

    private Color couleur;


    //parametres des patterns 
    public Stopwatch timer;
    public Stopwatch timerPattern;
    public Stopwatch timerFlag;
    [SerializeField]
    private float shootRate = 300;
    private float[] shootRates;
    private int flagPattern;
    private bool isnotDied = true;



    //dialogue et infos bosse
    [SerializeField]
    private bool flagDialogue = true;
    private Image imageFont;
    private Dialogue dialogueGura;
    public Slider lifeBar;

    //evenements
    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;

    public delegate void InterfaceVictory();
    public event InterfaceVictory OnInterfaceVictory;

    public delegate void BossMusic();
    public event BossMusic OnBossMusic;

    //pattern1
    private int pattern01Step;
    private int pattern01StartAngle;
    [SerializeField]
    private int[] pattern01Angles;

    //patern02
    private float pattern02Angle;
    private float pattern02Angleb;

    //patern03
    private float pattern03Angle;
    private float pattern03Angleb;
    private float pattern03Anglec;
    private float pattern03Angled;

    //patern04
    private float pattern05Angle;
    private float pattern05AngleNumber;
    private bool pattern05Dir = false;


    public void Initalize(PlayerController player, Dialogue dialogue, Image imgFont, MusicManager zicManager, UserInterface userInterface)
    {
        OnKilledEnemy += player.OnBulletHit;
        OnInterfaceVictory += userInterface.setVictoryScene;

        maxHealth = 4000;
        currentHealth = maxHealth;
        m_mainCamera = Camera.main;

        if(player)
            m_player = player.gameObject;

        dialogueGura = dialogue;
        imageFont = imgFont;

        OnBossMusic += zicManager.BossOnMap;
        OnBossMusic?.Invoke();
        OnBossMusic += zicManager.BossNoMoreOnMap;
    }
    // Start is called before the first frame update
    private void Start()
    {
        //Initialisation des timers
        timer = new Stopwatch();
        timer.Start();

        timerPattern = new Stopwatch();
        timerPattern.Start();

        timerFlag = new Stopwatch();
        timerFlag.Start();

        lifeBar.value = 1;

        //parametres des patterns
        shootRates = new float[] { 300, 10, 10, 15 };
        flagPattern = 0;

        //patern01
        pattern01Step = Random.Range(3, 5);
        pattern01StartAngle = Random.Range(0, 360);
        pattern01Angles = new int[360 / pattern01Step];
        for (int i = 0; i < pattern01Step; i++)
            pattern01Angles[i] = (pattern01StartAngle + (360 / pattern01Step * i)) % 360;

        //pattern02
        pattern02Angle = 0;
        pattern02Angleb = 0;

        //patter03
        pattern03Angle = 0f;
        pattern03Angleb = 90;
        pattern03Anglec = 180f;
        pattern03Angled = 270f;

        //pattern4
        pattern05Angle = 0f;
        pattern05AngleNumber = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (flagDialogue && Time.timeScale == 1)
        {

            Time.timeScale = 0;
            dialogueGura.StartDialogue(); // On commence le dialogue du boss
            imageFont.enabled = true;
        }
        else if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Space) && flagDialogue) //si le boss a pop ET qu'on est en dialogue
        {
            dialogueGura.NextLine();
            if(dialogueGura.currentDialogue == 0)
            {
                Time.timeScale = 1;
                flagDialogue = false;
                imageFont.enabled = false;
            }
        }


        if (Time.timeScale != 0 && isnotDied)
        {
            lifeBar.value = ((float)currentHealth / (float)maxHealth);

            Vector3 screenPos = BossGura.m_mainCamera.WorldToViewportPoint(target.position);
            Vector3 targetPos = new Vector3(0, 10, -5);
            transform.position = Vector3.MoveTowards(transform.position,targetPos,Time.deltaTime*3f);
            if (screenPos.y < 0.0F)
            {
                Destroy(gameObject);
            }
            //choix des patterns
            if(timerFlag.ElapsedMilliseconds >= 4000)
            {
                flagPattern = Random.Range(0,100);
                if (flagPattern < 50)
                    flagPattern = 1;
                else
                    flagPattern = 2;

                if (currentHealth > maxHealth * 0.7f)
                    flagPattern = 0;
                if (currentHealth < maxHealth * 0.20f)
                    flagPattern = 3;

                shootRate = shootRates[flagPattern];
                timerFlag.Restart();
                print(flagPattern);
            }
            //execution des patterns
            if (timer.ElapsedMilliseconds >= 1000 / shootRate)
            {

                // pattern1 cercle
                if(flagPattern == 0)
                {

                    if (timerPattern.ElapsedMilliseconds > 500)
                    {
                        pattern01Step = 20;
                        pattern01StartAngle = Random.Range(0, 360);
                        pattern01Angles = new int[pattern01Step];
                        for (int i = 0; i < pattern01Step; i++)
                            pattern01Angles[i] = (pattern01StartAngle + (360 / pattern01Step * i)) % 360;

                        //pattern en cercle
                        for (int i = 0; i < pattern01Step; i++)
                        {
                            float posX = transform.position.x + Mathf.Sin((pattern01Angles[i] * Mathf.PI) / 180);
                            float posY = transform.position.y + Mathf.Cos((pattern01Angles[i] * Mathf.PI) / 180);
                            Vector3 pos = new Vector3(posX, posY, transform.position.z);
                            GameObject bullet = Instantiate(bulletPrefab2, transform.position, Quaternion.Euler(0f, 0f, pattern01Angles[i]));
                            Vector3 scale = new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);
                            bullet.transform.localScale = scale*5;

                        }

                        timerPattern.Restart();
                    }
                }
               

                //pattern2 quadrpule spinner
                if (flagPattern == 1)
                {
                    GameObject[] bullets = new GameObject[12];
                    //droite
                    bullets[0] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f,pattern03Angle-15));
                    bullets[1] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f,pattern03Angle));
                    bullets[2] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f,pattern03Angle+15));
                    //gauche  
                    bullets[3] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, pattern03Angleb - 15));
                    bullets[4] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, pattern03Angleb));
                    bullets[5] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, pattern03Angleb + 15));
                    //gauche   
                    bullets[6] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, pattern03Anglec - 15));
                    bullets[7] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, pattern03Anglec));
                    bullets[8] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, pattern03Anglec + 15));
                    //gauche   
                    bullets[9] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, pattern03Angled - 15));
                    bullets[10] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, pattern03Angled));
                    bullets[11] = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, pattern03Angled + 15));


                    for (int i =0;i<12;i++)
                    {
                        Vector3 scale = new Vector3(bullets[i].transform.localScale.x, bullets[i].transform.localScale.y, bullets[i].transform.localScale.z);
                        bullets[i].transform.localScale = scale * 3;

                    }
                    pattern03Angle += 4 % 360;
                    pattern03Angleb += 4 % 360;
                    pattern03Anglec += 4 % 360;
                    pattern03Angled += 4 % 360;

                }
               
                //pattern3 quadruple sinusoides shootrate 200
                if (flagPattern == 2)
                {

                    for (int i = 0;i< pattern05AngleNumber;i++)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, (360 / pattern05AngleNumber) * i + pattern05Angle -15));
                        GameObject bullet1 = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, (360 / pattern05AngleNumber) * i + pattern05Angle));
                        GameObject bullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, (360 / pattern05AngleNumber) * i + pattern05Angle+15));

                        bullet.transform.localScale *=3;
                        bullet1.transform.localScale *=3;
                        bullet2.transform.localScale *=3;
                    }

                    if (pattern05Angle > 90)
                        pattern05Dir = true;

                    if (pattern05Angle < 0)
                        pattern05Dir = false;

                    //ralentir
                    if (pattern05Angle > 15 && pattern05Dir == false)
                        pattern05Angle += 0.5f;

                    if (pattern05Angle>75 && pattern05Dir == false)
                        pattern05Angle += 0.5f;

                    if (pattern05Angle > 75 && pattern05Dir == true)
                        pattern05Angle -= 0.5f;

                    if (pattern05Angle < 15 && pattern05Dir == true)
                        pattern05Angle -= 0.5f;

                    if (pattern05Angle < 75 && pattern05Dir == false)
                        pattern05Angle += 4;

                    if (pattern05Angle > 15 && pattern05Dir == false)
                        pattern05Angle += 4;

                    if (pattern05Angle < 75 && pattern05Dir == true)
                        pattern05Angle -= 4f;

                    if (pattern05Angle > 15 && pattern05Dir == true)
                        pattern05Angle -= 4f;


                }
                //pattern4 rosace shootRate = 15;
                if (flagPattern == 3)
                {

                    int nbLines = 4;
                    for (int i = 0; i < nbLines; i++)
                    {
                        GameObject  bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, (pattern02Angle + (360 / nbLines) * i) % 360));
                        pattern02Angle += 1 % 360;

                        bullet.transform.localScale *= 2f;

                    }
                    for (int i = 0; i < nbLines; i++)
                    {
                        GameObject bullet1 = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, (pattern02Angleb + (360 / nbLines) * i) % 360));
                        pattern02Angleb -= 1 % 360;
                        if (pattern02Angleb < 0)
                            pattern02Angleb += 360;
                        bullet1.transform.localScale *= 2f;


                    }
                }
                //lasers mais pas interessant
                if (currentHealth < 0.9f*maxHealth && currentHealth > 0.75f * maxHealth)
                {
                    for(int i =0;i<360/15; i++)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, (360 / 15) * i));
                        bullet.transform.localScale *= 1.5f ;

                    }


                }
                timer.Restart();

            }

        }
    }
    private void FixedUpdate()
    {
        if(m_player)
        {
            Vector3 direction = (m_player.transform.position - transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-direction.x, -direction.y, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
       
            }
   
    private void OnTriggerEnter(Collider other)
    {
        //touche par balle
        if (other.gameObject.tag == "Bullet")
        {
           if (isnotDied)
               StartCoroutine("Hurt");
            currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();

        }
        //mort
        if (currentHealth <= 0)
        {
            OnBossMusic?.Invoke();
            OnKilledEnemy?.Invoke();
            StartCoroutine("died");
        }

    }
    IEnumerator died() //La coroutine sert à désactiver partiellement le monstre pour jouer le son de mort avant de le supprimer pour de bons à la fin
    {
        isnotDied = false;
        couleur.r = 1f;
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
        GetComponent<AudioSource>().volume = 0.03f;
        GetComponent<AudioSource>().PlayOneShot(explosion);
        PlayerPrefs.SetInt("BossDead", 1);

        yield return new WaitForSeconds(2f);
        OnInterfaceVictory?.Invoke();
        Destroy(gameObject);

    }
    
    IEnumerator Hurt()
    {
        if (isnotDied != false)
        {
            couleur = GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            couleur.r = 1f;
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
            yield return new WaitForSeconds(0.15f);
            couleur.r = 0.302f;
            if (isnotDied)
                GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
        }
    }

}
