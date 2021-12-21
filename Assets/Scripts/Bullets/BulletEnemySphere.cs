using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemySphere : Bullet
{
    [SerializeField]
    private float speed = 0.1f;
    private Camera m_MainCamera;

    private void Awake()
    {
        m_MainCamera = Camera.main;
        shootRate = 2f;
    }

    void Update()
    {
        bulletMovment();

        Vector3 screenPos = m_MainCamera.WorldToViewportPoint(transform.position);
        if (screenPos.y < -0.5F || screenPos.y > 1.5F ||screenPos.x >1.5F || screenPos.x <-0.5F)
        {
            Destroy(gameObject);
        }
    }
    void bulletMovment()
    {
        Vector3 moveDir = new Vector3(0, 0, 1);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(new Vector3(this.transform.rotation.z, -speed, 0) * 0.007f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);

        }
    }
}