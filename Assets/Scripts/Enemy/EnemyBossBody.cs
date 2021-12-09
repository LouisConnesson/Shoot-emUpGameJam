using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
public class EnemyBossBody : Entity
{
    static Camera m_mainCamera;
    public Transform target;

    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;
    //bullet
    private float shootRate = 500f;
    public GameObject bulletPrefab;
    public int paternFlag = 0;
    private float patern = 0f;
    private float paternLimiter = 0f;
    private bool flag = false;
    private bool flagLimiter = false;
    //fin bullet

    private Color couleur;
    public GameObject body;

    public Slider lifeBar;

    public Stopwatch timer;
    private Stopwatch timerPatern;
    private bool timerFlag = false;

    public Transform[] bulletSpawn = new Transform[3];
    private bool shield = true;

    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();
        timerPatern = new Stopwatch();
        timerPatern.Start();
        lifeBar.value = 1;
    }

    public void Initalize(PlayerController player)
    {
        OnKilledEnemy += player.OnBulletHit;

        maxHealth = 1000;
        currentHealth = maxHealth;
        m_mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            lifeBar.value = ((float)currentHealth / (float)maxHealth);

            Vector3 screenPos = EnemyBossBody.m_mainCamera.WorldToViewportPoint(target.position);
            if (this.transform.position.y > 12)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (5F * Time.deltaTime), this.transform.position.z);
            }
            if (screenPos.y < 0.0F)
            {
                Destroy(gameObject);
            }
            if (timer.ElapsedMilliseconds >= 1000 / shootRate)
            {
                //ON ECHANGE LES PATERNES DE TIRS, le 2eme paterne doit jouer un certains nb de secondes
                if (Random.Range(0, 10000) > 9700 && timerFlag == false)
                {
                    paternFlag = 1;
                    timerFlag = true;
                    timerPatern.Restart();
                }
                if (timerPatern.ElapsedMilliseconds >= 2000 && timerFlag == true)
                {
                    timerFlag = false;
                    paternFlag = 0;
                }

                if (currentHealth > 500 && paternFlag == 0)
                {
                    shootRate = 10F;
                    Instantiate(bulletPrefab, bulletSpawn[0].position, Quaternion.Euler(0f, 0f, Random.Range(-50,50)));
                }
                else if (currentHealth > 500 && paternFlag == 1)
                {
                    shootRate = 500F;
                    if (flag == false)
                        patern = patern + 20f;
                    else
                        patern = patern - 20f;

                    if (patern > 80f || patern < -80f)
                    {
                        if (flag == false)
                            flag = true;
                        else
                            flag = false;
                    }
                    Instantiate(bulletPrefab, bulletSpawn[0].position, Quaternion.Euler(0f, 0f, patern));
                }
                //ATTAQUE ULTIME DU BOSS
                if (currentHealth < 500)
                {
                    shootRate = 500F;
                    if (flag == false)
                        patern = patern + 20f + paternLimiter;
                    else
                        patern = patern - 20f + paternLimiter;

                    if (flagLimiter == false)
                        paternLimiter = paternLimiter + 0.1f;
                    else
                        paternLimiter = paternLimiter - 0.1f;

                    if (paternLimiter > 20f || paternLimiter < -20f)
                    {
                        if (flagLimiter == false)
                            flagLimiter = true;
                        else
                            flagLimiter = false;
                    }
                    if (patern > 80f + paternLimiter || patern < -80f + paternLimiter)
                    {
                        if (flag == false)
                            flag = true;
                        else
                            flag = false;
                    }
                    Instantiate(bulletPrefab, bulletSpawn[0].position, Quaternion.Euler(0f, 0f, patern));
                    
                }
                //FIN ATTAQUE ULTIME DU BOSS
                timer.Restart();
            }
        }
    }

    IEnumerator Hurt()
    {
        couleur = body.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        couleur.r = 1f;
        body.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
        yield return new WaitForSeconds(0.15f);
        couleur.r = 0.302f;
        body.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
    }

    public void NoShield()
    {
        shield = false;
        UnityEngine.Debug.Log("y'a splus de shielkd");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            currentHealth = 0;
            //Debug.Log("player");

        }
        if (other.gameObject.tag == "Bullet" && shield == false)
        {
            StartCoroutine("Hurt");
            currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();
            //Debug.Log("bullet");

        }

        if (currentHealth <= 0)
        {
            OnKilledEnemy?.Invoke();
            Destroy(gameObject);
            //Debug.Log("j'appelle levent");

        }

    }
}
