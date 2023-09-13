using Inputs;
using Sound;
using UnityEngine;

namespace Bath
{
    public class StickSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private SoundSystem _soundSystem;

        [SerializeField] private StickPool _pool;
        //Main constructor of stick for bath mediator
        public BubbleStick SpawnStick()
        {
            BubbleStick stick = _pool.Pool.GetFreeElement();
            stick.transform.position = _spawnPoint.position;
            stick.Construct(_inputSystem, _soundSystem);
            return stick;
        }
    }
}