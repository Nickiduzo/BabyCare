using DG.Tweening;
using System.Linq;
using UnityEngine;

public class BasketUI : MonoBehaviour
{
    [SerializeField] private BasketItemUI[] _basketItemsUI;
    [SerializeField] private float _duration;
    [SerializeField] private float _spawnDelayRange;
    [SerializeField] private Ease _movingEase;
    [SerializeField] private float _apearHight;

    public Tween StartSpawnItems()
    {
        foreach (var item in _basketItemsUI)
        {
            if (item == _basketItemsUI.Last())
                return item.ApearWithDelay(Random.Range(0f, _spawnDelayRange), _apearHight, _duration, _movingEase);

            item.ApearWithDelay(Random.Range(0f, _spawnDelayRange), _apearHight, _duration, _movingEase);
        }

        return null;
    }
}
