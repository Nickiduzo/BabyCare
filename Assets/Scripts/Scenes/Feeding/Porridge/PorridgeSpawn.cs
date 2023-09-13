using UnityEngine;

namespace Feeding
{
    public class PorridgeSpawn : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private PorridgePool _pool;

        //Spawn Porridge
        public Porridge SpawnPorridge()
        {
            Porridge porridge = _pool.PoolPorridge.GetFreeElement();
            porridge.transform.position = _spawnPoint.position;
            porridge.Construct();
            return porridge;
        }
    }
}
