using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
public class EnemyBossAlterChan : Entity
{
    static Camera m_mainCamera;
    public Transform target;

    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;
    public delegate void BossMusic();
    public event BossMusic OnBossMusic;
    public delegate void InterfaceVictory();
    public event InterfaceVictory OnInterfaceVictory;
    [SerializeField]
    private GameObject m_player;
    private Rigidbody rb;
    [SerializeField]
    ///////////////CIBLE ET VISEE
    public GameObject cible;
    private Vector2 movement;
    private Vector2 lookDirection;
    private float lookAngle;
    ///////////////

    //bullet
    private float shootRate = 500f;
    public GameObject bulletPrefab;
    public GameObject bulletPrefabColor;
    public GameObject bulletSniper;
    public int paternFlag = 0;
    private float patern = -10f;
    private float patern2 = 10f;
    private bool flag = false;
    private bool flag2 = false;
    //fin bullet
    private bool flagStop = false;
    private bool[] flagCoroutine = new bool[3];

    private bool flagDead = true;
    private Color couleur;
    public GameObject body;
    private bool isnotDied = true;

    public Slider lifeBar;

    public Stopwatch timer;
    private Stopwatch timerLife;
    private Stopwatch timerPatern;
    private int paternIndex = 0;
    public Transform[] bulletSpawn = new Transform[3];
    PlayerController joueurHP;

    private bool flagDialogue = true;
    private bool flagDialogueEsquive = true;
    Dialogue dialogueChan;
    Dialogue dialogueChanEsquive;

    Image font;
    public AudioSource PaternSound;


    private void Awake()
    {
        timer = new Stopwatch();
        timerLife = new Stopwatch();
        timerLife.Start();
        timerPatern = new Stopwatch();
        lifeBar.value = 1;
        flagCoroutine[0] = false;
        flagCoroutine[1] = false;
        flagCoroutine[2] = false;
    }

    public void Initalize(PlayerController player, Dialogue dialogue, Dialogue dialogueEsquive, Image imgFont, MusicManager zicManager, UserInterface userInterface)
    {
        OnKilledEnemy += player.OnBulletHit;
        OnBossMusic += zicManager.BossOnMap;
        OnBossMusic?.Invoke();
        OnBossMusic += zicManager.BossNoMoreOnMap;

        OnInterfaceVictory += userInterface.setVictoryScene;



        maxHealth = 7000;
        currentHealth = maxHealth;
        m_mainCamera = Camera.main;
        joueurHP = player;
        dialogueChan = dialogue;
        dialogueChanEsquive = dialogueEsquive;
        font = imgFont;
        if (player)
        {
            m_player = player.gameObject;
            rb = m_player.GetComponent<Rigidbody>();

        }
    }
    // Update is called once per frame
    void Update()
    {
        ///////////////////ON GERE LE DIALOGUE ET CELA ARRETE LE TEMPS
        if (flagDialogue && Time.timeScale != 0)
        {
            Time.timeScale = 0;
            dialogueChan.StartDialogue(); // On commence le dialogue du boss
            font.enabled = true;

        }
        else if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Space) && flagDialogue) //si le boss a pop ET qu'on est en dialogue
        {
            dialogueChan.NextLine();
            if (dialogueChan.currentDialogue == 0) //si le dialogue est finit on remet le temps
            {
                Time.timeScale = 1;
                flagDialogue = false;
                font.enabled = false;
                timerPatern.Start();
                timer.Start();
            }
        }
        if (timerPatern.ElapsedMilliseconds >= 4500 || (timerPatern.ElapsedMilliseconds >= 3000 && paternIndex == 1) || (timerPatern.ElapsedMilliseconds >= 3000 && paternIndex == 6)) //On change le patern des attaques � chaque laps de temps choisit
        {
            if (paternIndex == 1) //si le patern est � 1 on ne peut pas utiliser le patern 4 sinon on est oblig� de se faire toucher
                paternIndex = Random.Range(2, 4);
            else if (paternIndex == 0)
                paternIndex = Random.Range(1, 7);
            else if (paternIndex == 2)
                paternIndex = Random.Range(3, 7);
            else if (paternIndex == 3)
                paternIndex = Random.Range(0, 3);
            else if (paternIndex == 4)
                paternIndex = Random.Range(5, 7);
            else if (paternIndex == 5)
                paternIndex = Random.Range(0, 5);
            else if (paternIndex == 6)
                paternIndex = Random.Range(0, 6);
            else
                paternIndex = Random.Range(0, 7);

            flagCoroutine[0] = false;
            flagCoroutine[1] = false;
            flagCoroutine[2] = false;
            timerPatern.Restart();
        }
        /*if (flagDialogueEsquive && Time.timeScale != 0 && currentHealth < 900) //Le dialogue de l'esquive se lance peu apr�s le d�but du combat
        {
            Time.timeScale = 0;
            dialogueChanEsquive.StartDialogue(); // On commence le dialogue du boss
            font.enabled = true;

        }
        else if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Space) && flagDialogueEsquive && currentHealth < 900) //si le boss a pop ET qu'on est en dialogue
        {
            dialogueChanEsquive.NextLine();
            if (dialogueChanEsquive.currentDialogue == 0) //si le dialogue est finit on remet le temps
            {
                Time.timeScale = 1;
                flagDialogueEsquive = false;
                font.enabled = false;
            }
        }*/
        /////////////////////
        if (Time.timeScale != 0 && isnotDied)
        {

            if (m_player) //on fais bouger la cible sur le joueur
            {
                Vector3 direction = (m_player.transform.position - cible.transform.position);
                movement = direction;
            }
            lifeBar.value = ((float)currentHealth / (float)maxHealth);

            Vector3 screenPos = EnemyBossAlterChan.m_mainCamera.WorldToViewportPoint(target.position);
            if (this.transform.position.y > 12)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (5F * Time.deltaTime), this.transform.position.z);
            }
            if (screenPos.y < 0.0F)
            {
                Destroy(gameObject);
            }
            /*if (timerLife.ElapsedMilliseconds >= 1000 / 8F && joueurHP.getmcurrentHealth() > 0) // on enl�ve la vie du boss au fil du temps tant que le joueur est encore en vie
            {
                currentHealth = currentHealth - 5;
                timerLife.Restart();
            }*/

            if (timer.ElapsedMilliseconds >= 1000 / (shootRate * Time.timeScale))
            {
                if (paternIndex == 0)//////////////////////////// les paternes du boss s'effectuent en fonction de sa vie
                {
                    if (flagCoroutine[0] == false)
                    {
                        //la coroutine sniping peut s'appeler n'importe quand mais les diff�rentes coroutines sniping doivent �tre espac�s un minimum dans le temps
                        StartCoroutine("sniping");
                        flagCoroutine[0] = true;
                    }
                    PaternSound.volume = 0.03f;
                    PaternSound.Play();
                    shootRate = 3F;
                    Instantiate(bulletPrefab, bulletSpawn[0].position, Quaternion.Euler(0f, 0f, Random.Range(-100, -10)));
                    Instantiate(bulletPrefab, bulletSpawn[1].position, Quaternion.Euler(0f, 0f, Random.Range(10, 100)));
                }
                else if (paternIndex == 1)////////////////////////////
                {
                    PaternSound.volume = 0.1f;
                    shootRate = 150F;
                    /////ON REMPLIS PATERN
                    if (flag == false)
                        patern = patern - 20f;
                    else
                        patern = patern + 20f;

                    if (patern > -10f || patern < -120f)
                    {
                        if (flag == false)
                            flag = true;
                        else
                            flag = false;
                    }
                    /////
                    /////ON REMPLIS PATERN2
                    if (flag2 == false)
                        patern2 = patern2 - 20f;
                    else
                        patern2 = patern2 + 20f;

                    if (patern2 > 120f || patern2 < 10f)
                    {
                        if (flag2 == false)
                            flag2 = true;
                        else
                            flag2 = false;
                    }
                    /////   
                    if (PaternSound.isPlaying == false)
                        PaternSound.Play();
                    Instantiate(bulletPrefab, bulletSpawn[0].position, Quaternion.Euler(0f, 0f, patern));
                    Instantiate(bulletPrefab, bulletSpawn[1].position, Quaternion.Euler(0f, 0f, patern2));
                }
                else if (paternIndex == 2)
                {
                    PaternSound.volume = 0.03f;
                    shootRate = 30F;
                    patern = patern + 10;
                    PaternSound.Play();
                    Instantiate(bulletPrefab, bulletSpawn[2].position, Quaternion.Euler(0f, 0f, patern));
                    if (flagCoroutine[1] == false)
                    {
                        StartCoroutine("sniping");
                        flagCoroutine[1] = true;
                    }
                }
                else if (paternIndex == 3)
                {
                    PaternSound.volume = 0.2f;
                    shootRate = 0.8f;
                    if (flagCoroutine[2] == false)
                    {
                        StartCoroutine("sniping");
                        flagCoroutine[2] = true;
                    }
                    PaternSound.Play();
                    CirclePatern();

                }
                else if (paternIndex == 4)
                {
                    shootRate = 10f;
                    PaternSound.volume = 0.15f;
                    if (PaternSound.isPlaying == false)
                        PaternSound.Play();

                    CrossPatern();
                }
                else if (paternIndex == 5)
                {
                    shootRate = 3f;
                    PaternSound.volume = 0.12f;
                    PaternSound.Play();
                    CirclePatern2();
                }
                else if (paternIndex == 6)
                {
                    shootRate = 1.3f;
                    float value = Random.Range(0, 50);
                    PaternSound.Play();
                    for (int i = 0; i < 30; i++)
                    {
                        Instantiate(bulletPrefab, bulletSpawn[2].position, Quaternion.Euler(0f, (i * 50 + value), (i * 50 + value)));

                    }
                }
                timer.Restart();
            }

        }
        if (currentHealth <= 0 && flagDead)
        {
            OnKilledEnemy?.Invoke();
            flagDead = false;
            OnBossMusic?.Invoke();
            StartCoroutine("died");
            //Debug.Log("j'appelle levent");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //StartCoroutine("Hurt");
            //Debug.Log("bullet");
            currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();
        }
    }
        private void CirclePatern()
    {
        float value = Random.Range(0, 20);
        for (int i = 0; i < 20; i++)
        {
            Instantiate(bulletPrefabColor, bulletSpawn[2].position, Quaternion.Euler(0f, 0f, (i * 20) + value));
        }
    }
    private void CirclePatern2()
    {
        float value = Random.Range(0, 20);
        for (int i = 0; i < 20; i++)
        {
            Instantiate(bulletPrefab, bulletSpawn[2].position, Quaternion.Euler(0f, 0f, (i * 20) + value));
        }
    }
    private void CrossPatern()
    {
        float value = Random.Range(0, 20);
        for (int i = 0; i < 20; i++)
        {
            Instantiate(bulletPrefab, bulletSpawn[2].position, Quaternion.Euler(0f, 0f, (i * 50) + value));
        }
    }
    private void FireSniper()
    {
        GameObject firedSniper = Instantiate(bulletSniper, bulletSpawn[2].position, bulletSpawn[2].rotation);
        firedSniper.GetComponent<Rigidbody>().velocity = bulletSpawn[2].up * 50f;
    }
    /////////////////////////////////////////////MOUVEMENTS DE LA CIBLE
    private void FixedUpdate()
    {
        if (m_player && !flagStop)
            Move(movement);
    }

    private void Move(Vector2 direction)
    {
        float step = 1000 * Time.deltaTime;
        cible.transform.position = Vector3.MoveTowards(cible.transform.position, m_player.transform.position + new Vector3(0, 0, 1), step);
    }
    /////////////////////////////////////////////
    IEnumerator died() //La coroutine sert � d�sactiver partiellement le monstre pour jouer le son de mort avant de le supprimer pour de bons � la fin
    {
        isnotDied = false;
        this.GetComponent<AudioSource>().Play();
        Destroy(body);
        PlayerPrefs.SetInt("BossDead", 1);

        yield return new WaitForSeconds(1f);
        OnInterfaceVictory?.Invoke();
        Destroy(gameObject);
    }
        IEnumerator sniping() //coroutine qui g�re le tire de sniper
    {
        cible.SetActive(true);
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            couleur = cible.GetComponent<MeshRenderer>().material.GetColor("_Color");
            couleur.r = 1f;
            couleur.g = 1f;
            couleur.b = 1f;
            cible.GetComponent<AudioSource>().Play();
            cible.GetComponent<MeshRenderer>().material.SetColor("_Color", couleur);
            yield return new WaitForSeconds(0.3f);
            couleur = cible.GetComponent<MeshRenderer>().material.GetColor("_Color");
            couleur.r = 1f;
            couleur.g = 0.1f;
            couleur.b = 0.1f;
            cible.GetComponent<MeshRenderer>().material.SetColor("_Color", couleur);
            yield return new WaitForSeconds(0.3f);
        }
        flagStop = true;
        yield return new WaitForSeconds(0.2f);
        if (m_player)
        {
            for (int i = 0; i < 40; i++)
            {
                yield return new WaitForSeconds(0.02f);
                lookDirection = cible.transform.position - bulletSpawn[2].transform.position;
                lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                bulletSpawn[2].transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - Random.Range(85f, 95f));
                FireSniper();
            }
        }

        flagStop = false;
        cible.SetActive(false);
    }
}
