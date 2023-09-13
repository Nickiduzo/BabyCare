using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBaloonController : MonoBehaviour
{
    [SerializeField] List<GameObject> _baloons;
    [SerializeField] float _duration;
    [SerializeField] Vector2 _endPos;

    private List<Vector3> _initialPositions = new();

    void Start()
    {
        for (int i = 0; i < _baloons.Count; i++)
        {
            _initialPositions.Add(_baloons[i].transform.position);
        }

        StartCoroutine(FlyAnim());
    }

    IEnumerator FlyAnim()
    {
        while (true)
        {
            foreach (var baloon in _baloons)
            {
                yield return baloon.transform.DOMove(_endPos, _duration).SetEase(Ease.Linear).OnUpdate(() =>
                {
                    if (baloon.transform.position.y > 11)
                        DOTween.Kill(baloon);
                }).WaitForKill();
            }
            for (int i = 0; i < _baloons.Count; i++)
            {
                _baloons[i].transform.position = _initialPositions[i];
            }
        }
    }
}
