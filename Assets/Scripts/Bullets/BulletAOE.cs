using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAOE : Bullet
{
    [SerializeField]
    private float speed = 10f;
    private Camera m_MainCamera;

    private void Awake()
    {
        m_MainCamera = Camera.main;
        damage = 50  +PlayerPrefs.GetInt("SecondWeaponWeaponLevel03") * 2; //recuperation du niveau de l'arme pour augmenter les degats
        shootRate = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        bulletMovment();

        Vector3 screenPos = m_MainCamera.WorldToViewportPoint(transform.position);
        if (screenPos.y > 1F)
        {
            Destroy(gameObject);
        }
    }
    void bulletMovment()
    {
        Vector3 moveDir = new Vector3(0, 0, 1);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(new Vector3(0, 0, speed) * 0.007f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            CheckForDestructibles();

            Destroy(gameObject);
        }
    }
    //fonction permettant de chercher les ennemis autour
    private void CheckForDestructibles()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,8f);

        foreach(Collider c in colliders)
        {
            if (c.tag == "enemy")
            {
                Entity tmp = c.GetComponent<Entity>();
                if (tmp != null)
                {
                    //on applique les degats a tous les ennemis
                    c.GetComponent<Entity>().TakeDamage(GetBulletDamage());
                }


            }
        }
    }
}
