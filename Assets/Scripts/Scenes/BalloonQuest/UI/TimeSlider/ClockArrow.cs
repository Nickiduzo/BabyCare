using Sound;
using UnityEngine;
using UnityEngine.UI;
using UI;
using System;

public class ClockArrow : MonoBehaviour, IWinInvoker
{
    [SerializeField] private float timer = 60f;
    [SerializeField] private Slider _slider;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private string alarmSound;
    [SerializeField] private float alarmTime;
    private bool _alarmed;
    public event Action OnWin;

    private void Awake()
    {
        _slider.maxValue = timer;
        _slider.value = timer;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            _slider.value = timer;

            if (timer <= alarmTime && !_alarmed)
            {
                _alarmed = true;
                soundSystem.PlaySound(alarmSound);
            }
            
            if(timer <= 0)
                OnWin?.Invoke();
        }
    }
}
