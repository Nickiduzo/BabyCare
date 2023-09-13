using Inputs;
using System;
using UnityEngine;

namespace PlayScene
{
    public class ConsoleSpawn : MonoBehaviour
    {
        [SerializeField] private ConsolePool _pool;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private GameObject spawnConsolePoint;

        //Spawn Object
        public Console SpawnConsole()
        {
            Console spawnobject = _pool.PoolConsole.GetFreeElement();
            spawnobject.transform.position = spawnConsolePoint.transform.position;
            spawnobject.Construct(_inputSystem);
            return spawnobject;
        }
    }
}
