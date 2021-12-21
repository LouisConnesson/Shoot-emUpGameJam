using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemySniper : Bullet
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 100f;
    private Camera m_MainCamera;



    private void Awake()
    {
        m_MainCamera = Camera.main;
        damage = 60;
        shootRate = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = m_MainCamera.WorldToViewportPoint(transform.position);
        if (screenPos.y < -0.5F || screenPos.y > 1.5F || screenPos.x > 1.5F || screenPos.x < -0.5F)
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