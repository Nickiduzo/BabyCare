using Quest;
using Scene;
using UnityEngine;

namespace UI
{
    public class ExitButton : HudElement
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private AdsInterstitialconfigurator _ads;

        private new void Start()
        {
            base.Start();
            Appear();
        }

        public void GoToQuest()
        {
            Click();
            LoadSceneQuest();
            _ads.StartInterstitialAd();
            _ads.interstitial.OnAdClosed.AddListener(LoadSceneQuest);
            _ads.interstitial.OnAdFailedToLoad.AddListener(LoadSceneQuest);
            _ads.interstitial.OnAdFailedToShow.AddListener(LoadSceneQuest);
            LoadSceneQuest();
        }
        public void GoToMain()
        {
            Click();
            LoadSceneMain();
            _ads.StartInterstitialAd();
            _ads.interstitial.OnAdClosed.AddListener(LoadSceneMain);
            _ads.interstitial.OnAdFailedToLoad.AddListener(LoadSceneMain);
            _ads.interstitial.OnAdFailedToShow.AddListener(LoadSceneMain);
            LoadSceneMain();
        }

        public void GoToMiniGames()
        {
            Click();
            LoadSceneMiniGames();
            _ads.StartInterstitialAd();
            _ads.interstitial.OnAdClosed.AddListener(LoadSceneMiniGames);
            _ads.interstitial.OnAdFailedToLoad.AddListener(LoadSceneMiniGames);
            _ads.interstitial.OnAdFailedToShow.AddListener(LoadSceneMiniGames);
            LoadSceneMiniGames();
        }



        public void ExitGame()
        {
            Debug.Log("Quit");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }

        private void EndCycle()
        {
            Disappear();
        }



        private void LoadSceneMain()
        {
            EndCycle();
            _sceneLoader.LoadScene(SceneType.Main);
        }
        private void LoadSceneMain(string s)
        {
            EndCycle();
            _sceneLoader.LoadScene(SceneType.Main);
        }
        private void LoadSceneQuest()
        {
            EndCycle();
            _sceneLoader.LoadScene(SceneType.Quest, false);
        }
        private void LoadSceneQuest(string s)
        {
            EndCycle();
            _sceneLoader.LoadScene(SceneType.Quest, false);
        }
        private void LoadSceneMiniGames()
        {
            EndCycle();
            _sceneLoader.LoadScene(SceneType.MiniGames);
        }
        private void LoadSceneMiniGames(string s)
        {
            EndCycle();
            _sceneLoader.LoadScene(SceneType.MiniGames);
        }

        // Метод відповідає за кнопку "назад" у сцені MiniGamesScene, в залежності від попередньої сцени
        public void LoadQuestOrPlay()
        {
            string previousScene = PlayerPrefs.GetString("previousScene");

            if (previousScene == "PlayScene")
            {
                _sceneLoader.LoadScene(SceneType.Play);
            }
            else LoadSceneQuest();
        }
    }
}

    


