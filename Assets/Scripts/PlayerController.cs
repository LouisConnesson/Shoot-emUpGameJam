using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class PlayerController : Entity
{
    public CharacterController cc;
    public GameObject hitBox;
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
    public Stopwatch timer3;
    public Stopwatch timer4;
    public Transform bulletSpawn;

    [SerializeField]
    private int p_score;

    public GameObject muzzle;
    private float radius = 1f;

    public GameObject shield;
    private bool isShielEnable = false;
    private bool isShielActivated = false;
    [SerializeField]
    private float shieldTime = 0f;
    private int numberproj = 5;
    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();

        timer2 = new Stopwatch();
        timer2.Start();

        timer3 = new Stopwatch();
        timer3.Start();

        timer4 = new Stopwatch();

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


        shieldTime = (2 + PlayerPrefs.GetInt("Shield") * 0.5f)*1000;
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
                if (timer.ElapsedMilliseconds >= 1000 / shootRate)
                {
                    Vector3 bulletpos = new Vector3(bulletSpawn.position.x, bulletSpawn.position.y, bulletSpawn.position.z+1);
                    GameObject bullet = Instantiate(bulletPrefab, bulletpos, Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
                    if(bullet.GetComponent<BulletDrone>())
                    {
                        bullet.GetComponent<BulletDrone>().GetPlayer(this.gameObject);
                    }
                    //Instantiate(bulletPrefab, bulletpos, Quaternion.Euler(-90f, 0f, 0f));
                    timer.Restart();
                    Destroy(Instantiate(muzzle, bulletSpawn.position, Quaternion.Euler(0f, 0f, 0f)),0.2f);

                }
            }

            if (Input.GetKey(KeyCode.E) && PlayerPrefs.GetInt("SecondWeapon") != 0)
            {
                if (timer2.ElapsedMilliseconds >= 1000 / shootRate2)
                {
                    Instantiate(bulletPrefab2, bulletSpawn.position, Quaternion.Euler(-90f, 0f, 0f));
                    timer2.Restart();
                }
            }
            if (Input.GetKey(KeyCode.A) && PlayerPrefs.GetInt("Shield") != 0)
            {
                if (timer3.ElapsedMilliseconds >= 5000)
                {
                    //Instantiate(shield, bulletSpawn.position, Quaternion.Euler(0f, 0f, 0f));
                    timer3.Restart();
                    timer4.Restart();
                    isShielActivated = true;
                }
            }
            if (Input.GetKey(KeyCode.Z))
            {
                hitBox.GetComponent<MeshRenderer>().enabled = true;
            }
            else
                hitBox.GetComponent<MeshRenderer>().enabled = false;

            if(timer4.ElapsedMilliseconds >= shieldTime)
            {
                shield.SetActive(false);
                isShielEnable = false;
                isShielActivated = false;
            }
            else if(isShielActivated && timer4.ElapsedMilliseconds < shieldTime)
            {
                shield.SetActive(true);
                isShielEnable = true;
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
        if (other.gameObject.tag == "enemy" )
        {
            this.GetComponent<AudioSource>().Play();
            if(other.GetComponent<EnemyKamikaze>())
            {
                if (isShielEnable == false)
                    currentHealth -= other.GetComponent<EnemyKamikaze>().GetDamage();
            }
            else
                if (isShielEnable == false)
                    addDamage(50);
        }
        if (other.gameObject.tag == "bulletEnemy")
        {
            this.GetComponent<AudioSource>().Play();
            if(isShielEnable == false)
                currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();
        }
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
    private void addDamage(int dmg)
    {
        print("bb");
        currentHealth -= dmg;
    }
    public void OnBulletHit()
    {
        p_score += 1;
    }
}
