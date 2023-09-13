using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

namespace UI
{
    //TODO extract view, rework to not MonoBehaviour, _field style, make immutable
    public class Timer : MonoBehaviour
    {
        public event Action OnTimeEnd;

        [SerializeField, Min(0)] private float startTime = 60;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GameObject clockHand;
        private float acumulatedTime;
        public bool End { get; private set; }

        private void Start()
        {
            text.text = startTime.ToString();
            acumulatedTime = startTime;
        }

        public void AddTime(float time)
        {
            if (time < 0)
                throw new ArgumentOutOfRangeException(nameof(time));

            acumulatedTime += time;
            End = false;
        }

        private void Update()
        {
            if (End)
                return;

            acumulatedTime = Mathf.Max(0, acumulatedTime - Time.deltaTime);

            text.text = Mathf.Round(acumulatedTime).ToString();
            clockHand.transform.DOLocalRotate(new Vector3(0, 0, acumulatedTime * 90), 1);

            if (acumulatedTime == 0)
            {
                End = true;
                OnTimeEnd?.Invoke();
            }
        }
    }
}