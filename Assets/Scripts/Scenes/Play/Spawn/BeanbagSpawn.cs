using Inputs;
using System;
using UnityEngine;


namespace PlayScene
{
    public class BeanbagSpawn : MonoBehaviour
    {
        [SerializeField] private BeanbagPool _pool;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private GameObject spawnBeanbagPoint;

        //Spawn Object
        public Beanbag SpawnBeanbag()
        {
            Beanbag spawnobject = _pool.PoolBeanbag.GetFreeElement();
            spawnobject.transform.position = spawnBeanbagPoint.transform.position;
            spawnobject.Construct(_inputSystem);
            return spawnobject;
        }
    }
}
