using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    public void LaunchScene1()
    {
        print("laod sc�ne 1");
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene( 1);
    }
    public void LaunchScene2()
    {
        print("laod sc�ne 2");
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void LaunchScene3()
    {
        print("laod sc�ne 3");
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene( 3);
    }
}
