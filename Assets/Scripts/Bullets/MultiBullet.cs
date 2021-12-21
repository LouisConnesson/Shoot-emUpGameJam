using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBullet : Bullet
{
    [SerializeField]
    private float speed = 10f;
    private Camera m_MainCamera;

    private void Awake()
    {
        m_MainCamera = Camera.main;
        damage = 10 + PlayerPrefs.GetInt("SecondWeaponWeaponLevel01") * 2;
        shootRate = 10f;
    }

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
            Destroy(gameObject);

        }
    }
}
