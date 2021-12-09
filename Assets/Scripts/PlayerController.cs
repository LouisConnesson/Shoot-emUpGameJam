using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PlayerController : Entity
{
    public CharacterController cc;
    public Transform target;
    public Camera m_MainCamera;
    public float moveSpeed;
    private float X;
    private float Y;
    private Vector3 moveDir;

    //bullet
    [SerializeField]
    private float shootRate =0;
    public GameObject bulletPrefab;
    public Stopwatch timer;
    public Transform bulletSpawn;

    [SerializeField]
    private int p_score;
    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();
        maxHealth = 500;
        currentHealth = maxHealth;
        shootRate = bulletPrefab.GetComponent<Bullet>().GetBulletRate();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = m_MainCamera.WorldToViewportPoint(target.position);
        X = (Input.GetAxis("Horizontal") * moveSpeed) * -1;
        Y = Input.GetAxis("Vertical") * moveSpeed;

        //On vérifie si le joueur quitte le champ d'action de la caméra et on l'en empêche
        if (screenPos.y > 1F)
        {
            Y = -1;
        }
        else if (screenPos.y < 0F)
        {
            Y = 1;
        }
        if (screenPos.x > 1F)
        {
            X = 1;
        }
        else if (screenPos.x < 0F)
        {
            X = -1;
        }
        // On remplie moveDir avant d'effectuer le mouvement
        moveDir = new Vector3(X, Y, moveDir.z);

        cc.Move(moveDir * Time.deltaTime);

        //instancier les tirs
        if (Time.timeScale == 1) //si le temps n'est pas en pause
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (timer.ElapsedMilliseconds >= 1000 / shootRate)
                {
                    Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.Euler(0f, 0f, 90f));
                    timer.Restart();
                }
            }
        }
    }

    public int getPlayerScore()
    {
        return p_score;
    }
    public int getmaxHealth()
    {
        return maxHealth;
    }
    public int getmcurrentHealth()
    {
        return currentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            addDamage(50);
        }
        if (other.gameObject.tag == "bulletEnemy")
        {
            addDamage(10); 
        }
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
    private void addDamage(int dmg)
    {
        currentHealth -= dmg;
    }
    public void OnBulletHit()
    {
        p_score += 1;
    }
}
