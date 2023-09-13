using UnityEngine;

namespace Feeding
{
    public class BlobsSpawn : MonoBehaviour
    {

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private BlobsPool _pool;

        //Spawn Blob
        public Blobs SpawnBlobs()
        {
            Blobs blobs = _pool.PoolBlobs.GetFreeElement();
            blobs.transform.position = _spawnPoint.position;
            blobs.Construct();
            return blobs;
        }
    }
}
