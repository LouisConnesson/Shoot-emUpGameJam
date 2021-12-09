using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class EnemyTorp : Entity
{
    static Camera m_mainCamera;
    public Transform target;
    public Transform torpRotation;

    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;
    //bullet
    public GameObject bulletPrefab;
    public Stopwatch timer;
    public Transform[] bulletSpawn = new Transform[3];

    private Color couleur;
    private float shootRate;

    public GameObject torpedo;
    private bool isnotDied = true;

    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();
        shootRate = bulletPrefab.GetComponent<BulletEnemy>().GetBulletRate();
    }

    public void Initalize(PlayerController player)
    {
        OnKilledEnemy += player.OnBulletHit;

        maxHealth = 300;
        currentHealth = maxHealth;
        m_mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1 && isnotDied)
        {
            Vector3 screenPos = EnemyTorp.m_mainCamera.WorldToViewportPoint(target.position);
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
                        Instantiate(bulletPrefab, bulletSpawn[i].position, Quaternion.Euler(0, 0, torpRotation.rotation.z * 90));
                    }
                    else if (i == 1)
                    {
                        Instantiate(bulletPrefab, bulletSpawn[i].position, Quaternion.Euler(0, 0, torpRotation.rotation.z * 90));
                    }
                }
                timer.Restart();
            }
        }
    }

    IEnumerator Hurt()
    {
        
        couleur = this.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        couleur.r = 1f;

        this.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
        yield return new WaitForSeconds(0.15f);
        couleur.r = 0.509434f;
        this.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            currentHealth = 0;
            //Debug.Log("player");

        }
        if (other.gameObject.tag == "Bullet")
        {
            StartCoroutine("Hurt");
            currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();
            UnityEngine.Debug.Log(other.GetComponent<Bullet>().GetBulletDamage());
            //Debug.Log("bullet");

        }

        if (currentHealth <= 0)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            OnKilledEnemy?.Invoke();
            if (other.GetComponent<BulletFragment>())
            {
                for (int i = 0; i < 8; i++)
                    Instantiate(other.gameObject, transform.position, Quaternion.Euler(0, 0, i * 45));
            }
            StartCoroutine("died");

            //Debug.Log("j'appelle levent");

        }

    }
    IEnumerator died() //La coroutine sert à désactiver partiellement le monstre pour jouer le son de mort avant de le supprimer pour de bons à la fin
    {
        isnotDied = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<AudioSource>().Play();
        Destroy(torpedo);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
