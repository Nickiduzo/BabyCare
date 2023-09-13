using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeScreenPanel : MonoBehaviour
{
    [SerializeField] private float _duration;

    private Image _fadeScreen;

    public event Action FadingIn;

    private void Awake()
    {
        _fadeScreen = GetComponent<Image>();
        FadeOut();
    }

    public Tween FadeOut()
    {
        _fadeScreen.DOFade(1f, 0f);
        return _fadeScreen.DOFade(0f, _duration * 1.5f).SetEase(Ease.InOutQuad).OnComplete(() => _fadeScreen.enabled = false);
    }

    public Tween FadeIn()
    {
        FadingIn?.Invoke();
        _fadeScreen.enabled = true;
        return _fadeScreen.DOFade(1f, _duration);
    }
}
