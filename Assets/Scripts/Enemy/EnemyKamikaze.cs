using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class EnemyKamikaze : Entity
{
    static Camera m_mainCamera;
    //bullet
    private Color couleur;
    [SerializeField]
    private GameObject m_player;
    public float moveSpeed = 500f;
    private Rigidbody rb;
    [SerializeField]
    public BonusScript bonusFire;
    private Vector2 movement;


    public delegate void KilledEnemy();
    public event KilledEnemy OnKilledEnemy;

    [SerializeField]
    private int damage = 200;

    private bool isnotDied = true;
    public void Initalize(PlayerController player)
    {

        OnKilledEnemy += player.OnBulletHit;

        maxHealth = 50;
        currentHealth = maxHealth;
        m_mainCamera = Camera.main;
        if(player)
        {
            m_player = player.gameObject;
            rb = m_player.GetComponent<Rigidbody>();

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && isnotDied)
        {
            if (m_player)
            {
                Vector3 direction = (m_player.transform.position - transform.position);
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, 0));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);


                movement = direction;
            }

        }

    }
    private void FixedUpdate()
    {
        if (m_player)
            Move(movement);
    }

    private void Move(Vector2 direction)
    {
        
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_player.transform.position, step);
        //rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        

    }


    IEnumerator Hurt()
    {
       
        couleur = this.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        couleur.g = 0.0f;

        this.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", couleur);
        yield return new WaitForSeconds(0.15f);
        couleur.g = 0.868f;
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

        }

        if (currentHealth <= 0)
        {
            OnKilledEnemy?.Invoke();
            Vector3 tmpos = transform.position;
            this.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine("died");

            if (other.GetComponent<BulletFragment>())
            {
                print("AHAHAHAHHAHAHAHAHHHHHHHHHHHHH");
                for (int i = 0; i < 8; i++)
                    Instantiate(other.gameObject, other.transform.position, Quaternion.Euler(i * 45, 90f, 90f));
                other.gameObject.SetActive(false);
            }

            //Debug.Log("j'appelle levent");

        }

    }
    IEnumerator died() //La coroutine sert à désactiver partiellement le monstre pour jouer le son de mort avant de le supprimer pour de bons à la fin
    {
        isnotDied = false;
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<MeshRenderer>().enabled = false;
        int currKilled = PlayerPrefs.GetInt("Success7")+1;
        PlayerPrefs.SetInt("Success7", currKilled);

            //Destroy(spikeball);
            if (Random.Range(0, 10) <= 2)
        {
            BonusScript m = Instantiate(bonusFire) as BonusScript;
            m.transform.position = transform.position;
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    public int GetDamage(){
        return damage;
    }
}
