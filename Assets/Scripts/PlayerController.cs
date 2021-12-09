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
    [SerializeField]
    private float shootRate2 = 0;
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject bulletPrefab2;

    public Stopwatch timer;
    public Stopwatch timer2;
    public Transform bulletSpawn;

    [SerializeField]
    private int p_score;
    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();

        timer2 = new Stopwatch();
        timer2.Start();

        maxHealth = 500;
        currentHealth = maxHealth;
        //selection des armes
        int main_id = PlayerPrefs.GetInt("MainWeapon");
        int second_id = PlayerPrefs.GetInt("SecondWeapon");
        bulletPrefab = FindObjectOfType<GameManager>().mainWeapon[main_id];
        bulletPrefab2 = FindObjectOfType<GameManager>().secondWeapons[second_id];

        shootRate = bulletPrefab.GetComponent<Bullet>().GetBulletRate();
        shootRate2 = bulletPrefab2.GetComponent<Bullet>().GetBulletRate();

        m_MainCamera = FindObjectOfType<Camera>();



    }

    // Update is called once per frame
    void Update()
    {
        m_MainCamera = FindObjectOfType<Camera>();

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
<<<<<<< HEAD
                if (timer.ElapsedMilliseconds >= 1000 / shootRate)
                {
                    Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.Euler(0f, 0f, 90f));
                    timer.Restart();
                }
=======
                Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.Euler(-90f, 0f, 0f));
                timer.Restart();
>>>>>>> origin/main
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (timer2.ElapsedMilliseconds >= 1000 / shootRate2)
            {
                Instantiate(bulletPrefab2, bulletSpawn.position, Quaternion.Euler(-90f, 0f, 0f));
                timer2.Restart();
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
            currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();
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
