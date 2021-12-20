using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ReactorController : MonoBehaviour
{
    // Start is called before the first frame update
    public VisualEffect reactor;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            reactor.SetFloat("MinLife", 0.2f);
            reactor.SetFloat("MaxLife", 0.4f);
        }
        else
        {
            reactor.SetFloat("MinLife", 0.05f);
            reactor.SetFloat("MaxLife", 0.1f);
        }
    }
}
