using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    static Camera m_mainCamera;
    public Transform target;

    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;


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
        if(Time.timeScale ==1)
        {
            Vector3 screenPos = Enemy.m_mainCamera.WorldToViewportPoint(target.position);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.02F, this.transform.position.z);
            if (screenPos.y < 0.0F)
            {
                Destroy(gameObject);
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   
            currentHealth = 0;

        }
        if (other.gameObject.tag == "Bullet")
        {
            currentHealth -= 10;

        }

        if (currentHealth <= 0)
        {
            OnKilledEnemy?.Invoke();
            Destroy(gameObject);

        }

    }
}
