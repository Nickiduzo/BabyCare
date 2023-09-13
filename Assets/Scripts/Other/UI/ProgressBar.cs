using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : HudElement
{
    [SerializeField] private Slider _slider;
    [SerializeField] private ProgressBarSeparatorsCreator _progressBarSeparatorsCreator;


    public void SetValue(int current)
    {
        _slider.DOValue(current, 1f);
    }

    public void SetMaxValue(int max)
    {
        _slider.maxValue = max;
        SetSeparators(max);
    }
    
    public void ResetSeparators()
    {
        _progressBarSeparatorsCreator.ResetSeparators();
    }

    public void SetCurrentValue(int value)
    {
        _slider.DOValue(value, 1f);
    }
    private void SetSeparators(int value)
    {
        float separatorSizeX = _slider.GetComponent<RectTransform>().rect.width / value;
        _progressBarSeparatorsCreator.CreateSeparators(value, separatorSizeX);
    }
}