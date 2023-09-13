using Sound;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mole.Spawners
{
    public class MoleSpawner : MonoBehaviour
    {
        [SerializeField] private float _moleScale;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private FxSystem _fxSystem;
        [SerializeField] private List<Vector3> _moleSpawnPoints;
        [SerializeField] private List<CarrotHole> _carrotHoles;
        [SerializeField] private CarrotHolesContainer _carrotHolesCointainer;
        [SerializeField] private HoleablePoolManager _pool;
        [SerializeField] private bool[] _spawnPointUsedFlags;
        private int wasP=-1;

        // set holes in List [_carrotHoles] from holes container
        private void Awake()
        {
            _carrotHoles = _carrotHolesCointainer.HolesOnScene;
            foreach (var hole in _carrotHoles)
            {
                SetMoleSpawnPosition(hole.MoleSpawnPosition);
            }
        }

        // set mole spawn point based on hole position, add current spawn point to [_moleSpawnPoints] List and set flag to this point
        public void SetMoleSpawnPosition(Vector3 position)
        {
            _moleSpawnPoints.Add(position);
            _spawnPointUsedFlags = new bool[_moleSpawnPoints.Count];
        }

        // remove all spawn points from [_moleSpawnPoints] List, set "null" for [_spawnPointUsedFlags]
        public void Clear()
        {
            _moleSpawnPoints.Clear();
            _spawnPointUsedFlags = null;
        }

        // searching spawn point with "flag" set this point as spawnPointIndex,
        // launch spawn moles set Construct for them, enable sprite mask where will spawn mole 
        public BaseHoleable SpawnMole()
        {
            int spawnPointIndex = Random.Range(0, 6);//_spawnPointUsedFlags.ToList().FindIndex(flag => flag == false);
            while (wasP == spawnPointIndex)
            {
                spawnPointIndex = Random.Range(0, 6);

            }
            wasP = spawnPointIndex;
            if (spawnPointIndex == -1)
                return null;

            Vector3 position = _moleSpawnPoints[spawnPointIndex];
            BaseHoleable spawnedMole = SpawnMole(position, spawnPointIndex);
            _spawnPointUsedFlags[spawnPointIndex] = true;

            spawnedMole.Construct(_soundSystem, _fxSystem);
            BaseHoleable mole = spawnedMole;
            _carrotHoles[spawnPointIndex].ActivateSpriteMask(true);
            return mole;
        }

        // get Mole from pool, set position and invoke [SetSpawnPointIndexAndScale] in "spawnedMole"
        private BaseHoleable SpawnMole(Vector3 position, int spawnPointIndex)
        {
            BaseHoleable spawnedMole = _pool.HoleablePools[Random.Range(0, _pool.HoleablePools.Count)].GetFreeElement();
            spawnedMole.transform.position = position;
            spawnedMole.SetSpawnPointIndexAndScale(spawnPointIndex, _moleScale);
            return spawnedMole;
        }

        // choose spawn point and hole based on [index], disable "flag" and sprite mask
        public void ResetFlag(int index)
        {
            _spawnPointUsedFlags[index] = false;
            _carrotHoles[index].ActivateSpriteMask(false);
        }
    }
}