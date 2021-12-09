using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
public class EnemyBossShieldMaker : Entity
{
    static Camera m_mainCamera;
    public Transform target;
    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;
    public delegate void DeathShield();
    public event DeathShield OnDeathShield;
    public Slider lifeBar;

    public GameObject sonde;
    private Color couleur;

    public void Initalize(PlayerController player)
    {
        OnKilledEnemy += player.OnBulletHit;

        maxHealth = 400;
        currentHealth = maxHealth;
        m_mainCamera = Camera.main;
    }
    public void ShieldEvent(EnemyBossShield shield)
    {
        OnDeathShield += shield.ShieldMakerKilled;
    }
    public void Start()
    {
        lifeBar.value = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            lifeBar.value = ((float)currentHealth / (float)maxHealth);
            Vector3 screenPos = EnemyBossShieldMaker.m_mainCamera.WorldToViewportPoint(target.position);
            if (this.transform.position.y > 7)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (5F * Time.deltaTime), this.transform.position.z);
            }
            else
            {
                this.GetComponent<Animator>().enabled = true;
            }
            if (screenPos.y < 0.0F)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Hurt()
    {
        couleur = sonde.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        couleur.r = 0.2f;
        sonde.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
        yield return new WaitForSeconds(0.15f);
        couleur.r = 1f;
        sonde.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            StartCoroutine("Hurt");
            currentHealth -= 35;
            //Debug.Log("bullet");

        }

        if (currentHealth <= 0)
        {
            OnDeathShield?.Invoke();
            OnKilledEnemy?.Invoke();
            Destroy(gameObject);
            //Debug.Log("j'appelle levent");

        }

    }
}
