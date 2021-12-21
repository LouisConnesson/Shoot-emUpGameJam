using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFragment : Bullet
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 10f;
    private Camera m_MainCamera;

    public GameObject prefab;
    private void Awake()
    {
        m_MainCamera = Camera.main;
        damage = 40 + PlayerPrefs.GetInt("SecondWeaponWeaponLevel02") * 2; //recuperation du niveau de l'arme pour augmenter les degats
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
        rb.velocity = transform.TransformDirection(new Vector3(0, 0, speed) * 0.007f);
    }

    //la fragmentation se fait du cote de l'ennemi
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Destroy(gameObject,0.01f);
        }
    }
}
