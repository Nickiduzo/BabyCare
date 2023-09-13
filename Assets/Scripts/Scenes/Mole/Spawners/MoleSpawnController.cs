using Mole.Config;
using Sound;
using System;
using System.Collections.Generic;
using System.Collections;
using UI;
using UnityEngine;
using UsefulComponents;
using UnityEngine.Events;

namespace Mole.Spawners
{
    public class MoleSpawnController : MonoBehaviour
    {
        [SerializeField] private CarrotLevelConfig _config;
        [SerializeField] private MoleSpawner _holeableSpawner;
        [SerializeField] ParticleView _timeParticle;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private float _timeToWait = 2f;
        [SerializeField] private int _baloonToIncrease = 2;
        [SerializeField] private int _timeToIncrease = 10;
        [SerializeField] private int _timeToDecrease = 1;

        public UnityEvent<ICollectable> OnMoleCaught { get; private set; } = new();
        public UnityEvent<int> OnBaloonCaught { get; private set; } = new();
        public UnityEvent<int> OnBombCaught { get; private set; } = new();

        public int CurrentBaloonCount => _wholeBaloonCatch;
        public int CurrentMoleCount => _wholeMoleCatch;

        private int _holeableToSpawn;
        private int _wholeMoleCatch;
        private int _wholeBaloonCatch;
        private List<BaseHoleable> _holeables = new();

        private bool _isBaloonCatchInProgress = false;
        private bool _isMoleCatchInProgress = false;
        private bool _isBombCatchInProgress = false;

        // spawn first mole
        public void StartMoleSpawning()
        {
            _holeableToSpawn = 1;
            StartCoroutine(SpawnMole(true));
        }

        // hide all moles and spawn new one, subscribe Actions [OnDied] and [OnHide], add to [_moles] List 
        private IEnumerator SpawnMole(bool isFirstSpawn = false)
        {
            while(true)
            {

                _soundSystem.PlaySound("cameIn");
                BaseHoleable spawnedMole = _holeableSpawner.SpawnMole();
                _holeables.Add(spawnedMole);
                RegisterMole(spawnedMole);

                if (isFirstSpawn)
                {
                    ShowStaticPointer(_holeables[0].transform.position);
                    _holeables[0].OnDied.AddListener(HideHint);
                    _holeables[0].OnHied.AddListener(HideHint);

                    isFirstSpawn = false;
                }

                yield return new WaitForSeconds(_timeToWait);

                yield return null;
            }
        }

        //check for the amount of bombs
        private int BombsAmount()
        {
            int amount = 0;
            foreach (BaseHoleable bh in _holeables)
            {
                if(bh is Bomb bomb) {
                    amount++;
                }
            }
            return amount;
        }

        // disable hint, unsubscribe Actions [OnDied] and [OnHide]
        private void HideHint(BaseHoleable holeable)
        {
            HidePointerSystem();
            holeable.OnDied.RemoveListener(HideHint);
            holeable.OnHied.RemoveListener(HideHint);
        }

        // invoke [Appear] in mole, subscribe Actions [OnDied], [OnHide] and [OnCatch]
        private void RegisterMole(BaseHoleable holeable)
        {
            holeable.Appear();

            if (holeable is Mole mole)
                mole.OnCatch.AddListener(() => MoleCatch(mole));
            if (holeable is Baloon baloon)
                baloon.OnCatch.AddListener(() => BaloonCatch(baloon));
            if (holeable is Bomb bomb)
            {
                bomb.OnCatch.AddListener(() => BombCatch(bomb));
                _soundSystem.PlaySound("BombWick");
            }

            holeable.OnDied.AddListener(HoleableDie);
            holeable.OnHied.AddListener(HoleableHied);
            holeable.IsRegistered = true;
        }

        // remove certain mole from [_moles] List, unsubscribe Actions [OnDied], [OnHide] and [OnCatch]
        private void RemoveMole(BaseHoleable holeable)
        {
            if (holeable is Mole mole)
                mole.OnCatch.RemoveListener(() => MoleCatch(mole));
            if (holeable is Baloon baloon)
                baloon.OnCatch.RemoveListener(() => BaloonCatch(baloon));
            if (holeable is Bomb bomb)
            {
                bomb.OnCatch.RemoveListener(() => BombCatch(bomb));
                if (BombsAmount() < 2)
                {
                    _soundSystem.StopSound("BombWick");
                }
            }

            holeable.OnDied.RemoveListener(HoleableDie);
            holeable.OnHied.RemoveListener(HoleableHied);
            holeable.IsRegistered = false;
            _holeables.Remove(holeable);
        }

        // add plus one caught mole, invoke Action [OnProgressChanged]
        private void MoleCatch(Mole mole)
        {
            if(!_isMoleCatchInProgress)
                StartCoroutine(CallMoleCatchWithDelay(mole));
        }
        private void BaloonCatch(Baloon baloon)
        {
            if(!_isBaloonCatchInProgress)
                StartCoroutine(CallBaloonCatchWithDelay(baloon));
        }
        private void BombCatch(Bomb bomb)
        {
            if (!_isBombCatchInProgress)
            {
                if (BombsAmount() < 2)
                {
                    _soundSystem.StopSound("BombWick");
                }
                StartCoroutine(CallBombCatchWithDelay(bomb));
            }
            }

            private IEnumerator CallBombCatchWithDelay(Bomb bomb)
        {
            _isBombCatchInProgress = true;

            yield return null;
            OnBombCaught?.Invoke(_timeToDecrease);

            _isBombCatchInProgress = false;
        }
        private IEnumerator CallMoleCatchWithDelay(Mole mole)
        {
            _isMoleCatchInProgress = true;

            _wholeMoleCatch++;
            yield return null;
            OnMoleCaught?.Invoke(mole);

            _isMoleCatchInProgress = false;
        }
        private IEnumerator CallBaloonCatchWithDelay(Baloon baloon)
        {
            _isBaloonCatchInProgress = true;

            _wholeBaloonCatch++;

            // Check if the counter is a multiple of _baloonToIncrease
            if (_wholeBaloonCatch % _baloonToIncrease == 0)
            {
                Instantiate(_timeParticle, baloon.transform.position, _timeParticle.transform.rotation);
                _soundSystem.PlaySound("ExtraTime");

                OnBaloonCaught?.Invoke(_timeToIncrease);
            }
            yield return null;

            _isBaloonCatchInProgress = false;
        }

        // remove mole from List and spawn new one
        private void HoleableDie(BaseHoleable holeable)
        {
            int flag = holeable.SpawnPointIndex;
            RemoveMole(holeable);
            //CalculateSpawnNumber();
            SpawnMole();
            _holeableSpawner.ResetFlag(flag);
        }

        // hide all moles from [_moles] List 
        private void HideAllMoles()
        {
            foreach (var mole in _holeables)
            {
                //_holeables[0].Hide();
                mole.Hide();
            }
        }

        // set "flag" for mole spawn point and spwan mole
        private void HoleableHied(BaseHoleable holeable)
        {
            _holeableToSpawn = 1;
            int flag = holeable.SpawnPointIndex;
            RemoveMole(holeable);

            SpawnMole();
            _holeableSpawner.ResetFlag(flag);
        }

        // check if all moles are caught, if so, return true
        private bool CatchAllMoles() => _wholeMoleCatch >= _config.CatchMoleToWin;

        // set how many moles need to spawn
        private void CalculateSpawnNumber()
            => _holeableToSpawn = _holeables.Count + 2 <= _config.MaxMoleToSpawn ? 2 : 0;
        
        // appear hint on firs mole
        private void ShowStaticPointer(Vector3 position)
            => HintSystem.Instance.ShowPointerHint(position + Vector3.up);

        // disable hint
        private void HidePointerSystem()
            => HintSystem.Instance.HidePointerHint();
    }
}