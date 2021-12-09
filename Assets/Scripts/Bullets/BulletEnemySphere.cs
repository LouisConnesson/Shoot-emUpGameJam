using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemySphere : Bullet
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 6f;
    private Camera m_MainCamera;
 
 


    private void Awake()
    {
        m_MainCamera = Camera.main;
        damage = 20;
        shootRate = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        bulletMovment();

        Vector3 screenPos = m_MainCamera.WorldToViewportPoint(transform.position);
        if (screenPos.y < -0.1F || screenPos.y > 1.1F ||screenPos.x >1.1F || screenPos.x <-0.1F)
        {
            Destroy(gameObject);
        }
    }
    void bulletMovment()
    {
        Vector3 moveDir = new Vector3(0, 0, 1);
        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(0,1,0) *0.5f * Time.deltaTime,ForceMode.Impulse);

        rb.velocity = transform.TransformDirection(new Vector3(this.transform.rotation.z, -speed, 0) * 0.007f);
        // rb.transform.position = new Vector3(rb.transform.position.x * Time.deltaTime, rb.transform.position.y * Time.deltaTime, rb.transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);

        }
    }
}