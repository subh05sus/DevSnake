using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip startMusic;
    public Button SoundOnButton; // Reference to the button to disable
    public Button SoundOffButton;  // Reference to the button to enable
    public AudioClip foodEatingSound; // Add this field

    private void Start()
    {
        PlayStartMusic();
        SoundOffButton.gameObject.SetActive(false);
    }

    public void PlayStartMusic()
    {
        musicSource.clip = startMusic;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SoundOn()
    {
        SoundOnButton.gameObject.SetActive(true);
        SoundOffButton.gameObject.SetActive(false);
        PlayStartMusic();
    }

    public void SoundOff()
    {
        SoundOnButton.gameObject.SetActive(false);
        SoundOffButton.gameObject.SetActive(true);
        StopMusic();
    }
        public void PlayFoodEatingSound() // Add this method
    {
        musicSource.PlayOneShot(foodEatingSound);
    }
}
