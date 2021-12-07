using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip startMusic;
    public AudioClip gameMusic;
    public AudioClip endMusic;

    private AudioSource music;

    static MusicPlayer instance = null;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startMusic;
            music.loop = true;
            music.Play();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        music.Stop();

        if (level == 0)
        {
            music.clip = startMusic;
        }
        if (level == 1)
        {
            music.clip = gameMusic;
        }
        if (level == 2)
        {
            music.clip = endMusic;
        }

        music.loop = true;
        music.Play();
    }
}
