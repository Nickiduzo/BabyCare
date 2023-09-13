using Quest;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scene
{
    [CreateAssetMenu(fileName = "SceneInfo", menuName = "Scene")]
    public class SceneInfo : ScriptableObject
    {
        [SerializeField] private string _sceneKey;
        [FormerlySerializedAs("_taskType")][SerializeField] private SceneType sceneType;

        public SceneType Type => sceneType;
        public string Key => _sceneKey;
    }
}