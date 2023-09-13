using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewFishing
{
    public class Bucket : MonoBehaviour
    {
        public event Action FishInBucket;
        public Hook _hook { get; set; }

        public List<FishSide> fishSide { get; set; }

        public int fishCount { get; set; }

        [SerializeField] private FishObserver _observerFish;
        [SerializeField] private HookObserver _observerHook;
        [SerializeField] public Animator bucketAnimation;

        private void Awake()
        {
            _observerFish.OnTriggerEnter += Process;
            _observerHook.OnTriggerStay += Process;
            _observerHook.OnTriggerEnter += Process;
            _observerHook.OnTriggerExit += Process;
        }

        private void OnDestroy()
        {
            _observerFish.OnTriggerEnter -= Process;
            _observerHook.OnTriggerStay -= Process;
            _observerHook.OnTriggerEnter -= Process;
            _observerHook.OnTriggerExit -= Process;
        }

        //when Bucket Fish gameObject
        public void Construct()
        {
        }

        //when fish enter bucket
        private void Process(Fish fish)
        {
            fishSide[fishCount].fishSideList[fish.fishTypo - 1].SetActive(true);
            _hook = fish._hook;
            FishInBucket?.Invoke();
            bucketAnimation.Play("BucketSucces");
            Destroy(fish.gameObject);
            if (_hook.WithFish)
                _hook.Unhook();
        }

        //when hook enter bucket
        private void Process(Hook hook)
        {
            if (hook.WithFish)
                hook.Unhook();
        }

        //when all fish in bucket
        public void ClearFishSide()
        {
            fishCount = 0;
            foreach (FishSide fs in fishSide)
            {
                for (int i = 0; i < 5; i++)
                    fs.fishSideList[i].SetActive(false);
            }
        }
    }
}