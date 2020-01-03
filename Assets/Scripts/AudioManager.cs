using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;

    public AudioClip pistol;
    public AudioClip rocket;

    public AudioClip[] audioClips;

    private void Awake()
    {
        /*
        if (instance == null) //jos status olio ei ole olemassa
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("creating audiomanager");
            instance = this;
        }
        */

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioClips = new AudioClip[]
        {
            pistol,
            rocket
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    public void PlaySound(AudioClip[] clips, int i)
    {
        audioSource.PlayOneShot(clips[i]);
    }
    */

    public void PlaySound(int i)
    {
        audioSource.PlayOneShot(audioClips[i]);
    }
}
