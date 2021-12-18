using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class BonusScript : MonoBehaviour
{
    static Camera m_mainCamera;
    public Transform target;
    // Start is called before the first frame update
    private void Awake()
    {
        m_mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            Vector3 screenPos = BonusScript.m_mainCamera.WorldToViewportPoint(target.position);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (2F * Time.deltaTime), this.transform.position.z);
            if (screenPos.y < -0.1F)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
