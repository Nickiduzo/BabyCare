using Inputs;
using System;
using UnityEngine;

namespace ShootingGallery
{
    public class BallShootSpawn : MonoBehaviour
    {
        [SerializeField] private BallShootPool _pool;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] public GameObject _points;

        //Spawn BallShoot
        public BallShoot SpawnBall()
        {
            BallShoot ball = _pool.PoolBallShoot.GetFreeElement();
            ball.transform.position = _points.transform.position;
            ball.spawmPos = _points.transform.position;
            ball.Construct(_inputSystem);
     
            return ball;
        }
    }
}
