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
    void Start()
    {
    }
    /*void SetCurrentHealth(int dmg)
    {
        currentHealth -= dmg;
    }*/
    // Update is called once per frame
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
    }
    void Update()
    {
        
    }
}
