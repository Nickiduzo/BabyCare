using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Product : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprtiteRenderer;
    [SerializeField] private Sprite[] _fishSprites;

    // spawn product and move to basket
    public Tween MoveToBasket(float delay, float apearHight, float duration, Ease ease, GameObject _basket)
    {
        if (gameObject.name == "FishProduct(Clone)")
            SetRandomFishSprite();

        Transform destinationTransform = transform;
        Vector3 spawnPoint = new Vector3(destinationTransform.position.x, destinationTransform.position.y + apearHight, destinationTransform.position.z);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(spawnPoint, 0f));
        sequence.AppendInterval(delay);
        sequence.Join(transform.DORotate(Vector3.zero, 0f));
        sequence.Append(transform.DOMove(destinationTransform.position, duration)).SetEase(ease)
            .OnComplete(() => 
            {
                gameObject.transform.parent = _basket.transform;
            });
        sequence.Play();

        return sequence;
    }

    // if product is fish set different sprites for each fish
    private void SetRandomFishSprite()
        =>_sprtiteRenderer.sprite = _fishSprites[Random.Range(0, _fishSprites.Length)];
}
