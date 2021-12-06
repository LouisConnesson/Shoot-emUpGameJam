using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public CharacterController cc;
    public Transform target;
    public Camera m_MainCamera;
    public float moveSpeed;
    private float X;
    private float Y;
    private Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = m_MainCamera.WorldToViewportPoint(target.position);
        X = (Input.GetAxis("Horizontal") * moveSpeed) * -1;
        Y = Input.GetAxis("Vertical") * moveSpeed;

        //On vérifie si le joueur quitte le champ d'action de la caméra et on l'en empêche
        if (screenPos.y > 1F)
        {
            Y = -1;
        }
        else if (screenPos.y < 0F)
        {
            Y = 1;
        }
        if (screenPos.x > 1F)
        {
            X = 1;
        }
        else if (screenPos.x < 0F)
        {
            X = -1;
        }
        // On remplie moveDir avant d'effectuer le mouvement
        moveDir = new Vector3(X, Y, moveDir.z);

        cc.Move(moveDir * Time.deltaTime);
    }
}
