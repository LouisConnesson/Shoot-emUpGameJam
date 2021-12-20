using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class BackgroundManager : MonoBehaviour
{
    public BackgroundLight lights;
    public BackgroundPlanet planet;
    public BackgroundPlanet planet2;
    private Stopwatch timer;
    private Stopwatch timerplanet;
    private Stopwatch timerplanet2;
    private bool flag = true;
    private bool flag2 = true;
    private void Awake()
    {
        timer = new Stopwatch();
        timer.Start();
        timerplanet = new Stopwatch();
        timerplanet.Start();
        timerplanet2 = new Stopwatch();
        timerplanet2.Start();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (timer.ElapsedMilliseconds >= (200))
            {
                Instantiate(lights, new Vector3(Random.Range(-10f, 10f), 20f, -20F), Quaternion.Euler(90, 0, 0));
                timer.Restart();
            }
            if (timerplanet.ElapsedMilliseconds >= (2000) && flag)
            {
                Instantiate(planet, new Vector3(Random.Range(-9f, 8f), 23f, -200F), Quaternion.Euler(45, 45, 0));
                timerplanet.Restart();
                flag = false;
            }
            else if (timerplanet.ElapsedMilliseconds >= (68000))
            {
                flag = true;
            }

            if (timerplanet2.ElapsedMilliseconds >= (800) && flag2)
            {
                Instantiate(planet2, new Vector3(Random.Range(-9f, 8f), 20f, -150F), Quaternion.Euler(45, 45, 0));
                timerplanet2.Restart();
                flag2 = false;
            }
            else if (timerplanet2.ElapsedMilliseconds >= (50000))
            {
                flag2 = true;
            }
        }
    }
}
