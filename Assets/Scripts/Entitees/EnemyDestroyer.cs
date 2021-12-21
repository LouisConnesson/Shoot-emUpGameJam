using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class EnemyDestroyer : Entity
{
    static Camera m_mainCamera;
    public Transform target;

    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;
    public EnemyKamikaze MobKamikaze;
    public GameObject spawner;
    public PlayerController playerActual;
    //bullet
    private float shootRate = 0.6f;
    public Stopwatch timer;
    public BonusScript bonus;
    public BonusScript bonusFire;
    public BonusScript bonusTime;
    public BonusScript bonusHeal;
    private bool isnotDied = true;

    // public GameObject spike;
    private Color couleur;

    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();
    }

    public void Initalize(PlayerController player)
    {
        OnKilledEnemy += player.OnBulletHit;
        playerActual = player;
        maxHealth = 800;
        currentHealth = maxHealth;
        m_mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && isnotDied)
        {
            Vector3 screenPos = EnemyDestroyer.m_mainCamera.WorldToViewportPoint(target.position);
            if (this.transform.position.y > 10) // on veut rester sur l'écran pour faire apparaitre des kamikazes
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (5F * Time.deltaTime), this.transform.position.z); // on gere les déplacements de haut en bas

            if (screenPos.y < 0.0F)
            {
                Destroy(gameObject);
            }
            if (timer.ElapsedMilliseconds >= 1000 / (shootRate * Time.timeScale)) // on fais pop des kamikazes à intervalle régulier
            {
                EnemyKamikaze m = Instantiate(MobKamikaze) as EnemyKamikaze;
                m.Initalize(playerActual);
                m.transform.position = spawner.transform.position;
                timer.Restart();
            }
        }
    }

     IEnumerator Hurt() //coroutine pour faire clignoter le monstre lorsqu'il subit des dégats
     {
        couleur = this.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        couleur.r = 1f;

        this.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
        yield return new WaitForSeconds(0.15f);
        couleur.r = 0.453f;
        this.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //si on percute un joueur, on pert des pv
        {
            currentHealth = currentHealth - 100;

        }
        if (other.gameObject.tag == "Bullet") //si une balle nous touche, on perd des pv
        {
            StartCoroutine("Hurt");
            currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();

        }

        if (currentHealth <= 0)
        {
            OnKilledEnemy?.Invoke();
            Vector3 tmpos = transform.position;
            this.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine("died");

            if (other.GetComponent<BulletFragment>())
            {
                for (int i = 0; i < 8; i++)
                    Instantiate(other.gameObject, tmpos, Quaternion.Euler(0, 0, i * 45));
            }

        }
    }
    IEnumerator died() //La coroutine sert à désactiver partiellement le monstre pour jouer le son de mort avant de le supprimer pour de bons à la fin
    {
        isnotDied = false;
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<MeshRenderer>().enabled = false;
        if (Random.Range(0, 10) <= 6)
        {
            BonusScript m = Instantiate(bonus) as BonusScript;
            m.transform.position = target.position;
        }
        else if (Random.Range(0, 10) <= 2)
        {
            BonusScript m = Instantiate(bonusFire) as BonusScript;
            m.transform.position = target.position;
        }
        else if(Random.Range(0, 10) <= 5)
        {
            BonusScript m = Instantiate(bonusTime) as BonusScript;
            m.transform.position = target.position;
        }
        else
        {
            BonusScript m = Instantiate(bonusHeal) as BonusScript;
            m.transform.position = target.position;
        }


        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}