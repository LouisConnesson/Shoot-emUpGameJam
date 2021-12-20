using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    public void LaunchScene1()
    {
        print("laod scène 1");
        PlayerPrefs.SetInt("Level", 0);
        Time.timeScale = 1;
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene( 1);
    }
    public void LaunchScene2()
    {
        print("laod scène 2");
        PlayerPrefs.SetInt("Level", 1);
        Time.timeScale = 1;

        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void LaunchScene3()
    {
        print("laod scène 3");
        PlayerPrefs.SetInt("Level", 2);
        Time.timeScale = 1;

        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene( 3);
    }
    public void LaunchScene4()
    {
        print("laod scène 4");
        PlayerPrefs.SetInt("Level", 3);
        Time.timeScale = 1;

        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }
    public void LaunchSurvival()
    {
        print("laod scène 5");
        PlayerPrefs.SetInt("Level", 4);
        Time.timeScale = 1;

        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

}
