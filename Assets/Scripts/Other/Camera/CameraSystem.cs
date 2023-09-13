using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    public class CameraSystem : MonoBehaviour, ICameraSystem
    {
        [SerializeField] private List<GameCamera> _cameras;

        public void ChangeCamera(GameCameraType type)
        {
            foreach (var gameCamera in _cameras)
            {
                gameCamera.Camera.Priority = 0;

                if (gameCamera.Type == type)
                    gameCamera.Camera.Priority = 1;
            }
        }
    }
}