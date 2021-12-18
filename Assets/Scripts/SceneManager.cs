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
        UnityEngine.SceneManagement.SceneManager.LoadScene( 1);
    }
    public void LaunchScene2()
    {
        print("laod scène 2");

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void LaunchScene3()
    {
        print("laod scène 3");
        UnityEngine.SceneManagement.SceneManager.LoadScene( 3);
    }
}
