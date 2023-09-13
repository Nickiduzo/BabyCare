using TMPro;
using UnityEngine;

namespace UI
{
    public class FishCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private int _fishCount;

        private void OnEnable()
        {
            FishActorUI.OnAppearFishCounter += Reset;
            FishActorUI.OnUpdateFishCounter += AddFish;
            FishActorUI.OnWithDrawFish += WithdrawFish;
        }

        private void OnDisable()
        {
            FishActorUI.OnAppearFishCounter -= Reset;
            FishActorUI.OnUpdateFishCounter -= AddFish;
            FishActorUI.OnWithDrawFish -= WithdrawFish;
        }

        public void AddFish()
        {
            _fishCount++;
            _text.text = _fishCount.ToString();
        }

        public void WithdrawFish()
        {
            _fishCount--;
            _text.text = _fishCount.ToString();
        }

        internal void Reset()
        {
            _fishCount = 0;
            _text.text = _fishCount.ToString();
        }
    }
}