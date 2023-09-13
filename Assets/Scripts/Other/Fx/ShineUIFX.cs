using DG.Tweening;
using UnityEngine;

public class ShineUIFX : MonoBehaviour
{
    [SerializeField] private float _appearDuration;

    public void Apear()
    {
        AppearWithScale();
    }
    private void AppearWithScale()
    {
        Vector3 cachedScale = transform.localScale;
        transform.localScale = Vector3.zero;


        transform.DOScale(cachedScale, _appearDuration);
    }
}
