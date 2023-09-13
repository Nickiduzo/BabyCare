using Scene;
using Sound;
using UnityEngine;

namespace Quest
{
    public class TaskSpawner : MonoBehaviour
    {
        private const string PreviousTaskId = "PreviousTaskId";

        [SerializeField] private Transform _taskSpawnPoint;
        [SerializeField] private SaveLoadSystem _saveLoad;
        [SerializeField] private Task[] _tasks;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private SoundSystem _soundSystem;

        private int _previousTaskId;

        // uses for spawn rendom task
        public Task SpawnRandomTask()
        {
            _previousTaskId = GetPreviousTaskId();
            int taskId = GetNextTaskId();
            SetPreviousTaskId(taskId);
            return SpawnTask(taskId);
        }

        // create task based on ID
        public Task SpawnTask(int taskID)
        {
            Task spawnedTask = Instantiate(_tasks[taskID], _taskSpawnPoint.position, _tasks[taskID].transform.rotation);
            spawnedTask.Construct(_soundSystem, _sceneLoader, _saveLoad);
            return spawnedTask;
        }

        // move to next task ID
        private int GetNextTaskId()
        {
            return _previousTaskId >= _tasks.Length-1 ? 0 : _previousTaskId + 1;
        }
        
        // save previous task ID in prefab
        private void SetPreviousTaskId(int id)
            => PlayerPrefs.SetInt(PreviousTaskId, id);

        // take task ID from prefab
        private int GetPreviousTaskId()
            => PlayerPrefs.GetInt(PreviousTaskId);
    }
}