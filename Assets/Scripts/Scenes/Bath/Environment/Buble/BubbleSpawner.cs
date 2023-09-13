using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bath
{
    public class BubbleSpawner : MonoBehaviour
    {
        [SerializeField] private Foam _foam;
        [SerializeField] private List<Bubble> _bubblePrefabs = new();
        [SerializeField, Range(10, 200)] private int _maxBubbles = 10;
        [SerializeField, Range(0, 1)] private float _destroyBubblesTime;
        private readonly List<Bubble> _bubbles = new();

        private bool _isCraneOn = false;

        //Destroying all bubbles when tube is playing particle
        public void DestroyBubbles()
        {
            _isCraneOn = true;
            if (_bubbles.Count > 0)
            {
                StartCoroutine(DestroyBubblesRoutine());
            }
        }

        public void StopDestroyingBubbles()
        {
            _isCraneOn = false;
        }

        //Destroying all bubbles by coroutine, so all bubbles destroys one after another with delay
        private IEnumerator DestroyBubblesRoutine()
        {
            _foam.StartFoaming();

            while (_bubbles.Count != 0)
            {
                if (!_isCraneOn)
                    yield break;

                var bubble = _bubbles.First();

                bubble.Destroy();
                _foam.gameObject.SetActive(true);

                yield return new WaitForSeconds(_destroyBubblesTime);
                _bubbles.Remove(bubble);
            }
        }

        //Spawns bubble, invokes in sponge 
        public void SpawnBubble(Transform position)
        {
            if (_bubbles.Count <= _maxBubbles)
            {
                var bubbleClone = Instantiate(GetRandomBubble(), transform).GetComponent<Bubble>();
                bubbleClone.Construct(position);
                _bubbles.Add(bubbleClone);

                if (_isCraneOn)
                    DestroyBubbles();
            }
        }

        //Gets random bubble prefab from list
        private Bubble GetRandomBubble()
        {
            int index = Random.Range(0, _bubblePrefabs.Count);
            return _bubblePrefabs[index];
        }
    }
}