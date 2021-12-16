using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPlanet : Bullet
{
    // Start is called before the first frame update
    [SerializeField]
    public float speed;
    private Camera m_MainCamera;



    private void Awake()
    {
        m_MainCamera = Camera.main;
        shootRate = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        bulletMovment();

        Vector3 screenPos = m_MainCamera.WorldToViewportPoint(transform.position);
        if (screenPos.y < -0.5F)
        {
            Destroy(gameObject);
        }
    }
    void bulletMovment()
    {
        Vector3 moveDir = new Vector3(0, 0, 1);
        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(0,1,0) *0.5f * Time.deltaTime,ForceMode.Impulse);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (speed * Time.deltaTime), this.transform.position.z);

    }

}

