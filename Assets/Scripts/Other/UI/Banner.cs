using UnityEngine;

namespace UI
{
    public class Banner : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        public void SetScreenSpaceOverlay()
        {
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
        public void SetWorldSpace()
        {
            _canvas.renderMode = RenderMode.WorldSpace;
        }
    }
}

