using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewFishing
{
    public class fishSpawn : MonoBehaviour
    {
        [SerializeField] private FishPool _pool;
        [SerializeField] private List<GameObject> points;

        //Spawn all fish first time
        public List<Fish> SpawnFish()
        {
            List<Fish> fishs = new List<Fish>();          
            foreach (GameObject point in points)
            {
                FishConstracror fishcn = _pool.PoolFish.GetFreeElement();
                fishcn.transform.position = point.transform.position + new Vector3(0,0,-3);
                fishcn.Construct();
                fishcn.fish.Construct();
                fishcn.fish.transform.SetParent(point.transform);
                fishs.Add(fishcn.fish);
            }
           
            return fishs;
        }

        //Spawn all fish Again
        public List<Fish> SpawnFishAgain()
        {
            foreach (Transform child in _pool.gameObject.transform)
            {
                Destroy(child.gameObject);
            }
                
            List<Fish> fishs = new List<Fish>();
            foreach (GameObject point in points)
            {
                _pool.SpawnAgain();
                FishConstracror fishcn = _pool.PoolFish.GetFreeElement();
                fishcn.transform.position = point.transform.position + new Vector3(0, 0, -3);
                 fishcn.Construct();
                fishcn.fish.Construct();
                fishcn.fish.transform.SetParent(point.transform);
                fishs.Add(fishcn.fish);
            }

            return fishs;
        }
    }
}
