using Inputs;
using Quest;
using Scene;
using Sound;
using System.Collections.Generic;
using UnityEngine;
using UsefulComponents;

namespace Feeding
{
    public class FeedingSceneMediator : MonoBehaviour
    {
        [SerializeField] private FeedingSceneController _FeedingSceneController;
        [SerializeField] private BottleSpawn _SpawnBottle;
        [SerializeField] public GameObject spawnBottlePoint;
        [SerializeField] private PorridgeSpawn _SpawnPorridge;
        [SerializeField] private CookiesSpawn _SpawnCookie;
        [SerializeField] public List<GameObject> spawnCookiesPoints;
        [SerializeField] private AppleSpawn _SpawnApple;      
        [SerializeField] private SpoonSpawn _SpawnSpoon;
        [SerializeField] private EatZoneSpawn _SpawnEatZone;
        [SerializeField] private BabyfeedingSpawn _SpawnBaby;
        [SerializeField] public Animator _babyAnimation;
        [SerializeField] private NapkinSpawn _SpawnNapkin;
        [SerializeField] public GameObject spawnPointNapkin;

        [SerializeField] public FxSystem _fxsystem;
        [SerializeField] public SoundSystem _soundSystem;
        [SerializeField] public SceneLoader _sceneLoader;
        [SerializeField] private InputSystem _inputSystem;



        private EatZone eatZone;
        private BabyFeeding _baby;
        public Napkin Napkin { get; set; }

        private Bottle SpawnBottle(Vector3 position)
        {
            Bottle bottle = _SpawnBottle.SpawnBottle(position);

            bottle._dragAndDrop.OnDragStart += FoodOnDragStart;
            bottle._dragAndDrop.OnDragEnded += FoodStopDragging;
            bottle._dragAndDrop.OnDrag += FoodOnDrag;
            ActivateHint(spawnBottlePoint.transform.position, spawnBottlePoint.transform.position);
            return bottle;

        }

        //when the player drag food
        private void FoodOnDrag()
        {
            eatZone.idletime = 0;
            if (!_baby.eatAnimationPlay && !_babyAnimation.GetBool("Drag"))
            {
                _babyAnimation.SetBool("Drag", true);
                
            }
        }

        //when the player stop drag food
        private void FoodStopDragging()
        {
            _babyAnimation.SetBool("Drag", false);
        }

        //when the player start drag food
        private void FoodOnDragStart()
        {
            if (!_baby.eatAnimationPlay)
            {
                _babyAnimation.SetBool("Drag", true);
                _soundSystem.PlaySound("popping");
            }
        }

        // spawn zone responsible for eating
        private void SpawnEatZone()
        {
            EatZone zone = _SpawnEatZone.SpawnEatZone();
            eatZone = zone;
            zone.Fxsystem = _fxsystem;
            zone.SoundSystem = _soundSystem;
            zone.mediator = this;
            zone.Porridge = SpawnPorridge();
            zone.Apple = SpawnApple();
            BabyFeeding baby = SpawnBaby();
            zone.babyAnimation = baby.babyAnimator;
            zone.Baby = baby;
            _babyAnimation = baby.babyAnimator;
            zone.Blobs = baby.blobs;

        }
        private Porridge SpawnPorridge()
        {
            Porridge porridge = _SpawnPorridge.SpawnPorridge();

            return porridge;

        }
        private Apple SpawnApple()
        {
            Apple apple = _SpawnApple.SpawnApple();
            apple._dragAndDrop.OnDragStart += FoodOnDragStart;
            apple._dragAndDrop.OnDragEnded += FoodStopDragging;
            apple._dragAndDrop.OnDrag += FoodOnDrag;
            return apple;

        }

        private BabyFeeding SpawnBaby()
        {
            BabyFeeding baby = _SpawnBaby.SpawnBaby();
            baby.blobs._inputSystem = _inputSystem;
            _baby = baby;
            return baby;

        }
        public Spoon SpawnSpoon()
        {
            Spoon spoon = _SpawnSpoon.SpawnSpoon();
            spoon._dragAndDrop.OnDragStart += FoodOnDragStart;
            spoon._dragAndDrop.OnDragEnded += FoodStopDragging;
            spoon._dragAndDrop.OnDrag += FoodOnDrag;
            return spoon;

        }
        public void SpawnCookie()
        {
            foreach (GameObject point in spawnCookiesPoints)
            {
                int count = 0;
                Quaternion q = new Quaternion();
                q.eulerAngles = new Vector3(0, 0, 0.5f);
                Cookies cookie = _SpawnCookie.SpawnCookies(point.transform.position, q);
                cookie._dragAndDrop.OnDragStart += FoodOnDragStart;
                cookie._dragAndDrop.OnDragEnded += FoodStopDragging;
                cookie._dragAndDrop.OnDrag += FoodOnDrag;
                count++;
            }

        }

        public Napkin SpawnNapkin()
        {
            Napkin napkin = _SpawnNapkin.SpawnNapkin(spawnPointNapkin.transform.position);
            napkin.DragAndDrop.OnDrag += FoodOnDrag;
            napkin.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
            Napkin = napkin;
            return napkin;

        }

        //activate hint pointer
        private void ActivateHint(Vector3 startPosition, Vector3 endPosition)
        {
            HintSystem.Instance.ShowPointerHint(startPosition, endPosition, 1);

        }

        //finishes the level
        public void LvlEnd()
        {
            _soundSystem.PlaySound("Win");
            _sceneLoader.LoadScene(SceneType.Quest, true);
        }

        void Start()
        {
            SpawnEatZone();
            SpawnBottle(spawnBottlePoint.transform.position);
            SpawnSpoon();
            SpawnCookie();
            SpawnNapkin();
        }




    }
}
