using System;
using DG.Tweening;
using Sound;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UsefulComponents;

namespace Fishing.Spawners
{
    public class FishingLevelMediator : MonoBehaviour
    {
        
        [SerializeField] private FishActorUI _actorUI;
        [SerializeField] private BoatSpawner _boatSpawner;
        [SerializeField] private FishSpawner _fishSpawner;
        [SerializeField] private FishLevelConfig _fishConfig;
        [SerializeField] private FishingNetSpawner _netSpawner;
        [SerializeField] private FishBasketSpawner _basketSpawner;
        [SerializeField] private Image fadeImage;

        [Space(10)]
        [Header("Extras")]
        [SerializeField] private Transform _startHintPos;

        private FishingNet _net;
        private Hook _hook;
        private Animator _boatAnimator;
        private BasketPrefab _basket;
        
        private void Start()
        {
            InitHUDSpawn();
            _netSpawner.OnSpawn += NetSpawned;
            _boatSpawner.OnSpawn += BoatSpawned;
            _boatSpawner.SpawnBoat();
            StartHint();
        }

        private void InitHUDSpawn()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(_actorUI.ShowProgressBar);
            sequence.AppendInterval(0.5f);
            sequence.AppendCallback(_actorUI.AppearFishCounter);
            sequence.AppendCallback(_netSpawner.SpawnNet);
        }

        private void BoatSpawned(Hook hook, Animator animator)
        {
            _hook = hook;
            _boatAnimator = animator;
            _hook.OnDoHook += HideHint;
            _actorUI.InitProgressBar(hook);
        }

        private void NetSpawned(FishingNet net)
        {
            _net = net;
            _net.OnCatchEnough += SpawnBasket;
            _actorUI.AppearFishCounter();
            _hook.NetSpawned(net);
        }
        
        private void SpawnBasket()
        {
            _basket = _basketSpawner.SpawnFishBasket();
            _actorUI.InitWinInvoker(_basket);
            _basket.STORED_MAX_COUNT = _fishConfig.FishSpawnCount;
            _basket.SetSortingIndex();
            _basket.AddComponent<FishTriggerObserver>().OnTriggerEnter += _basket.StoreObj;
            BasketSpawned(_basket);
        }

        private void BasketSpawned(BasketPrefab basket)
        {
            _fishSpawner.DisappearFishes();
            _basket = basket;
            _basket.OnBasketArrived += _net.MakeCaughtFishesDraggable;
            _basket.OnBasketLeft += _net.Disappear;
            _basket.OnAllProductsStored += InitWinInvoker;
            _actorUI.InitWinInvoker(basket);
            UpdateHUD();
        }
        
        private void InitWinInvoker()
        {
            _actorUI.InitWinInvoker(_basket);
        }

        private void StartHint()
        {
            HintSystem.Instance.ShowPointerHint(_startHintPos.position);
            _boatAnimator.SetBool("IsFishing", false);
        }

        private void HideHint()
        {
            HintSystem.Instance.HidePointerHint();
            _boatAnimator.SetBool("IsFishing", true);
        }

        private void UpdateHUD()
        {
            _actorUI.HideProgressBar();
            fadeImage.DOFade(0.85f, 1f);
        }

        private void OnDestroy()
        {
            _netSpawner.OnSpawn -= NetSpawned;
            _boatSpawner.OnSpawn -= BoatSpawned;
            var triggerObserver = _basket.GetComponent<FishTriggerObserver>();
            triggerObserver.OnTriggerEnter -= _basket.StoreObj;
        }
    }
}