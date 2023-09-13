using DG.Tweening;
using UnityEngine;

public class ArrowHint : MonoBehaviour
{
    [SerializeField] private bool _activateOnStart;

    private void Start()
    {
        if (_activateOnStart)
            ActivateArrow();
    }

    public void ActivateArrow()
    {
        this.gameObject.SetActive(true);
        this.transform.DOScale(new Vector3(0.9f, 0.9f, 0.5f), 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
