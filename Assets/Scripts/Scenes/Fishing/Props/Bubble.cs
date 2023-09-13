using DG.Tweening;
using System.Collections;
using UnityEngine;
namespace Fishing.Props
{
    public class Bubble : MonoBehaviour
    {
        private SpriteRenderer Renderer { get; set; }
        private Vector3 StartPos { get; set; }
        private Sequence sequence { get; set; }

        private void Awake()
        {
            Renderer = GetComponent<SpriteRenderer>();
            StartPos = this.transform.position;

            Play();
        }

        private void Play()
        {
            sequence.Kill();
            sequence = DOTween.Sequence();
            sequence.Append(Renderer.DOFade(0.25f, 0.2f));
            sequence.Append(transform.DOMove(StartPos + RandomEndPoint(), 3)).Insert(1.2f, Renderer.DOFade(0, 1f));
            sequence.AppendCallback(Reset);
        }

        private Vector3 RandomEndPoint() => new(0, Random.Range(4, 6), 0);

        private void Reset()
        {
            this.transform.position = StartPos;
            if (this.isActiveAndEnabled)
                StartCoroutine(RandomWaitRoutine());
        }

        private IEnumerator RandomWaitRoutine()
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            Play();
        }
    }
}