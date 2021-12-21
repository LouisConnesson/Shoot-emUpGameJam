using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class CDShield : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject m_player;
    public Material shield;
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player)
        {
            if (m_player.GetComponent<PlayerController>().timer3.ElapsedMilliseconds <= m_player.GetComponent<PlayerController>().cdShield)
            {
                Color color = new Color(191f, 4f, 17f);
                shield.SetColor("_Frontcolor", color * 0.2f);
            }
            else
            {
                Color color = new Color(4, 191f, 163f);
                shield.SetColor("_Frontcolor", color * 2);
            }
        }
        
    }
}
