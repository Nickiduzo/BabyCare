using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using Sound;
using UnityEngine.Events;

namespace UI
{
    public class WinPannel : Panel
    {
        [SerializeField] private float appearDuration;
        [SerializeField] private Vector3 scaleEndValue;
        [SerializeField] private WinPannelFinalScore FinalScore;
        [SerializeField] private List<GameObject> hideElement;
        [SerializeField] private GameObject Pannel;
        [SerializeField] public SoundSystem _soundSystem;

        public UnityEvent OnAppear { get; private set; } = new();

        // for the appearance  Win Panne
        public override void Appear()
        {
            Pannel.SetActive(true);
            Pannel.transform.DOScale(scaleEndValue, appearDuration).SetEase(Ease.InBack); // Win Panne pop up animation 
            _soundSystem.PlaySound("Win");
            //disable other ui element
            foreach (GameObject e in hideElement)
            {
                e.SetActive(false);
            }        
            FinalScore.Appear();

            OnAppear?.Invoke();
        }
        // for the Disappear  Win Panne
        public override void Disappear()
        {
            Pannel.transform.DOScale(new Vector3(0, 0, 0), appearDuration).SetEase(Ease.InBack);
            Pannel.SetActive(false);
        }
    }
}
