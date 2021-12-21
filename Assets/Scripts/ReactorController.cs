using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ReactorController : MonoBehaviour
{
    public VisualEffect reactor;

    void Update()
    {
        //si on avance on augmente la taille du reacteur
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
