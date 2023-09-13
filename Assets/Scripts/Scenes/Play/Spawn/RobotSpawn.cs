using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene
{
    public class RobotSpawn : MonoBehaviour
    {
        [SerializeField] private RobotPool _pool;
        [SerializeField] private GameObject spawnRobotPoint;

        //Spawn Object
        public Robot SpawnRobot()
        {
            Robot spawnobject = _pool.PoolRobot.GetFreeElement();
            spawnobject.transform.position = spawnRobotPoint.transform.position;
            spawnobject.Construct();
            return spawnobject;
        }
    }
}
