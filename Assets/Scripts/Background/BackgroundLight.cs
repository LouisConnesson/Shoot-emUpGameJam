using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLight : Bullet
{
    // Start is called before the first frame update
    [SerializeField]
    public float speed = 10f;
    private Camera m_MainCamera;



    private void Awake()
    {
        m_MainCamera = Camera.main;
        shootRate = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        bulletMovment();

        Vector3 screenPos = m_MainCamera.WorldToViewportPoint(transform.position);
        if (screenPos.y < -0.1F)
        {
            Destroy(gameObject);
        }
    }
    void bulletMovment()
    {
        Vector3 moveDir = new Vector3(0, 0, 1);
        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(0,1,0) *0.5f * Time.deltaTime,ForceMode.Impulse);
        rb.velocity = transform.TransformDirection(new Vector3(0, 0, speed) * 0.7f);

    }

}
