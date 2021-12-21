using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    protected int currentHealth;
    // Start is called before the first frame update
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
    }
}
