using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGallery
{
    public class ClockAnimation : MonoBehaviour
    {
        public float duration = 2.0f;
        private FxSystem fxSystem;
        private void Awake()
        {
            fxSystem = FindObjectOfType<FxSystem>();
        }

        public void ShowClock()
        {
            StartCoroutine(ShowClockCoroutine());
        }

        private IEnumerator ShowClockCoroutine()
        {
            fxSystem.PlayEffect("PlusTime", transform.position);
            yield return new WaitForSeconds(duration);
        }
    }
}