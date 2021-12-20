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

    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;

    private Color couleur;
   // public GameObject body;

   // public Slider lifeBar;
    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;

    private bool isnotDied = true;
    private float angle = 0f;

    public Stopwatch timer;
    public Stopwatch timerPattern;
    public Stopwatch timerFlag;
    [SerializeField]
    private float shootRate = 300;
    private float[] shootRates;
    private int flagPattern;

    public Slider lifeBar;

    [SerializeField]
    private bool patternFlag = false;

    private bool[] firstmove;
    [SerializeField]
    private bool finishMove =false;
    //pattern general
    //public GameObject[] spawners;
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
    private int isPattern04Active;
    private GameObject m_player;

    //patern05
    private float pattern05Angle;
    private float pattern05AngleNumber;
    private bool pattern05Dir = false;

    private bool pattern06flag = false;

    private IEnumerator coroutine;
    private Image imageFont;
    private Dialogue dialogueGura;

    public delegate void InterfaceVictory();
    public event InterfaceVictory OnInterfaceVictory;

    [SerializeField]
    private bool flagDialogue = true;
    [SerializeField]
    private int flag0tmp;

    public delegate void BossMusic();
    public event BossMusic OnBossMusic;

    private void Awake()
    {
        //lifeBar.value = 1;
    }
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
        //InvokeRepeating("FirePattern01", 0f, 10f);
        timer = new Stopwatch();
        timer.Start();
        timerPattern = new Stopwatch();
        timerFlag = new Stopwatch();
        timerFlag.Start();
        timerPattern.Start();

        shootRates = new float[] { 300, 10, 10, 15 };
        flagPattern = 0;
        firstmove = new bool[] { false, false, false, false };
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

        pattern05Angle = 0f;
        isPattern04Active = 0;

        pattern05AngleNumber = 4;

        //coroutine = CiblePlayer(3.0f);
        //InvokeRepeating("MoveSpawners", 20f, 0.2f);
        lifeBar.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (flagDialogue && Time.timeScale == 1)
        {
            print("1");

            Time.timeScale = 0;
            dialogueGura.StartDialogue(); // On commence le dialogue du boss
            imageFont.enabled = true;
        }
        else if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Space) && flagDialogue) //si le boss a pop ET qu'on est en dialogue
        {
            print("2");
            dialogueGura.NextLine();
            if(dialogueGura.currentDialogue == 0)
            {
                print("3");
                Time.timeScale = 1;
                flagDialogue = false;
                imageFont.enabled = false;
            }
        }


        if (Time.timeScale == 1 && isnotDied)
        {
            lifeBar.value = ((float)currentHealth / (float)maxHealth);

            Vector3 screenPos = BossGura.m_mainCamera.WorldToViewportPoint(target.position);
            Vector3 targetPos = new Vector3(0, 10, -5);
            transform.position = Vector3.MoveTowards(transform.position,targetPos,Time.deltaTime*3f);
            if (screenPos.y < 0.0F)
            {
                Destroy(gameObject);
            }

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
            if (timer.ElapsedMilliseconds >= 1000 / shootRate)
            {

                //cercle
                if(flagPattern == 0)
                {

                    if (timerPattern.ElapsedMilliseconds > 500)
                    {
                        pattern01Step = 20;
                        pattern01StartAngle = Random.Range(0, 360);
                        pattern01Angles = new int[pattern01Step];
                        for (int i = 0; i < pattern01Step; i++)
                            pattern01Angles[i] = (pattern01StartAngle + (360 / pattern01Step * i)) % 360;



                        //print("else");
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
               

                //pattern03 quadrpule spinner  shootRate = 200
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
                //pattern04
                /*if (currentHealth <800)
                {
                    isPattern04Active = 1;
                    timerPattern.Restart();
  

                    for (int i = 0; i < 4; i++)
                    { 
                        Vector3 dir = m_player.transform.position - spawners[i].transform.position;
                        float mangle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        Instantiate(bulletPrefab, spawners[i].transform.position, Quaternion.Euler(0f, 0f, 90+mangle));

                    }

                    // for (int i=0;i<4;i


                }*/
                //pattern05 quadruple sinusoides shootrate 200
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
                //pattern02 rosace shootRate = 15;
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
       

       

       

        /*if (isPattern04Active == 0 && finishMove)
        {
            for (int i = 0; i < 4; i++)
            {
                //spawners[i].transform.position = transform.position + (spawners[i].transform.position - transform.position).normalized * 4;
                spawners[i].transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * 20);
            }
        }
        if (isPattern04Active == 1)
        {
            int x = 0, y = 5, z = -5;
            int ecart = 10;
            Vector3 pos1 = new Vector3(x + ecart, y, z);
            Vector3 pos2 = new Vector3(x - ecart, y, z);
            Vector3 pos3 = new Vector3(x, y + ecart, z);
            Vector3 pos4 = new Vector3(x, y - ecart, z);

            spawners[0].transform.position = Vector3.MoveTowards(spawners[0].transform.position, pos1, Time.deltaTime * 10);
            spawners[1].transform.position = Vector3.MoveTowards(spawners[1].transform.position, pos2, Time.deltaTime * 10);
            spawners[2].transform.position = Vector3.MoveTowards(spawners[2].transform.position, pos3, Time.deltaTime * 10);
            spawners[3].transform.position = Vector3.MoveTowards(spawners[3].transform.position, pos4, Time.deltaTime * 10);
        }*/

    }
    /*void MoveSpawners()
    {
        print("jsuis dans la boucle");
        Vector3 pos1a = new Vector3(transform.position.x - 5, transform.position.y, -5);
        Vector3 pos2a = new Vector3(transform.position.x + 5, transform.position.y, -5);
        Vector3 pos3a = new Vector3(transform.position.x, transform.position.y - 5, -5);
        Vector3 pos4a = new Vector3(transform.position.x, transform.position.y + 5, -5);

 
            if (Vector3.Distance(pos1a, spawners[0].transform.position) > 0.2f)
            {
                spawners[0].transform.position = Vector3.MoveTowards(spawners[0].transform.position, pos1a, Time.deltaTime * 10);
            }
            else
                firstmove[0] = true;

            if (Vector3.Distance(pos2a, spawners[1].transform.position) > 0.2f)
            {
                spawners[1].transform.position = Vector3.MoveTowards(spawners[1].transform.position, pos2a, Time.deltaTime * 10);

            }
            else
                firstmove[1] = true;


            if (Vector3.Distance(pos3a, spawners[2].transform.position) > 0.2f)
            {
                spawners[2].transform.position = Vector3.MoveTowards(spawners[2].transform.position, pos3a, Time.deltaTime * 10);

            }
            else
                firstmove[2] = true;
            if (Vector3.Distance(pos4a, spawners[3].transform.position) > 0.2f)
            {
                spawners[3].transform.position = Vector3.MoveTowards(spawners[3].transform.position, pos4a, Time.deltaTime * 10);

            }
            else
                firstmove[3] = true;

            finishMove = true;
            for (int i = 0; i < 4; i++)
                if (firstmove[i] == false)
                    finishMove = false;


    }
    private IEnumerator CiblePlayer(float waitTime)
    {
        print("routçe");
        Vector3 playerPos = m_player.transform.position;
        
        while(spawners[0].transform.position != playerPos)
        {
            for (int i = 0; i < 4; i++)
            {
                spawners[i].transform.position = Vector3.MoveTowards(spawners[i].transform.position, playerPos, Time.deltaTime * 40);

            }
        }
       
        yield return new WaitForSeconds(waitTime);

    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
           if (isnotDied)
               StartCoroutine("Hurt");
            currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();
            //Debug.Log("bullet");

        }

        if (currentHealth <= 0)
        {
            OnBossMusic?.Invoke();
            OnKilledEnemy?.Invoke();
            StartCoroutine("died");
            //Debug.Log("j'appelle levent");

        }

    }
    IEnumerator died() //La coroutine sert à désactiver partiellement le monstre pour jouer le son de mort avant de le supprimer pour de bons à la fin
    {
        isnotDied = false;
        couleur.r = 1f;
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
        GetComponent<AudioSource>().PlayOneShot(explosion);
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
