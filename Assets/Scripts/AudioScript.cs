using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(AudioSource))]

public class AudioScript : MonoBehaviour
{

    public AudioSource music;
    private float musicVolume = 0.146f;

    // Start is called before the first frame update
    void Start()
    {
        //music = GetComponent<AudioSource>();
        //music.PlayDelayed(1);
        music.PlayDelayed(1);
    }

    // Update is called once per frame
    void Update()
    {
        music.volume = musicVolume;
    }

    public void updateVolume (float volume)
    {
        musicVolume = volume;
    }
}
