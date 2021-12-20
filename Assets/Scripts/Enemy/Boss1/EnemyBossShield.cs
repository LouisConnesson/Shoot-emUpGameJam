using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
public class EnemyBossShield : Entity
{
    static Camera m_mainCamera;
    public Transform target;
    public GameObject shield;
    private Color couleur;

    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;
    private int nbShieldMaker = 2;

    public Slider lifeBar;
    public delegate void NoShield();
    public event NoShield OnNoShield;

    public void Initalize(PlayerController player)
    {
        OnKilledEnemy += player.OnBulletHit;

        maxHealth = 400;
        currentHealth = maxHealth;
        m_mainCamera = Camera.main;
    }
    public void Start()
    {
        lifeBar.value = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            lifeBar.value = ((float)currentHealth / (float)maxHealth);
            Vector3 screenPos = EnemyBossShield.m_mainCamera.WorldToViewportPoint(target.position);
            if (this.transform.position.y > 12)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (5F * Time.deltaTime), this.transform.position.z);
            }
            if (screenPos.y < 0.0F)
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void ShieldMakerKilled()
    {
        nbShieldMaker--;
    }
    public void NoShieldEvent(EnemyBossBody boss)
    {
        OnNoShield += boss.NoShield;
    }
    IEnumerator Hurt()
    {
        couleur = shield.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        couleur.a = 0.300f;
        shield.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
        yield return new WaitForSeconds(0.15f);
        couleur.a = 0.439f;
        shield.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && nbShieldMaker == 0)
        {
            StartCoroutine("Hurt");
            currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();
            //Debug.Log("bullet");

        }

        if (currentHealth <= 0)
        {
            OnNoShield?.Invoke();
            OnKilledEnemy?.Invoke();
            Destroy(gameObject);
            //Debug.Log("j'appelle levent");

        }

    }
}

