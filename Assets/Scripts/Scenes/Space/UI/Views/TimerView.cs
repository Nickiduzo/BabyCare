using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class TimerView : HudElement, IWinInvoker
{
    public event Action OnWin;

    [SerializeField] RectTransform _minuteHand;
    [SerializeField] RectTransform _secHand;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField, Range(10, 120)] int _fullTime = 60;

    private int _remainingTime;

    public int RemainingTime
    {
        get { return _remainingTime; }
        set
        {
            ChangeTime(value);
            _remainingTime = value;

            if (value <= 0)
            {
                ChangeTime(0);
                OnWin?.Invoke();
            }
        }
    }

    private new void Start()
    {
        base.Start();
        Appear();

        //Start timer
        RemainingTime = _fullTime;
        StartCoroutine(StartTimer());

        //Rotate hands
        _minuteHand.DORotate(new Vector3(0f, 0f, -360f), 60f, RotateMode.FastBeyond360).SetRelative(true).SetLoops(-1).SetEase(Ease.Linear);
        _secHand.DORotate(new Vector3(0f, 0f, -360f), 1f, RotateMode.FastBeyond360).SetRelative(true).SetLoops(-1).SetEase(Ease.Linear);
    }

    public void ChangeTime(int time)
    {
        _text.text = time.ToString();
    }

    IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);

            if (RemainingTime > 0)
                RemainingTime--;
            else
                yield break;
        }
    }
}
