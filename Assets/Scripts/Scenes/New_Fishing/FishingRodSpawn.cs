using Inputs;
using System;
using UnityEngine;


namespace NewFishing
{
    public class FishingRodSpawn : MonoBehaviour
    {
        [SerializeField] private FishingRodPool _pool;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] public GameObject spawnFishingRodPoint;

        //Spawn FishingRod
        public FishingRod SpawnFishingRod()
        {
            FishingRod rod = _pool.PoolFishingRod.GetFreeElement();
            rod.transform.position = spawnFishingRodPoint.transform.position;
            rod.Construct(_inputSystem);
            return rod;
        }
    }
}
