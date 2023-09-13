using CAS.AdObject;
using UnityEngine;

public class AdsInterstitialconfigurator : MonoBehaviour
{
    public InterstitialAdObject interstitial;
    [SerializeField] private ToggleSwither toggle;

    void Start()
    {
        interstitial = gameObject.GetComponent<InterstitialAdObject>();
        //interstitial.OnAdClosed.AddListener(toggle.SoundOn);
        interstitial.OnAdClosed.AddListener(ResumeTheGame);
    }

    //show Interstitial Ad
    public void StartInterstitialAd()
    {
        interstitial.LoadAd();
        if (interstitial.isAdReady)
        {
            //toggle.SoundOff();
            PauseTheGame();
        }
        interstitial.Present();
    }

    //pause the game during ad
    private void PauseTheGame()
    {
        toggle.SoundOff();
        Time.timeScale = 0;
    }

    //resume the game after ad
    private void ResumeTheGame()
    {
        toggle.SoundOn();
        Time.timeScale = 1;
    }
}
