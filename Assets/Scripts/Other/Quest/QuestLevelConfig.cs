using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(fileName = "QuestLevel", menuName = "Configs/QuestLevel")]
    public class QuestLevelConfig : ScriptableObject
    {
        public SceneType sceneType;
        public int animalType;
    }
}