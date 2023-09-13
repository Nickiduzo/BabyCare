using System;
using DG.Tweening;
using UnityEngine;

namespace Dream
{
    public class TimeCycle : MonoBehaviour
    {
        public event Action OnBabyAwake;

        [SerializeField] Animator _animator;
        [SerializeField] RoomLight _roomLight;
        [SerializeField] GameObject _moon;
        [SerializeField] GameObject _sun;

        [SerializeField] private Color BgColorAwake;
        [SerializeField] private Color TurnOffLamp;
        [SerializeField] private Color ChildAwake;

        //Changing color of Environment to make effect of changing lights in the room

        public void AddElementsForChangingColor(SpriteRenderer spriteRenderer)
        {
            if (spriteRenderer == null)
                throw new ArgumentNullException(nameof(spriteRenderer));

            _roomLight.AddElement(spriteRenderer);
        }

        //Changing color of Environment when lights is off
        public void TurnOff()
        {
            _roomLight.ChangeColor(TurnOffLamp, TurnOffLamp, 0.1f);
        }

        //Changing color of Environment when sunrise
        private void SunRising()
        {
            _animator.SetTrigger("SunRise");
            _roomLight.ChangeColor(BgColorAwake, ChildAwake, 2f);
        }

        //Starting animation Sunrise
        public void BabyAwake()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(SunRising);
            sequence.AppendInterval(2f);
            sequence.Play();
        }
    }
}