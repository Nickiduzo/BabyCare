using Sound;
using System;
using UnityEngine;

namespace NewFishing
{
    public class Fish : MonoBehaviour
    {
        public event Action FishInBucket;
        public event Action FishOnHook;

        [SerializeField] private HookObserver _observerHook;
        [SerializeField] private Collider2D _collider;
        [SerializeField] public int fishTypo;
        [SerializeField] public Animator fishAnimation;
        [SerializeField] private SoundSystem _sound;
        [SerializeField] private string _hookSound;

        public Hook _hook { get; set; }

        public FxSystem fxsystem { get; set; }
        public SoundSystem soundSystem { get; set; }

        private int idleAnimCountToOpenMouth = 2;
        private int idleAnimCount = 0;

        private bool onhook = false;

        private void Awake()
        {
            _observerHook.OnTriggerEnter += Process;
          

        }

        private void OnDestroy()
        {
            _observerHook.OnTriggerEnter -= Process;

        }

        //when construct Fish gameObject
        public void Construct()
        {
          
        }

        //when hook enter fish
        private void Process(Hook hook)
        {
            _hook = hook;
            if (!hook.WithFish)
            {
                playIdleAnimation();
                gameObject.transform.SetParent(hook.gameObject.transform);
                gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -1f), gameObject.transform.rotation);
                gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), gameObject.transform.rotation);
                _collider.enabled = true;
                hook.HookUp(10);
                onhook = true;
                _sound.PlaySound(_hookSound);
                FishOnHook?.Invoke();
            }
        }

        //when idle Animation End
        public void idleAnimationEnd()
        {
           idleAnimCount++;
           
           if (idleAnimCount == idleAnimCountToOpenMouth)
           {
                if (!onhook)
                {
                    playEatAnimation();
                }
                idleAnimCount = 0;
           }
        }

        //choose what fish play Eat Animation
        public void playEatAnimation()
        {
            
            switch (fishTypo)
            {
                case 1:
                    fishAnimation.Play("Fish1topEat");
                    break;
                case 2:
                    fishAnimation.Play("Fish2topEat");
                    break;
                case 3:
                    fishAnimation.Play("Fish3topEat");
                    break;
                case 4:
                    fishAnimation.Play("Fish4topEat");
                    break;
                case 5:
                    fishAnimation.Play("Fish5topEat");
                    break;
            }
        }

        //choose what fish play Idle Animation
        public void playIdleAnimation()
        {
            if(fishTypo == 1 || fishTypo == 5)
            {
                fishAnimation.Play("FishtopIdle");
            }
            else
            {
                fishAnimation.Play("FishtopIdle2");
            }          
        }

        //when fish start eat
        public void fishStartEat()
        {
            _collider.enabled = true;
        }
        //when fish end eat
        public void fishEndEat()
        {
            if (!onhook)
            {
                _collider.enabled = false;
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            idleAnimCountToOpenMouth = UnityEngine.Random.Range(2, 6);
            _collider.enabled = false;
        }

    }
}
