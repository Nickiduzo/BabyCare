using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quest;

public class ConfigChanger : MonoBehaviour
{
    [SerializeField] private QuestLevelConfig _config;
    private Dictionary<string, SceneType> _sceneCodeDictionary;
    private string _sceneName;


    // set new scene type in the config depending on scene name
    private void Awake()
    {
        _sceneCodeDictionary = new Dictionary<string, SceneType>()
        {
            { "SheepScene", SceneType.Sheep },
            { "CowScene", SceneType.Cow },
            { "CarrotScene", SceneType.Carrot },
            { "Apple", SceneType.Apple },
            { "Bee-garden", SceneType.Bee },
            { "FishingScene", SceneType.Fishing },
            { "SunflowerScene", SceneType.Sunflower },
            { "TomatoeScene", SceneType.Tomato },
            { "ThimbleScene", SceneType.Thimble },
            { "Сhickenscene", SceneType.Chicken },
            { "FeedingScene", SceneType.Feeding },
        };
    }

    private void Start()
        => GetSceneName();
    
    private void GetSceneName()
    {
        _sceneName = SceneManager.GetActiveScene().name;
        SelectorForCorrectScene();
    }

    private void SelectorForCorrectScene()
    {
        if (_sceneCodeDictionary.ContainsKey(_sceneName))
        {
            SceneType sceneType = _sceneCodeDictionary[_sceneName];
            _config.sceneType = sceneType;
            Debug.Log("Config scene type: " + _config.sceneType);
            Debug.Log("_config.animalType " + _config.animalType);
        }
        else
        {
            Debug.Log("This config was not found " + _sceneName);
        }
    }
}