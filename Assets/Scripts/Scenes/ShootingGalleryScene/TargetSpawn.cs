using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGallery
{
    public class TargetSpawn : MonoBehaviour
    {
        [SerializeField] private TargetsPool _pool;

        //spawn targets
        public Targets SpawnTargets(Vector3 at)
        {
            Targets target = _pool.PoolTargets.GetFreeElement();
            target.transform.position = at;
            return target;
        }
    }
}
