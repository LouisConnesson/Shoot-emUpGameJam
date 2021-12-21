using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class PlayerController : Entity
{
    //parametres
    public GameObject upReactor;
    public GameObject downReactor;
    public CharacterController cc;
    public GameObject hitBox;
    public Transform target;
    public Camera m_MainCamera;
    public float moveSpeed;
    [SerializeField]
    private float X;
    private float Y;
    private Vector3 moveDir;
    public int p_score;

    //armes
    [SerializeField]
    private float shootRate =0;
    [SerializeField]
    private float shootRate2 = 0;
    [SerializeField]
    private GameObject bulletPrefab;
    private float shootRateSave = 0;
    [SerializeField]
    private GameObject bulletPrefab2;
    private int p_power = 0;
    public GameObject muzzle;

    public GameObject shield;
    private bool isShielEnable = false;
    private bool isShielActivated = false;
    public float shieldTime = 0f;

    public int main_id;
    [SerializeField]
    public int second_id;
    private Animator anim;

    private int currentAmmoPR = 0;
    private int currentAmmoSD = 0;

    public int cdShield;
    private int currMenuPoints;
    //timers
    public Stopwatch timer;
    public Stopwatch timer2;
    public Stopwatch timer3;
    public Stopwatch timer4;
    public Stopwatch timerFlame;
    private bool flameFlag = false;
    public Transform bulletSpawn;



    private bool cheatShield = false;
    private void Awake()
    {
        cdShield = 15000;
        anim = GetComponent<Animator>();

        timer = new Stopwatch();
        timer.Start();

        timer2 = new Stopwatch();
        timer2.Start();

        timer3 = new Stopwatch();
        timer3.Start();

        timer4 = new Stopwatch();

        timerFlame = new Stopwatch();
        timerFlame.Start();

        maxHealth = 500;
        currentHealth = maxHealth;

        //selection des armes
        main_id = PlayerPrefs.GetInt("MainWeapon");
        second_id = PlayerPrefs.GetInt("SecondWeapon");
        bulletPrefab = FindObjectOfType<GameManager>().mainWeapon[main_id];
        bulletPrefab2 = FindObjectOfType<GameManager>().secondWeapons[second_id];
        shootRate = bulletPrefab.GetComponent<Bullet>().GetBulletRate();
        shootRateSave = shootRate;
        if(second_id !=0 )
            shootRate2 = bulletPrefab2.GetComponent<Bullet>().GetBulletRate();
        else
            shootRate2 =0;

        shieldTime = (0.5f + PlayerPrefs.GetInt("Shield") * 0.25f) * 1000;
        currMenuPoints = PlayerPrefs.GetInt("Points");

        m_MainCamera = FindObjectOfType<Camera>();


    }

    void Update()
    {
        //sauvegarde du score pour le classement
        PlayerPrefs.SetInt("ScoreTMP", p_score);
        //ajout des points
        PlayerPrefs.SetInt("Points", currMenuPoints + p_score);
        m_MainCamera = FindObjectOfType<Camera>();

        Vector3 screenPos = m_MainCamera.WorldToViewportPoint(target.position);
        
        X = (Input.GetAxis("Horizontal") * moveSpeed) * -1;
        Y = Input.GetAxis("Vertical") * moveSpeed;
        if (X < 0)
        {

            anim.SetBool("isHorizontal", true);
            anim.SetBool("isRight", true);
            anim.SetBool("isLeft", false);
        }
        else if(X > 0 )
        {
            anim.SetBool("isHorizontal", true);
            anim.SetBool("isRight", false);
            anim.SetBool("isLeft", true);

        }
        else
        {
            anim.SetBool("isHorizontal", false);

        }
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
        if (Time.timeScale != 0) //si le temps n'est pas en pause
        {
           
            if (main_id == 0)
                currentAmmoPR = 1;
            if (main_id == 1)
            currentAmmoPR = PlayerPrefs.GetInt("Current1");

            if (second_id == 1)
                currentAmmoSD = PlayerPrefs.GetInt("Current2");
            if (second_id == 2)
                currentAmmoSD = PlayerPrefs.GetInt("Current3");
            if (second_id == 3)
                currentAmmoSD = PlayerPrefs.GetInt("Current4");
            if (second_id == 4)
                currentAmmoSD = PlayerPrefs.GetInt("Current5");

            //bouclier demo
            cheatShield = false;
            if (Input.GetKey(KeyCode.M))
                cheatShield = true;

            //tir arme principale
            if (Input.GetKey(KeyCode.Space) && Time.timeScale !=0 && currentAmmoPR > 0)
            {
                if (timer.ElapsedMilliseconds >= 1000 / (shootRate * Time.timeScale))
                {
                    Vector3 bulletpos = new Vector3(bulletSpawn.position.x, bulletSpawn.position.y, bulletSpawn.position.z+1);
                    GameObject bullet = Instantiate(bulletPrefab, bulletpos, Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
                    if(bullet.GetComponent<BulletDrone>())
                    {
                        bullet.GetComponent<BulletDrone>().GetPlayer(this.gameObject);
                    }
                    timer.Restart();
                    Destroy(Instantiate(muzzle, bulletSpawn.position, Quaternion.Euler(0f, 0f, 0f)),0.2f);
                    //munitions
                    if (main_id == 1)
                    {
                        int curr = PlayerPrefs.GetInt("Current1") - 1;
                        PlayerPrefs.SetInt("Current1", curr);

                    }

                }
            }
            //tir arme secondaire
            if (Input.GetKey(KeyCode.E) && second_id != 0 && Time.timeScale!=0 && currentAmmoSD > 0)
            {
                if (timer2.ElapsedMilliseconds >= 1000 / (shootRate2*Time.timeScale))
                {
                    //munitions
                    if (second_id ==1)
                    {
                        if(currentAmmoSD >= 3)
                        {
                            Instantiate(bulletPrefab2, bulletSpawn.position, Quaternion.Euler(-110f, -90f, 90f));
                            Instantiate(bulletPrefab2, bulletSpawn.position, Quaternion.Euler(-90, -90f, 90f));
                            Instantiate(bulletPrefab2, bulletSpawn.position, Quaternion.Euler(-70f, -90f, 90f));
                            int curr = PlayerPrefs.GetInt("Current2") - 3;
                            PlayerPrefs.SetInt("Current2", curr);
                        }
                    }
                    else
                    {
                        Instantiate(bulletPrefab2, bulletSpawn.position, Quaternion.Euler(-90f, 0f, 0f));

                        if (second_id == 2)
                        {
                            int curr = PlayerPrefs.GetInt("Current3") - 1;
                            PlayerPrefs.SetInt("Current3", curr);
                        }
                        if (second_id == 3)
                        {
                            int curr = PlayerPrefs.GetInt("Current4") - 1;
                            PlayerPrefs.SetInt("Current4", curr);
                        }
                        if (second_id == 4)
                        {
                            int curr = PlayerPrefs.GetInt("Current5") - 1;
                            PlayerPrefs.SetInt("Current5", curr);
                        }

                    }
                    timer2.Restart();
                }
            }
            //bouclier
            if ( (Input.GetKey(KeyCode.A) && PlayerPrefs.GetInt("Shield") != 0 && Time.timeScale !=0) || Input.GetKey(KeyCode.M))
            {
                int tmptime = 0;
                if (cheatShield)
                    tmptime = 0;
                else
                    tmptime = cdShield;

                if (timer3.ElapsedMilliseconds >= tmptime)
                {
                    timer3.Restart();
                    timer4.Restart();
                    isShielActivated = true;
                }
            }
            //hitbox
            if (Input.GetKey(KeyCode.Z))
            {
                hitBox.GetComponent<MeshRenderer>().enabled = true;
            }
            else
                hitBox.GetComponent<MeshRenderer>().enabled = false;

            //gestion du bouclier
            if(timer4.ElapsedMilliseconds >= shieldTime)
            {
                shield.SetActive(false);
                isShielEnable = false;
                isShielActivated = false;
            }
            else if(isShielActivated && timer4.ElapsedMilliseconds < shieldTime && PlayerPrefs.GetInt("Shield") > 0 )
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
    public int getPlayerPower()
    {
        return p_power;
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
        //impact ennemy
        if (other.gameObject.tag == "enemy" )
        {
            this.GetComponent<AudioSource>().Play();
            if(other.GetComponent<EnemyKamikaze>())
            {
                if (isShielEnable == false && PlayerPrefs.GetInt("BossDead") == 0)
                    currentHealth -= other.GetComponent<EnemyKamikaze>().GetDamage();
            }
            else
                if (isShielEnable == false && PlayerPrefs.GetInt("BossDead") == 0)
                    addDamage(50);
            if (isShielEnable == false && PlayerPrefs.GetInt("BossDead") == 0)
            {
                shootRate = shootRateSave;
                p_power = 0;
            }
            //succes no hit
            if (isShielEnable == false && PlayerPrefs.GetInt("BossDead") == 0)
            {
                if (PlayerPrefs.GetInt("Level") == 0)
                    if (PlayerPrefs.GetInt("Success1") != 1)
                        PlayerPrefs.SetInt("Success1", -1);

                if (PlayerPrefs.GetInt("Level") == 1)
                    if (PlayerPrefs.GetInt("Success2") != 1)
                        PlayerPrefs.SetInt("Success2", -1);

                if (PlayerPrefs.GetInt("Level") == 2)
                    if (PlayerPrefs.GetInt("Success3") != 1)
                        PlayerPrefs.SetInt("Success3", -1);

            }


        }
        //impact balle ennemie
        if (other.gameObject.tag == "bulletEnemy")
        {
            this.GetComponent<AudioSource>().Play();
            if (isShielEnable == false && PlayerPrefs.GetInt("BossDead") == 0)
            {
                currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();
                shootRate = shootRateSave;
                p_power = 0;
            }
            //succes no hit
            if (isShielEnable == false && PlayerPrefs.GetInt("BossDead") == 0)
            {
                if (PlayerPrefs.GetInt("Level") == 0)
                    if (PlayerPrefs.GetInt("Success1") != 1)
                        PlayerPrefs.SetInt("Success1", -1);
                if (PlayerPrefs.GetInt("Level") == 1)
                    if (PlayerPrefs.GetInt("Success2") != 1)
                        PlayerPrefs.SetInt("Success2", -1);

                if (PlayerPrefs.GetInt("Level") == 2)
                    if (PlayerPrefs.GetInt("Success3") != 1)
                        PlayerPrefs.SetInt("Success3", -1);

            }
        }
        //bonus
        if (other.gameObject.tag == "Bonus")
        {
            p_score += 2;
        }
        if (other.gameObject.tag == "BonusTorp")
        {
            p_score += 4;
        }
        if (other.gameObject.tag == "BonusFire")
        {
            if (p_power < 10)
            {
                shootRate = shootRate * 1.1f;
                p_power += 1;
            }
            p_score += 1;
        }
        if (other.gameObject.tag == "BonusTime")
        {
            Time.timeScale = 0.65f;
            StartCoroutine("TimeSlow");
            p_score += 1;
        }
        if (other.gameObject.tag == "BonusHeal")
        {
            currentHealth += 60;
            p_score += 1;
        }
        if (currentHealth <= 0)
        {

            Destroy(gameObject);

        }
    }
    private void addDamage(int dmg)
    {
        currentHealth -= dmg;
    }
    public void OnBulletHit()
    {
        p_score += 1;
    }
    IEnumerator TimeSlow()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1;
    }
}
