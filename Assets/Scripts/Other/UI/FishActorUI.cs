using System;
using Fishing;

namespace UI
{
    public class FishActorUI : ActorUI
    {
        public static event Action OnUpdateFishCounter;
        public static event Action OnWithDrawFish;
        public static event Action OnAppearFishCounter;


        public override void InitProgressBar(IProgressWriter progressWriter)
        {
            base.InitProgressBar(progressWriter);
            _progressWriter.OnProgressChanged += UpdateFishCounter;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _progressWriter.OnProgressChanged -= UpdateFishCounter;
        }

        private void UpdateFishCounter() => OnUpdateFishCounter?.Invoke();
        public void WithDrawFish(Fish fish) => OnWithDrawFish?.Invoke();
        public void AppearFishCounter() => OnAppearFishCounter?.Invoke();
    }
}