using System.Collections.Generic;
using UnityEngine;

namespace Thimble
{
    public class ThimbleCupSpawn : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawnCupsPoints;
        [SerializeField, Range(0, 10)] private float _horizontalTossSpeed = 5f;
        [SerializeField, Range(0, 10)] private float _verticalTossSpeed = 5f;
        [SerializeField] private ThimbeCupPool _pool;

        public List<ThimbleCup> SpawnCups()
        {
            List<ThimbleCup> temp = new();
            int goldenBallCupNumber = Random.Range(0, spawnCupsPoints.Count);

            for (int i = 0; i < spawnCupsPoints.Count; i++)
            {
                bool withGoldenBall = goldenBallCupNumber == i;

                ThimbleCup cup = SpawnCup
                    (spawnCupsPoints[i].transform.position, withGoldenBall, _horizontalTossSpeed, _verticalTossSpeed);
                temp.Add(cup);
            }

            return temp;
        }

        //Spawn ThimbleCup
        private ThimbleCup SpawnCup(Vector3 at, bool withGoldenBall, float horizontalTossSpeed, float verticalTossSpeed)
        {
            ThimbleCup cup = _pool.PoolCup.GetFreeElement();
            cup.transform.position = at;
            cup.Construct(withGoldenBall, horizontalTossSpeed, verticalTossSpeed);

            return cup;
        }
    }
}