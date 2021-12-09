using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] starships = new GameObject[3];
    public GameObject[] mainWeapon = new GameObject[2];
    public GameObject[] secondWeapons = new GameObject[3];
    private void Awake()
    {
        int id_starship = PlayerPrefs.GetInt("Starship");
        Instantiate(starships[id_starship], new Vector3(0, 8, -5), Quaternion.Euler(180f, 180f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resetLevel()
    {
        Scene sceneActuelle = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneActuelle.buildIndex);
    }
}
