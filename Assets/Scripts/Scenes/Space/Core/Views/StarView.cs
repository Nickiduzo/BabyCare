using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StarView : TriggerObserverWithPayload<RocketView>, ICollectable
{
    public static UnityEvent<ICollectable> OnCollectStar { get; private set; } = new();
    public static UnityEvent<int> OnCertainAmount { get; private set; } = new();

    [SerializeField] ParticleView _starParticle;
    [SerializeField] ParticleView _timerParticle;
    [SerializeField] Animator _animator;
    [SerializeField, Range(1, 5)] int _scorePoints;
    [SerializeField, Range(5, 100)] int _scoreToIncreaseTime;
    [SerializeField, Range(5, 20)] int _timeForIncrease = 10;

    public Animator Animator => _animator;
    public int ScorePoints => _scorePoints;

    private void Start()
    {
        OnTriggerEnter += CollectStar;
    }

    private void CollectStar(RocketView _)
    {
        OnCollectStar?.Invoke(this);
        Instantiate(_starParticle, transform.position, _starParticle.transform.rotation);

        //When an object is divided by _scoreToIncreaseTime without a remainder
        if (ScoreService.Score % _scoreToIncreaseTime == 0)
        {
            OnCertainAmount?.Invoke(_timeForIncrease);
            Instantiate(_timerParticle, transform.position, _starParticle.transform.rotation);
        }

        Destroy(gameObject); 
    }
}
