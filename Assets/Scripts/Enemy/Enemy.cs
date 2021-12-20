using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class Enemy : Entity
{
    static Camera m_mainCamera;
    public Transform target;

    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;
    //bullet
    private float shootRate = 0.6f;
    public GameObject bulletPrefab;
    public Stopwatch timer;
    public Transform[] bulletSpawn = new Transform[3];
    public GameObject spikeball;
    public BonusScript bonus;

    private bool isnotDied = true;

   // public GameObject spike;
    //private Color couleur;

    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();
    }

    public void Initalize(PlayerController player)
    {
        OnKilledEnemy += player.OnBulletHit;

        maxHealth = 100;
        currentHealth = maxHealth;
        m_mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1 && isnotDied)
        {
            Vector3 screenPos = Enemy.m_mainCamera.WorldToViewportPoint(target.position);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (5F * Time.deltaTime), this.transform.position.z);
            if (screenPos.y < 0.0F)
            {
                Destroy(gameObject);
            }
            if (timer.ElapsedMilliseconds >= 1000 / shootRate)
            {
                for (int i = 0; i < bulletSpawn.Length; i++) // on tire les 3 balles avec les deux sur les cotés qui changent d'angle
                {
                    if (i == 0)
                    {
                        Instantiate(bulletPrefab, bulletSpawn[i].position, Quaternion.Euler(0f, 0f, 0f));
                    }
                    else if (i == 1)
                    {
                        Instantiate(bulletPrefab, bulletSpawn[i].position, Quaternion.Euler(0f, 0f, 25f));
                    }
                    else
                    {
                        Instantiate(bulletPrefab, bulletSpawn[i].position, Quaternion.Euler(0f, 0f, -25f));
                    }
                }
                timer.Restart();
            }
        }
    }

   /* IEnumerator Hurt()
    {
        couleur = spike.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        print(couleur.r);
        couleur.r = 0.2f;
        spike.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
        yield return new WaitForSeconds(0.15f);
        couleur.r = 1f;
        spike.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   
            currentHealth = 0;
            //Debug.Log("player");

        }
        if (other.gameObject.tag == "Bullet")
        {
            //StartCoroutine("Hurt");
            //Debug.Log("bullet");
            currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();

        }

        if (currentHealth <= 0)
        {
            OnKilledEnemy?.Invoke();
            Vector3 tmpos= transform.position;
            this.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine("died");

            if (other.GetComponent<BulletFragment>())
            {
                for (int i = 0; i < 8; i++)
                    Instantiate(other.gameObject, transform.position, Quaternion.Euler(i * 45, 90f,90f));

            }

            //Debug.Log("j'appelle levent");

        }
    }
    IEnumerator died() //La coroutine sert à désactiver partiellement le monstre pour jouer le son de mort avant de le supprimer pour de bons à la fin
    {
        isnotDied = false;
        this.GetComponent<AudioSource>().Play();
        Destroy(spikeball);
        BonusScript m = Instantiate(bonus) as BonusScript;
        m.transform.position = target.position;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
