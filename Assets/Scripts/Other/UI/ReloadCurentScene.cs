using Quest;
using Scene;
using UnityEngine;

namespace UI
{
    public class ReloadCurentScene : HudElement
    {
        [SerializeField] private SceneLoader _sceneLoader;

        private new void Start()
        {
            base.Start();
            Appear();
        }


        public void ReloadScene()
        {
            Click();
            EndCycle();
            _sceneLoader.ReloadCurrentScene();

        }

        private void EndCycle()
        {
            Disappear();
        }
    }
}
