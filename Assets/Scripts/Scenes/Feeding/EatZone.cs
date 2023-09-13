using DG.Tweening;
using Sound;
using System.Collections;
using UnityEngine;
using UsefulComponents;

namespace Feeding
{
    public class EatZone : MonoBehaviour
    {

        // Serialized fields for inspector assignment4
        public FeedingSceneMediator mediator;

        [SerializeField] private FeedingSceneController _config;
        [SerializeField] private BottleObserver _observerBottle;
        [SerializeField] private SpoonObserver _observerSpoon;
        [SerializeField] private CookiesObserver _observerCookies;
        [SerializeField] private AppleObserver _observerApple;
        [SerializeField] private NapkinObserver _observerNapkin;
        [SerializeField] internal Animator babyAnimation;
        [SerializeField] private GameObject _particlePoint;

        // Properties for other systems
        public FxSystem Fxsystem { get; set; }
        public SoundSystem SoundSystem { get; set; }

        // Properties for game objects
        public Porridge Porridge { get; set; }
        public Apple Apple { get; set; }
        public Blobs Blobs { get; set; }
        public BabyFeeding Baby { get; set; }

        // Idle time and food left counters
        public int idletime = 0; // Time without player action 
        public int FoodLeft = 11;

        // Method to initialize the class
        public void Construct()
        {
            // TODO: Add initialization logic if needed
        }

        private void Awake()
        {
            // Subscribe to trigger events of different food observers
            _observerBottle.OnTriggerStay += Process;
            _observerSpoon.OnTriggerStay += Process;
            _observerCookies.OnTriggerStay += Process;
            _observerApple.OnTriggerStay += Process;
            _observerNapkin.OnTriggerExit += Process;

        }

        private void OnDestroy()
        {
            // Unsubscribe from trigger events to avoid memory leaks
            _observerBottle.OnTriggerStay -= Process;
            _observerSpoon.OnTriggerEnter -= Process;
            _observerCookies.OnTriggerStay -= Process;
            _observerApple.OnTriggerStay -= Process;
            _observerNapkin.OnTriggerEnter -= Process;

        }



        // Method to handle feeding from a bottle
        private void Process(Bottle bottle)
        {

            if (bottle.drop && Blobs.EatingFood() && !Baby.eatAnimationPlay)
            {
                Debug.Log("Processing Bottle...");
                
                bottle.drop = false;
                Baby.eatAnimationPlay = true;
                babyAnimation.SetBool("Drag", false);
                babyAnimation.Play("Babybottle");
                PlayBlobsFullAnimation();
                SoundSystem.PlaySound("drinking");
                bottle.transform.DOLocalMove(new Vector3(0, 0, 1), 0.5f);
                Destroy(bottle.gameObject);
                FoodLeft--;
            }
        }

        // Method to handle feeding from a spoon
        private void Process(Spoon spoon)
        {

            if (spoon.drop && Blobs.EatingFood() && !Baby.eatAnimationPlay)
            {
                Debug.Log("Processing Spoon...");
                Baby.EatingFoodSpawnBlob();
                spoon.drop = false;
                Baby.eatAnimationPlay = true;
                babyAnimation.SetBool("Drag", false);
                babyAnimation.Play("BabyEating");
                SoundSystem.PlaySound("bobbing");
                PlayBlobsFullAnimation();

                if (Porridge.EatingPorridge())
                {
                    mediator.SpawnSpoon();
                    Destroy(spoon.gameObject);
                }
                else
                {
                    Destroy(spoon.gameObject);
                }
                FoodLeft--;
            }
        }

        // Method to handle feeding cookies
        private void Process(Cookies cookies)
        {

            if (cookies.drop && Blobs.EatingFood() && !Baby.eatAnimationPlay)
            {
                Debug.Log("Processing Cookies...");
                Baby.EatingFoodSpawnBlob();
                cookies.drop = false;
                Baby.eatAnimationPlay = true;
                babyAnimation.SetBool("Drag", false);
                babyAnimation.Play("BabyEating");
                PlayBlobsFullAnimation();
                SoundSystem.PlaySound("cookie");
                cookies.transform.DOLocalMove(new Vector3(0, 0, 1), 0.5f);
                Destroy(cookies.gameObject);
                FoodLeft--;
            }
        }

        // Method to handle feeding an apple
        private void Process(Apple apple)
        {
            if (apple.drop && Blobs.EatingFood() && !Baby.eatAnimationPlay)
            {
                Debug.Log("Processing Apple...");
                Baby.EatingFoodSpawnBlob();
                apple.drop = false;
                Baby.eatAnimationPlay = true;
                babyAnimation.Play("BabyEating");
                PlayBlobsFullAnimation();
                SoundSystem.PlaySound("bobbing");
                if (this.Apple.EatingApple())
                {
                    // TODO: Implement apple eating logic
                }
                else
                {
                    babyAnimation.SetBool("Drag", false);
                    Destroy(apple.gameObject);
                }
                FoodLeft--;
            }
        }

        // Method to handle cleaning with a napkin
        private void Process(Napkin napkin)
        {
            Debug.Log("Cleaning with Napkin...");
            babyAnimation.SetBool("Drag", false);
            idletime = 0;
        }

        // Method called when cleaning is successful
        private void SuccsesClean()
        {
            PlaySuccses();
            babyAnimation.SetBool("Blobs", false);
            CheckLvlEnd();
        }

        // Method to check if the level is complete
        private void CheckLvlEnd()
        {
            if (FoodLeft <= 0 && Blobs.blobsCount <= 0)
            {
                Debug.Log("Level Complete!");
                mediator.LvlEnd();
            }
        }

        // Method to play animation when blobs are full
        private void PlayBlobsFullAnimation()
        {
            if (Blobs.blobsCount == Blobs.maxBlobsCount)
            {


                Debug.Log("Blobs Full!");
                HintSystem.Instance.ShowPointerHint(mediator.spawnPointNapkin.transform.position, mediator.spawnPointNapkin.transform.position, 1);
                Baby.StopAfterEatAnimations();
                StartCoroutine(PlaySoundWithDelay("disgust", 0.5f));
                
                babyAnimation.SetBool("Blobs", true);
                Baby.eatAnimationPlay = false;

            }
        }

        private IEnumerator PlaySoundWithDelay(string soundName, float delay)
        {
            yield return new WaitForSeconds(delay); // Затримка перед відтворенням звуку.
            SoundSystem.PlaySound(soundName);
        }

        // Method to play animation and sound for successful action
        private void PlaySuccses()
        {
            Debug.Log("Successful Action!");
            Fxsystem.PlayEffect("Success", _particlePoint.transform.position);
            SoundSystem.PlaySound("Success");
        }

        private void Update()
        {
            // Check idle time and play animations accordingly
            idletime++;
            if (idletime == 1000 && FoodLeft > 0)
            {
                babyAnimation.Play("BabyIdleHendToMouth");
            }
            else if (idletime == 2000 && FoodLeft > 0)
            {
                babyAnimation.Play("BabyIdleCloud");
                idletime = 400;
            }
        }

        private void Start()
        {
            // Subscribe to the "isClean" event of Blobs
            Blobs.isClean += SuccsesClean;
        }
    }
}
