using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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

}
