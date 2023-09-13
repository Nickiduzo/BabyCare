using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewFishing
{
    public class FishSIdeSpawn : MonoBehaviour
    {
        [SerializeField] private FishSidePool _pool;

        //Spawn FishSide
        public FishSide SpawnFishSide(Vector3 at)
        {
            FishSide rod = _pool.PoolFishSide.GetFreeElement();
            rod.transform.position = at;
            rod.Construct();
            return rod;
        }
    }
}
