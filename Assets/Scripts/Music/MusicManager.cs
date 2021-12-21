using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource bossMusic;
    private bool flagBoss = false;
    // Start is called before the first frame update
    public void BossOnMap()// si le boss pop
    {
        flagBoss = true;
    }
    public void BossNoMoreOnMap()//si le boss meurt
    {
        flagBoss = false;
    }
    public void Update()
    {
        if (flagBoss) // si un boss apparait
        {
            if(bossMusic.isPlaying == false) 
                bossMusic.Play(); // on joue la musique de boss
            backgroundMusic.mute = true;
        }
        else
        {
            bossMusic.Stop();
            backgroundMusic.mute = false; // on démute la musique de fond
        }
    }
}
