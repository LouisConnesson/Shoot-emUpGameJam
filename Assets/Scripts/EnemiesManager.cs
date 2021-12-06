using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public GameObject Mob;
    public Transform target;

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
        GameObject m = Instantiate(Mob) as GameObject;
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
