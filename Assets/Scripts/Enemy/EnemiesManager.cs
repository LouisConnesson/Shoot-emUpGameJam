using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public Enemy Mob;
    public EnemyTorp MobTorp;
    private int waveFlag = 0;
    public Transform target;
    [SerializeField]
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
        StartCoroutine(SpawnerWave());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void spawnEnemy()
    {
        if (Random.Range(0f, 100f) < 10)
        {
            Enemy m = Instantiate(Mob) as Enemy;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);
        }
        else
        {
            EnemyTorp m = Instantiate(MobTorp) as EnemyTorp;
            m.Initalize(player);
            m.transform.position = new Vector3(Random.Range(-8, 9), 25, -5);
        }
    }
    private void spawnWave()
    {
        if (waveFlag == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Enemy m = Instantiate(Mob) as Enemy;
                m.Initalize(player);
                m.transform.position = new Vector3(-5 + i * 4, 20 + i * 2, -2.8F);
            }
            waveFlag++;
        }
        else if (waveFlag == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                Enemy m = Instantiate(Mob) as Enemy;
                m.Initalize(player);
                m.transform.position = new Vector3(-8.5f + i * 4, 20, -5);
            }
            waveFlag = 0;
        }
    }
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.0f);
            spawnEnemy();
        }
    }
    IEnumerator SpawnerWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            spawnWave();
        }
    }
}
