using UnityEngine;

namespace Quest
{
    public class SaveLoadSystem : MonoBehaviour
    {
        private const string TaskKey = "Task";

        public void Save(Task task)
        {
            PlayerPrefs.SetInt(TaskKey, (int)task.Type);
        }
    }
}