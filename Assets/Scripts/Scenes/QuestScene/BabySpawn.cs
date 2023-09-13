using UnityEngine;


namespace QuestBaby
{
    public class BabySpawn : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private BabyPool _pool;

        //Spawn Baby
        public Baby SpawnBaby()
        {
            Baby baby = _pool.PoolBaby.GetFreeElement();
            baby.gameObject.transform.position = _spawnPoint.position;
            return baby;
        }
    }
}
