using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource soundSource;
    public AudioSource musicSource;

    //weapons and bomb
    public AudioClip pistol;
    public AudioClip machinegun;
    public AudioClip shotgun;
    public AudioClip rocketLauncher;
    public AudioClip flamethrower;
    public AudioClip bomb;
    public AudioClip rocketExplosion;

    //pickup
    public AudioClip ammoPickup;
    public AudioClip pointPickup;
    public AudioClip hexDamagePickup;
    public AudioClip infiniteDashPickup;
    public AudioClip bombPickup;
    public AudioClip shieldPickup;
    public AudioClip infiniteAmmoPickup;
    public AudioClip godmodePickup;

    //powerup end
    public AudioClip hexDamageEnd;
    public AudioClip infiniteDashEnd;
    public AudioClip shieldEnd;
    public AudioClip infiniteAmmoEnd;
    public AudioClip godmodeEnd;

    //dash
    public AudioClip dash;
    public AudioClip dashRecharge;

    //player
    public AudioClip playerDeath;

    //misc
    public AudioClip gameOver;
    public AudioClip nextWave;
    public AudioClip completeLevel;
    public AudioClip button_click;

    //enemy
    public AudioClip enemyDeath;

    //out of ammo
    public AudioClip outOfAmmo;


    //music tracks
    public AudioClip mainMenu;
    public AudioClip level1;
    public AudioClip level2;
    public AudioClip level3;
    //public AudioClip endlessMode;


    public AudioClip[] audioClips;
    public AudioClip[] musicTracks;

    private AudioClip currentTrack;

    private void Awake()
    {
        
        if (instance == null) //jos status olio ei ole olemassa
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("creating audiomanager");
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        

        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioClips = new AudioClip[]
        {
            pistol, //0
            machinegun, //1
            shotgun, //2
            rocketLauncher, //3
            flamethrower, //4
            bomb, //5
            rocketExplosion, //6
            ammoPickup, //7
            pointPickup, //8
            hexDamagePickup, //9
            infiniteDashPickup, //10
            bombPickup, //11
            shieldPickup, //12
            infiniteAmmoPickup, //13
            godmodePickup, //14
            hexDamageEnd, //15
            infiniteDashEnd, //16
            shieldEnd, //17
            infiniteAmmoEnd, //18
            godmodeEnd, //19
            dash, //20
            dashRecharge, //21
            playerDeath, //22
            gameOver, //23
            nextWave, //24
            completeLevel, //25
            button_click, //26
            enemyDeath, //27
            outOfAmmo //28
        };

        musicTracks = new AudioClip[]
        {
            mainMenu,
            level1,
            level2,
            level3,
            //endlessMode
        };
    }

    /*
    public void PlaySound(AudioClip[] clips, int i)
    {
        audioSource.PlayOneShot(clips[i]);
    }
    */

    public void PlaySound(int i)
    {
        soundSource.PlayOneShot(audioClips[i]);
    }

    public void PlaySound(int i, float volume)
    {
        soundSource.PlayOneShot(audioClips[i], volume);
    }

    public void PlayMusic(int i)
    {
        //musicManager.PlayOneShot(musicTracks[i]);
        musicSource.Stop();
        musicSource.clip = musicTracks[i];
        currentTrack = musicTracks[i];
        musicSource.Play();
    }

   
    public void StopMusic()
    {
        musicSource.Stop();
    }
    
}
