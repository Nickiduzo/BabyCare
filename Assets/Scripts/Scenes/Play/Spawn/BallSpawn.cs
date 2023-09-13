using Inputs;
using System;
using UnityEngine;

namespace PlayScene
{
    public class BallSpawn : MonoBehaviour
    {
        [SerializeField] private BallPool _pool;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private GameObject spawnBallPoint;

        //Spawn Object
        public Ball SpawnBall()
        {
            Ball spawnobject = _pool.PoolBall.GetFreeElement();
            spawnobject.transform.position = spawnBallPoint.transform.position;
            spawnobject.Construct(_inputSystem);
            return spawnobject;
        }
    }
}
