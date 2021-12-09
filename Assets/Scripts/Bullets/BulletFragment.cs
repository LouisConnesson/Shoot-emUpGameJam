using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFragment : Bullet
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 10f;
    private Camera m_MainCamera;



    private void Awake()
    {
        m_MainCamera = Camera.main;
        damage = 500;
        shootRate = 10f;
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
        //rb.AddForce(new Vector3(0,1,0) *0.5f * Time.deltaTime,ForceMode.Impulse);
        rb.velocity = transform.TransformDirection(new Vector3(speed, 0, 0) * 0.007f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Destroy(gameObject,0.2f);

        }
    }
}
