using Scene;
using Sound;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using DG.Tweening;
using UI;
using Timer = UI.Timer;

namespace NewFishing
{
    public class NewFishingMediator : MonoBehaviour
    {
        [SerializeField] private FishingRodSpawn _SpawnFishingRod; 

        [SerializeField] private fishSpawn _SpawnFish;

        [SerializeField] private BucketSpawn _SpawnBucket;
        
        [SerializeField] private FishSIdeSpawn _SpawnFishSide;
        [SerializeField] public List<GameObject> spawnFishSidePoints;

        [SerializeField] public GameObject Holes;

        [SerializeField] public FxSystem _fxsystem;
        [SerializeField] public InputSystem _input;
        [SerializeField] public SoundSystem _soundSystem;
        [SerializeField] private string _holesRotateSound;
        [SerializeField] public SceneLoader _sceneLoader;
        [SerializeField] private WinPannel _WinPannel;

        [SerializeField] private Timer _timer;
        [SerializeField] private ScoreCount _score;

        private Bucket _bucket { get; set; }
        private Hook _hook { get; set; }
        private FishingRod _rod { get; set; }

        public int fishCount = 7;
        private int fishLeftCount = 7;
        private int score = 0;
        private bool holeRotateFlag = false;
        private float holeRotateSpeed = 0;

        //Spawn Fishing Rod
        private FishingRod SpawnFishingRod()
        {
            FishingRod rod = _SpawnFishingRod.SpawnFishingRod();
            _hook = rod._hook;
            _rod = rod;
            return rod;
        }
        //Spawn Bucket
        private Bucket SpawnBucket()
        {
            Bucket bucket = _SpawnBucket.SpawnBucket();
            bucket.fishSide = SpawnFishSide();
            bucket.FishInBucket += FisnInBucket;
            bucket.fishCount = 0;
            _bucket = bucket;
            return bucket;
        }

        //Spawn Fish in holes first time 
        private List<Fish> SpawnFish()
        {
            List<Fish> fish = _SpawnFish.SpawnFish();
            foreach (Fish f in fish)
            {
                f._hook = _hook;
                f.FishOnHook += moveFishingRodtoBucket;
            }

            return fish;
        }

        //Spawn Fish in holes
        private List<Fish> SpawnFishAgain()
        {
            List<Fish> fish = _SpawnFish.SpawnFishAgain();
            foreach (Fish f in fish)
            {
                f._hook = _hook;
                f.FishOnHook += moveFishingRodtoBucket;
            }

            return fish;
        }
        //Spawn Fish in Bucket
        private List<FishSide> SpawnFishSide()
        {
            List<FishSide> fish = new List<FishSide>();
            foreach (GameObject p in spawnFishSidePoints)
            {
                FishSide f = _SpawnFishSide.SpawnFishSide(p.transform.position);
                fish.Add(f);
            }

            return fish;
        }

        //when fisn enter Bucket
        private void FisnInBucket()
        {
            score++;
            _bucket.fishCount++;
            _score.AddScore(1);
            fishLeftCount--;
            playSuccess();
            if (fishLeftCount == 0)
            {
                fishLeftCount = fishCount;
                _bucket.ClearFishSide();
                nextTour();
            }
        }

        //when all fisn enter Bucket
        private void nextTour()
        {
            playSuccessTimerUp();
            SpawnFishAgain();
            int temp = 1;
            int speedamp = score / 7;
            if (Random.Range(-15, 10) < 0)
            {
                temp = -1;
            }
            _soundSystem.PlaySound(_holesRotateSound);
            holeRotateSpeed = Random.Range(1 + speedamp, 3 + speedamp) * temp;
            holeRotateFlag = true;
        }


        //rotate holes
        private void rotateHoles()
        {
            if (holeRotateFlag)
            {
                Holes.transform.DOLocalRotate(new Vector3(0, 0, holeRotateSpeed), 1, RotateMode.LocalAxisAdd);
            }
        }

        //move Fishing Rod to Bucket
        private void moveFishingRodtoBucket()
        {           
            _rod.StopMoveToDestination();
            Sequence move = DOTween.Sequence();
            move.Append(_rod.rod.transform.DOMove(_bucket.transform.position + new Vector3(2, -1, 0), 1f));
            move.AppendCallback(rodInteractivity);
            move.Play();
        }

       

        //after the rod has moved to the bucket
        private void rodInteractivity()
        {
            if (!_rod.fishingRodDragFlag)
            {
                _rod.StartMoveToDestination();
                _rod.MoveToDestination();
            }
            else
            {
                _rod.StartMoveToDestination();
                _rod.StartSlowDragMove();
            }
        }

        //play Success and TimerUp particle and sound 
        public void playSuccessTimerUp()
        {
            _timer.AddTime(10);
            _fxsystem.PlayEffect("PlusTime", _bucket.gameObject.transform.position);
            _fxsystem.PlayEffect("Success", _bucket.gameObject.transform.position);
            _soundSystem.PlaySound("Success");
        }

        //play Success particle and sound 
        public void playSuccess()
        {
            _fxsystem.PlayEffect("Success", _bucket.gameObject.transform.position);
            _fxsystem.PlayEffect("Success", _bucket.gameObject.transform.position);
            _soundSystem.PlaySound("Success");
        }

        //when time end
        public void LvlEnd()
        {
            _rod.StopAllActions();
            _WinPannel.Appear();
        }

        // Start is called before the first frame update
        void Start()
        {
            _timer.OnTimeEnd += LvlEnd;
            fishLeftCount = fishCount;
            SpawnFishingRod();
            SpawnBucket();

            SpawnFish();
        }

        // Update is called once per frame
        void Update()
        {
            rotateHoles();
        }
    }
}