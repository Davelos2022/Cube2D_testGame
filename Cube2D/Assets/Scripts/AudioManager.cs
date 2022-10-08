using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip Win;
    [SerializeField] private AudioClip Lose;
    [SerializeField] private Slider volumeSlider;

    public enum SoundsGame { Win, Lose };
    private AudioSource audioSource;
    public Slider VolumeSider => volumeSlider;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = volumeSlider.value;
        volumeSlider.onValueChanged.AddListener(delegate { VolumeValue(); });
    }

    public void PlaySound(SoundsGame typeSound)
    {
        switch (typeSound)
        {
            case SoundsGame.Win:
                audioSource.PlayOneShot(Win);
                break;
            case SoundsGame.Lose:
                audioSource.PlayOneShot(Lose);
                break;
            default:
                break;
        }
    }

    private void VolumeValue()
    {
        audioSource.volume = volumeSlider.value;
    }
}
