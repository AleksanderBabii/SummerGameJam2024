using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource audioSource;
    private AudioSource musicSource;
    private AudioSource soundSource;


    private void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //don't destroy Sound manager on the next level
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //destroys duplicate
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        //Assign initial volumes
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
        
    }

    public void PlaySound(AudioClip _sound)
    {
        audioSource.PlayOneShot(_sound);    
    }
    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume,string volumeName,float change,AudioSource source)
    {
        // changing initial value of volume
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1); //Load last saved sound volume from player prefs
        currentVolume += change;

        //Checking if we reached max or min value of volume
        if (currentVolume > 1) { currentVolume = 0; }
        else if (currentVolume < 0) { currentVolume = 1; }

        //assign final value
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //Save final value to player prefs
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
