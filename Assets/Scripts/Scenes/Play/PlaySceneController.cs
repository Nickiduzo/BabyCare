using UnityEngine;

namespace PlayScene
{
    [CreateAssetMenu(fileName = "PlaySceneLevel", menuName = "Configs/PlaySceneLevel")]
    public class PlaySceneController : ScriptableObject
    {
        [SerializeField] private Child _child;
        [SerializeField] private Ball _ball;
        [SerializeField] private Beanbag _beanbag;
        [SerializeField] private Console _console;
        [SerializeField] private Dino _dino;
        [SerializeField] private Horse _hourse;
        [SerializeField] private Robot _robot;
        public Child child => _child;
        public Ball ball => _ball;
        public Beanbag beanbag => _beanbag;
        public Console console => _console;
        public Dino dino => _dino;
        public Horse hourse => _hourse;
        public Robot robot => _robot;

    }
}
