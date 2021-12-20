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

    private GameObject starship;
    private void Awake()
    {
        int id_starship = PlayerPrefs.GetInt("Starship");
        starship = Instantiate(starships[id_starship], new Vector3(0, 8, -5), Quaternion.Euler(180f, 180f, 0f));
        starship.transform.position = new Vector3(0, 5, -5);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void resetLevel()
    {
        PlayerPrefs.Save();
        Scene sceneActuelle = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneActuelle.buildIndex);
    }
    public void LaunchMenu()
    {
        if(PlayerPrefs.GetInt("Level") == 4)
        {
            List<int> list = new List<int>();
            list.Add(PlayerPrefs.GetInt("Score1"));
            list.Add(PlayerPrefs.GetInt("Score2"));
            list.Add(PlayerPrefs.GetInt("Score3"));
            list.Add(PlayerPrefs.GetInt("Score4"));
            list.Add(PlayerPrefs.GetInt("Score5"));
            list.Add(PlayerPrefs.GetInt("Score6"));
            list.Add(PlayerPrefs.GetInt("Score7"));
            list.Add(PlayerPrefs.GetInt("Score8"));
            list.Add(PlayerPrefs.GetInt("Score9"));
            list.Add(PlayerPrefs.GetInt("Score10"));

            int newScore = starship.GetComponent<PlayerController>().p_score;
            list.Add(newScore);
            list.Sort();
            PlayerPrefs.SetInt("Score1", list[1]);
            PlayerPrefs.SetInt("Score2", list[2]);
            PlayerPrefs.SetInt("Score3", list[3]);
            PlayerPrefs.SetInt("Score4", list[4]);
            PlayerPrefs.SetInt("Score5", list[5]);
            PlayerPrefs.SetInt("Score6", list[6]);
            PlayerPrefs.SetInt("Score7", list[7]);
            PlayerPrefs.SetInt("Score8", list[8]);
            PlayerPrefs.SetInt("Score9", list[9]);
            PlayerPrefs.SetInt("Score10", list[10]);
        }
        

        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
