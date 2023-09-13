using Quest;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneInfo[] _scenesInfo;
        [SerializeField] private FadeScreenPanel _fadeScreenPanel;

        public static bool IsLevelComplied = true;

        private const string previousScene = "previousScene";
        public void LoadScene(SceneType type, bool isLevelComplied = true, float delay = 1f)
        {
            IsLevelComplied = isLevelComplied;
            SceneInfo loadSceneInfo = GetScene(type);

            if (loadSceneInfo == null)
                return;

            if (loadSceneInfo.Key == CurrentScene())
                return;
            
            PlayerPrefs.SetString(previousScene, CurrentScene());
            StartCoroutine(LoadScene(loadSceneInfo, delay));
        }

        public void ReloadCurrentScene(bool isLevelComplied = true, float delay = 1f)
        {
            StartCoroutine(ReloadCurrentSceneStart(isLevelComplied , delay));
        }

        private IEnumerator ReloadCurrentSceneStart(bool isLevelComplied = true, float delay = 1f)
        {
            IsLevelComplied = isLevelComplied;
            _fadeScreenPanel.FadeIn();
            yield return new WaitForSeconds(delay);
            SceneManager.LoadSceneAsync (CurrentScene());
        }

        private SceneInfo GetScene(SceneType type)
            => _scenesInfo.FirstOrDefault(sceneInfo => sceneInfo.Type == type);

        private string CurrentScene()
            => SceneManager.GetActiveScene().name;

        private IEnumerator LoadScene(SceneInfo loadSceneInfo, float delay)
        {
            _fadeScreenPanel.FadeIn();
            yield return new WaitForSeconds(delay);
            SceneManager.LoadSceneAsync(loadSceneInfo.Key);
        }
    }
}