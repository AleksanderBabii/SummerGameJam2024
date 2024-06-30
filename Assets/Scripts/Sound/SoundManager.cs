using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();

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
        
    }

    public void PlaySound(AudioClip _sound)
    {
        audioSource.PlayOneShot(_sound);    
    }
}
