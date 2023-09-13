using UnityEngine;
using Sound;

public class ToggleSwither : MonoBehaviour
{
    [SerializeField] private SoundSystem _soundSystem;

    private const string MUSIC_KEY = "isMusicOn";
    private const string SOUND_KEY = "isSoundOn";

    public void SoundOff()
    {
        PlayerPrefs.SetInt(MUSIC_KEY, 0);
        PlayerPrefs.SetInt(SOUND_KEY, 0);
        PlayerPrefs.Save();
        ResetMusic();
    }
    public void SoundOn()
    {
        PlayerPrefs.SetInt(MUSIC_KEY, 1);
        PlayerPrefs.SetInt(SOUND_KEY, 1);
        PlayerPrefs.Save();
        ResetMusic();
    }

    private void ResetMusic()
    {
        _soundSystem.InitSetting();
    }
}
