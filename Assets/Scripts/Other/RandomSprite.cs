using UnityEngine;

namespace Fishing
{
    public class RandomSprite : MonoBehaviour
    {
        [SerializeField] private FishBody[] _bodies;

        public void SetUpSpriteOrder(int _SortOrderIndex)
        {
            for (int i = 0; i < _bodies.Length; i++)
            {
                _bodies[i].ChangeSpriteSortOrder(_SortOrderIndex);
            }
        }

        public FishBody GetRandomSprite()
            => _bodies[Random.Range(0, _bodies.Length)];
    }
}