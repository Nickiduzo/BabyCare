using UI;
using UnityEngine;

public class MenuActorUI : MonoBehaviour
{
    [SerializeField] private HudElement[] _hudElements;
    [SerializeField] private FadeScreenPanel _fadePanel;

    private void Awake()
    {
        if( _fadePanel != null)
        {
            _fadePanel.FadingIn += DisapearHudElements;
        }
    }

    private void DisapearHudElements()
    {
        if (_hudElements == null)
            return;

        foreach (var element in _hudElements)
        {
            if(element != null)
            {
                element.Disappear();
            }
        }
    }

    private void OnDestroy()
    {
        if( _fadePanel != null)
        {
            _fadePanel.FadingIn -= DisapearHudElements;
        }
    }
}
