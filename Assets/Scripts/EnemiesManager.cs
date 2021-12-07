using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public Enemy Mob;
    public Transform target;
    [SerializeField]
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void spawnEnemy()
    {
        Enemy m = Instantiate(Mob) as Enemy;
        m.Initalize(player);
        m.transform.position = new Vector3(Random.Range(-5, 15), 18, -5);
    }
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.0f);
            spawnEnemy();
        }
        

    }
}
