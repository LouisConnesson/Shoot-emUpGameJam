using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static Camera m_mainCamera;
    public Transform target;
    private void Awake()
    {
        m_mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Enemy.m_mainCamera.WorldToViewportPoint(target.position);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.02F, this.transform.position.z);
        if(screenPos.y < 0.0F)
        {
            Destroy(gameObject);
        }
    }
}
