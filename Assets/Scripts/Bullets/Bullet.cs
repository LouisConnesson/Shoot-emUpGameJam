using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float shootRate;
    public int GetBulletDamage()
    {
        return damage;
    }
    public float GetBulletRate()
    {
        return shootRate;
    }
    // Update is called once per frame

}
