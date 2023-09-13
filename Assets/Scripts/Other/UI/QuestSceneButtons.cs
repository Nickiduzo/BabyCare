using Quest;
using Scene;
using UnityEngine;

namespace UI
{
    public class QuestSceneButtons : HudElement
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private AdsInterstitialconfigurator _ads;
        public SceneType SelectScene;
        private new void Start()
        {
            base.Start();
            Appear();
        }
        public void GoToScene()
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
            _sceneLoader.LoadScene(SelectScene, false);
        }
        private void LoadScene(string s)
        {
            EndCycle();
            _sceneLoader.LoadScene(SelectScene, false);
        }

        private void EndCycle()
        {
            Disappear();
        }
    }
}
