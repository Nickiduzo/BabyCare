using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Dream
{
    public class RoomLight : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> _spriteRenderersOfElement;
        [SerializeField] private Image _background;
        [SerializeField] private Camera _camera;

        //Changing color of Background, backgroundcamera and other sprites that needs to change color. Needed to make effect of changing light in room

        //Adding element to list[_spriteRenderersOfElement].
        public void AddElement(SpriteRenderer spriteRenderer)
        {
            if (spriteRenderer == null)
                throw new ArgumentNullException(nameof(spriteRenderer));

            _spriteRenderersOfElement.Add(spriteRenderer);
        }

        public void ChangeColor(Color all, Color bg, float duration)
        {
            if (duration < 0)
                throw new ArgumentOutOfRangeException(nameof(duration));
            
            _background.DOColor(bg, duration);
            _camera.DOColor(all, duration);

            foreach (var spriteRenderer in _spriteRenderersOfElement)
            {
                spriteRenderer.DOColor(bg, duration);
            }
        }
    }
}