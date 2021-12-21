using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPen : Bullet
{
    [SerializeField]
    private float speed = 10f;
    private Camera m_MainCamera;

    [SerializeField]
    private int pierce;

    private void Awake()
    {
        m_MainCamera = Camera.main;
        damage = 30 + PlayerPrefs.GetInt("MainWeaponWeaponLevel01") * 2; //recuperation du niveau de l'arme pour augmenter les degats
        shootRate = 5f;
        pierce = 2;
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

    //on ne detruit la balle qu'apres avoir traverse plusieurs ennemis
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            pierce -= 1;
        }
        if(pierce <=0)
        {
            Destroy(gameObject);
        }

    }
}
