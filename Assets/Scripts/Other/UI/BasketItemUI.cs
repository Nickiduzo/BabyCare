using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BasketItemUI : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.DOFade(0f, 0f);
    }
    public Tween ApearWithDelay(float delay, float apearHight, float duration, Ease ease)
    {
        Transform destinationTransform = transform;
        Vector3 spawnPoint = new Vector3(destinationTransform.position.x, destinationTransform.position.y + apearHight, destinationTransform.position.z);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(delay);
        sequence.Append(transform.DOMove(spawnPoint, 0f));
        sequence.Join(transform.DORotate(Vector3.zero, 0f));
        sequence.Join(_image.DOFade(1f, 0.1f));
        sequence.Append(transform.DOMove(destinationTransform.position, duration)).SetEase(ease);
        sequence.Append(transform.DORotate(destinationTransform.rotation.eulerAngles, duration / 2f));


        sequence.Play();

        return sequence;
    }
}
