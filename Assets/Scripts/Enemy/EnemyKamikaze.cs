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
    public float moveSpeed = 10f;
    private Rigidbody rb;
    [SerializeField]
    private Vector2 movement;

    [SerializeField]
    private Vector2 directionpub;

    public void Initalize(PlayerController player)
    {
        maxHealth = 50;
        currentHealth = maxHealth;
        m_mainCamera = Camera.main;
        m_player = player.gameObject;
        rb = m_player.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            Vector3 direction = (m_player.transform.position - transform.position);
            directionpub = direction;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y,0));
            transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime * 5f);

      
            movement = direction;
        }

    }
    private void FixedUpdate()
    {
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

        }

        if (currentHealth <= 0)
        {
            if (other.GetComponent<BulletFragment>())
            {
                for (int i = 0; i < 8; i++)
                    Instantiate(other.gameObject, transform.position, Quaternion.Euler(0, 0, i * 45));

            }
            Destroy(gameObject);


        }

    }
}
