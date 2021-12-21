using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneManager : MonoBehaviour
{

    public void LaunchScene1()
    {
        PlayerPrefs.SetInt("Level", 0);
        Time.timeScale = 1;
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene( 1);
    }
    public void LaunchScene2()
    {
        PlayerPrefs.SetInt("Level", 1);
        Time.timeScale = 1;

        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void LaunchScene3()
    {
        PlayerPrefs.SetInt("Level", 2);
        Time.timeScale = 1;

        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene( 3);
    }
    public void LaunchScene4()
    {
        PlayerPrefs.SetInt("Level", 3);
        Time.timeScale = 1;

        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }
    public void LaunchSurvival()
    {
        PlayerPrefs.SetInt("Level", 4);
        Time.timeScale = 1;

        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }


}
