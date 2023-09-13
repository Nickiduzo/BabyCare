using Quest;
using Scene;
using UnityEngine;

namespace UI
{
    public class MiniGameButton : HudElementMinigames
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private AdsInterstitialconfigurator _ads;
        private void Start() => Appear();

        public SceneType MiniGameScene;   //Scene selection for each minigame
        public void GoToMiniGame()
        {
            Click();
            _ads.StartInterstitialAd();
            _ads.interstitial.OnAdClosed.AddListener(LoadScene);
            _ads.interstitial.OnAdFailedToLoad.AddListener(LoadScene);
            _ads.interstitial.OnAdFailedToShow.AddListener(LoadScene);
            LoadScene();
        }
        private void LoadScene()
        {
            EndCycle();
            _sceneLoader.LoadScene(MiniGameScene, false);
        }
        private void LoadScene(string s)
        {
            EndCycle();
            _sceneLoader.LoadScene(MiniGameScene, false);
        }
        private void EndCycle()
        {
            Disappear();
        }
    }
}
