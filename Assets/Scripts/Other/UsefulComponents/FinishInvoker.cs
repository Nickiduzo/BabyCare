using Quest;
using Scene;
using UnityEngine;

public class FinishInvoker : MonoBehaviour
{   
    [SerializeField] private SceneLoader _sceneLoader;

    public void LoadFinishScene()
        => _sceneLoader.LoadScene(SceneType.Quest);
}