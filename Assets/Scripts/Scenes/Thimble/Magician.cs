using System;
using Sound;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Thimble
{
    public class Magician : MonoBehaviour
    {
        [SerializeField] public Animator _animator;
        private IReadOnlyList<ThimbleCup> _cups;
        private Timer _timer;
        private ScoreCount _score;
        private FxSystem _fxSystem;
        private SoundSystem _soundSystem;

        private int _maxTossCount = 2;
        private int _tossCount = 1;

        public event Action OnEndGame;

        //when construct magician gameObject
        public void Construct(Timer timer, ScoreCount score, FxSystem fxSystem, SoundSystem soundSystem, IReadOnlyList<ThimbleCup> cups)
        {
            _timer = timer ? timer : throw new ArgumentNullException(nameof(timer));
            _score = score ? score : throw new ArgumentNullException(nameof(score));
            _fxSystem = fxSystem ? fxSystem : throw new ArgumentNullException(nameof(fxSystem));
            _soundSystem = soundSystem ? soundSystem : throw new ArgumentNullException(nameof(soundSystem));
            
            if (cups.Any(cup => cup == null))
                throw new ArgumentNullException(nameof(cups));

            _cups = cups;
        }
        
        private void Start()
        {
            NextTour();
        }

        //when start toss cups
        private void StartToss()
        {
            _cups[^1].OnEndShow -= StartToss;
            Toss();
        }

        //when tossing cups
        private void Toss()
        {
            int first = Random.Range(0, _cups.Count);
            int second = Random.Range(0, _cups.Count);
            while (first == second)
            {
                second = Random.Range(0, _cups.Count);
            }

            _cups[first].OnEndMove -= Toss;
            _cups[second].OnEndMove -= Toss;
            if (_tossCount > 0)
            {
                _tossCount--;
                Toss(first, second);
                _cups[second].OnEndMove += Toss;
            }
            else
            {
                TossEnd();
                _tossCount = _maxTossCount;
            }
        }

        //move 2 cups
        private void Toss(int cupFirst, int cupSecond)
        {
            if (_cups[cupFirst].transform.position.z > _cups[cupSecond].transform.position.z)
            {
                _cups[cupFirst].Move(_cups[cupSecond].transform, -0.3f);
                _cups[cupSecond].Move(_cups[cupFirst].transform, 0.3f);
            }
            else
            {
                _cups[cupFirst].Move(_cups[cupSecond].transform, 0.3f);
                _cups[cupSecond].Move(_cups[cupFirst].transform, -0.3f);
            }
        }

        //when end toss cups
        private void TossEnd()
        {
            foreach (ThimbleCup cup in _cups)
            {
                cup.StartInteractivity();
                cup.OnStartShow += StopInteractivityAllCup;
                cup.OnEndShow += NextTour;
                if (cup.withGoldenBall)
                {
                    cup.OnFindGoldenBall += SuccessFind;
                }
                else
                {
                    cup.OnDontFindGoldenBall += DontFind;
                }
            }
        }

        //All cup ball showing animation
        private void UpAllCup()
        {
            foreach (ThimbleCup cup in _cups)
            {
                cup.MoveShowGoldenBall();
            }

            _cups[^1].OnEndShow += StartToss;
            _animator.Play("StartToss");
        }

        //Start next tour
        private void NextTour()
        {
            if (_timer.End)
                return;

            foreach (ThimbleCup cup in _cups)
            {
                cup.StopInteractivity();
                cup.OnFindGoldenBall -= SuccessFind;
                cup.OnDontFindGoldenBall -= DontFind;
                cup.OnEndShow -= NextTour;
            }

            UpAllCup();
        }

        //when find golden ball
        private void SuccessFind()
        {
            _maxTossCount++;

            if (!_timer.End)
                _timer.AddTime(10);

            _score.AddScore(1);
            foreach (var cup in _cups.Where(cup => cup.withGoldenBall))
            {
                if (!_timer.End)
                    _fxSystem.PlayEffect("PlusTime", cup.gameObject.transform.position);

                _fxSystem.PlayEffect("Success", cup.gameObject.transform.position);
            }

            _soundSystem.PlaySound("Success");
            _animator.Play("Success");

            if (_timer.End)
                OnEndGame?.Invoke();
        }

        //when dont find golden ball
        private void DontFind()
        {
            _soundSystem.PlaySound("Failed");
            _animator.Play("Failed");
            if (_timer.End)
                OnEndGame?.Invoke();
        }

        //stops the ability to interact for all cups
        private void StopInteractivityAllCup()
        {
            foreach (var cup in _cups) 
                cup.StopInteractivity();
        }
    }
}