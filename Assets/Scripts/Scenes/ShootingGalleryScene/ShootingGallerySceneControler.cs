using UnityEngine;

namespace ShootingGallery
{
    [CreateAssetMenu(fileName = "ShootingGalleryLevel", menuName = "Configs/ShootingGalleryLevel")]
    public class ShootingGallerySceneControler : ScriptableObject
    {
        [SerializeField] private Targets _targets;
        [SerializeField] private BallShoot _ballShoot;
        public Targets targets => _targets;
        public BallShoot ballShoot => _ballShoot;

    }
}
