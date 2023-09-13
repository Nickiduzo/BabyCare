using UnityEngine;

namespace UI
{
    public class FishHud : Hud
    {
        [SerializeField] private FishCounter _fishCounter;

        public override void Disappear()
        {
            base.Disappear();
            //_fishCounter.Disappear();
        }
    }

}