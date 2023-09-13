using DG.Tweening;
using Inputs;
using Sound;
using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Fishing
{
    public class Hook : MonoBehaviour, IProgressWriter
    {
        private const string Rod = "Rod";
        private const string CatchFish = "CatchFish";
        public event Action OnProgressChanged;
        public int CurrentProgress => _catchedFishCount;
        public int MaxProgress => _config.FishSpawnCount;

        [SerializeField] private FishLevelConfig _config;
        [SerializeField] private float _movingDuration;
        [SerializeField] private float _movingToStartPointDuration;
        [SerializeField] private float _fishUnHookTime;
        [SerializeField] private Transform _heightBoundPosition;
        [SerializeField] private float _hookCooldownTime;

        private InputSystem _inputSystem;

        private bool _catchingFish;
        private bool _isFirstFish;
        private int _catchedFishCount;
        private bool _isHooking;

        private UnhookFish _unhookFish;
        private Vector3 _startPosition;
        private Collider2D _hookCollider;
        private Vector3 _hookPoint;
        private Sequence _sequence;
        private Transform _previousUnhookFishParent;
        private ISoundSystem _soundSystem;
        private FishingNet _fishingNet;
        private Coroutine _hintRoutine;
        private Animator _boatAnimator;

        public event Action OnDoHook;

        private float LastHookTime = 0;
        public bool IsHookReady => Time.time - LastHookTime > _hookCooldownTime;

        private void OnDisable()
        {
            _inputSystem.OnTapped -= Catch;
            _inputSystem.OnTapped -= ActivateHint;
        }

        public void Construct(InputSystem inputSystem, ISoundSystem soundSystem, Animator animator)
        {
            _soundSystem = soundSystem;
            _inputSystem = inputSystem;
            _isFirstFish = true;
            _boatAnimator = animator;
            _hookCollider = GetComponent<Collider2D>();
            _startPosition = transform.position;
            _inputSystem.OnTapped += Catch;
            _inputSystem.OnTapped += ActivateHint;
            LastHookTime = Time.time;
        }

        public void NetSpawned(FishingNet net)
            => _fishingNet = net;

        private void OnTriggerEnter2D(Collider2D target)
        {
            if (target.TryGetComponent(out UnhookFish unhook))
            {
                _unhookFish = unhook;
                _unhookFish.Hooked();

                _previousUnhookFishParent = _unhookFish.transform.parent;

                _catchingFish = true;
                _hookCollider.enabled = false;

                PutBack();
            }

            if (!target.TryGetComponent(out Fish fish) || !CanCatch(fish))
            {
                if (target.TryGetComponent<IUncaughtable>(out var noFish))
                {
                    noFish.Caught();
                }
                return;
            }

            if (_isFirstFish)
            {
                _isFirstFish = false;
            }

            LastHookTime = Time.time;


            _catchedFishCount++;
            _soundSystem.PlaySound(CatchFish);
            OnProgressChanged?.Invoke();


            fish.Hooked();
            MakeChildOfHook(fish.gameObject);

            _catchingFish = true;
            _hookCollider.enabled = false;

            PutInNet(fish);
        }

        //Put back hooked octopus or crab
        private void PutBack()
        {
            Vector3 cachedPosition = transform.position;
            Vector3 destination = CalculateMidOfHookPoint();

            _sequence.Kill();
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(MakeChildUnhookFish);
            _sequence.Append(MoveTo(destination, _movingDuration));
            _sequence.Append(MoveTo(cachedPosition, _movingDuration));
            _sequence.AppendCallback(_unhookFish.UnHooked);
            _sequence.AppendCallback(MakeUnChildUnhookFish);
            _sequence.Append(MoveToStartPoint());
            _sequence.AppendCallback(ResetHook);
        }

        private Vector3 CalculateMidOfHookPoint()
            => (_hookPoint + _startPosition) / 2;

        private void MakeUnChildUnhookFish()
            => _unhookFish.transform.SetParent(_previousUnhookFishParent);

        private void MakeChildUnhookFish()
            => _unhookFish.transform.SetParent(transform);

        private void MakeChildOfHook(GameObject obj)
        {
            obj.transform.SetParent(transform);
            obj.transform.position = transform.position;
        }

        private bool CanCatch(Fish fish)
            => !fish.IsCatch && !_catchingFish;

        // Put catch fish in fishing net

        private void PutInNet(Fish fish)
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            MoveFishToNet(fish);
            _sequence.Append(MoveToStartPoint());
            _sequence.AppendCallback(ResetHook);
        }

        private void MoveFishToNet(Fish fish)
        {
            var fishSequence = DOTween.Sequence();
            fishSequence.AppendInterval(_movingToStartPointDuration - _fishUnHookTime);
            fishSequence.Append(MoveToNet(fish));
        }

        // Make hook able to catch fish
        private void ResetHook()
        {
            _catchingFish = false;
            _hookCollider.enabled = true;
            _isHooking = false;

            if (_catchedFishCount < _config.FishSpawnCount) return;

            DOTween.Kill(_sequence);
            enabled = false;
        }

        private Tween MoveToStartPoint()
            => MoveTo(_startPosition, _movingToStartPointDuration);

        private Tween MoveToNet(Fish fish)
        {
            var posInNet = _fishingNet.NetStorePosition;
            fish.SetPositionInNet(posInNet);
            return fish.JumpTo(posInNet).OnComplete(() => PlaySuccess(fish));
        }

        private void PlaySuccess(Fish fish)
        {
            FxSystem.Instance.PlayEffect("Success", fish.gameObject.transform.position);
            _soundSystem.PlaySound("Success");
        }

        private Tween MoveToHookPoint()
            => MoveTo(_hookPoint, _movingDuration);

        private Tween MoveTo(Vector3 point, float speed)
            => transform.DOMove(point, speed);

        // Does hook to desired position
        private void Catch(Vector3 targetPosition)
        {
            if (!CanHook()) return;

            _hookPoint = targetPosition;

            if (!IsFitInBound()) return;

            _isHooking = true;

            DoHook();
        }

        private bool CanHook()
            => !_catchingFish && !_isHooking && IsHookReady;

        private bool IsFitInBound()
            => _hookPoint.y < _heightBoundPosition.position.y;

        private void DoHook()
        {
            OnDoHook?.Invoke();

            _soundSystem.PlaySound(Rod);
            _sequence.Kill();
            _sequence = DOTween.Sequence(_sequence);
            _sequence.Append(MoveToHookPoint());
            _sequence.Append(MoveToStartPoint());
            _sequence.AppendCallback(ResetHook);

        }

        private void ActivateHint(Vector3 targetPos)
        {
            if (_hintRoutine != null)
            {
                StopCoroutine(_hintRoutine);
            }

            _hintRoutine = StartCoroutine(TimeToActivateHint());
        }
        
        private IEnumerator TimeToActivateHint()
        {
            yield return new WaitForSeconds(6);
            _boatAnimator.SetBool("IsFishing", false);
        }
    }
}