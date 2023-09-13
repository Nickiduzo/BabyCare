using System.Collections.Generic;
using Sound;
using UI;
using UnityEngine;

namespace Thimble
{
    public class MagicianSpawn : MonoBehaviour
    {
        [SerializeField] private MagicianPool _pool;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Timer _timer;
        [SerializeField] private ScoreCount _score;
        [SerializeField] public FxSystem _fxsystem;
        [SerializeField] public SoundSystem _soundSystem;

        //Spawn Magician
        public Magician SpawnMagician(IReadOnlyList<ThimbleCup> cups)
        {
            Magician magician = _pool.PoolMagician.GetFreeElement();
            magician.transform.position = _spawnPoint.position;
            magician.Construct(_timer, _score, _fxsystem, _soundSystem, cups);
            return magician;
        }
    }
}
