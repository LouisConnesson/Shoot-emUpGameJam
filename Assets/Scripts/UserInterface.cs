using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviourSingleton<UserInterface>
{
    public GameObject pause;
    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && flag == false)
        {
            pause.SetActive(true);
            Time.timeScale = 0;
            flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && flag == true)
        {
            pause.SetActive(false);
            Time.timeScale = 1;
            flag = false;
        }
    }

    public void rerunTime()
    {
        Time.timeScale = 1;
    }
}
