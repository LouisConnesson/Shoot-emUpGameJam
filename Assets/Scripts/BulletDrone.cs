using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrone : Bullet
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 10f;
    private Camera m_MainCamera;
    GameObject player;

    private void Awake()
    {
        m_MainCamera = Camera.main;
        damage = 1000;
        shootRate = 1f;
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
    public void GetPlayer(GameObject p_player)
    {
        player = p_player;
    }
   
    void bulletMovment()
    {
        Vector3 moveDir = new Vector3(0, 0, 1);
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(0,1,0) *0.5f * Time.deltaTime,ForceMode.Impulse);
        //rb.velocity = transform.TransformDirection(new Vector3(0, 0, speed) * 0.007f);

        int distance = 5;
        Vector3 maintainDistance= (transform.position - player.transform.position).normalized * distance + player.transform.position;
        maintainDistance.z = transform.position.z;
        transform.position = maintainDistance;

        transform.RotateAround(player.transform.position,new Vector3(0,0,1),100*Time.deltaTime);
        
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Destroy(gameObject);

        }
    }
}
