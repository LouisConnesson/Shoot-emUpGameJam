using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource bossMusic;
    private bool flagBoss = false;
    // Start is called before the first frame update
    public void BossOnMap()
    {
        flagBoss = true;
    }
    public void BossNoMoreOnMap()
    {
        flagBoss = false;
    }
    public void Update()
    {
        if (flagBoss)
        {
            if(bossMusic.isPlaying == false)
                bossMusic.Play();
            backgroundMusic.mute = true;
        }
        else
        {
            bossMusic.Stop();
            backgroundMusic.mute = false;
        }
    }
}
