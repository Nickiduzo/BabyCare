using Inputs;
using Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene
{
    public class PlaySceneMediator : MonoBehaviour
    {
        [SerializeField] private ChildSpawn _SpawnChild;
        [SerializeField] private BallSpawn _SpawnBall;
        [SerializeField] private BeanbagSpawn _SpawnBeanbag;
        [SerializeField] private ConsoleSpawn _SpawnConsole;
        [SerializeField] private DinoSpawn _SpawnDino;
        [SerializeField] private HorseSpawn _SpawnHorse;
        [SerializeField] private RobotSpawn _SpawnRobot;

        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private HintController _HintController;
        [SerializeField] private SceneLoader _sceneLoader;

        private Transform _ParentPart;
        private Transform _ParentPartHand;
        private ChildView _ChildView;

        private Ball SpawnBall()
        {
            Ball ball = _SpawnBall.SpawnBall();
            ball._ParentPart = _ParentPart;
            ball._Child = _ChildView;
            ball._BaseToyView.mediator = this;
            ball.SetValue();
            _HintController._toys.Add(ball._BaseToyView);
            return ball;

        }

        private Beanbag SpawnBeanbag()
        {
            Beanbag beanbag = _SpawnBeanbag.SpawnBeanbag();
            beanbag._ParentPart = _ParentPartHand;
            beanbag._Child = _ChildView;
            beanbag._BaseToyView.mediator = this;
            beanbag.SetValue();
            _HintController._toys.Add(beanbag._BaseToyView);
            return beanbag;

        }

        private Console SpawnConsole()
        {
            Console console = _SpawnConsole.SpawnConsole();
            console._ParentPart = _ParentPart;
            console._Child = _ChildView;
            console._sceneLoader = _sceneLoader;
            console._BaseToyView.mediator = this;
            console.SetValue();
            _HintController._toys.Add(console._BaseToyView);
            return console;

        }

        private Child SpawnChild()
        {
            Child child = _SpawnChild.SpawnChild();
            _ParentPart = child._ToyparentPart;
            _ChildView = child._ChildView;
            _ParentPartHand = child._ToyparentPartHand;

            _HintController._child = child._ChildView;
            return child;

        }
        private Dino SpawnDino()
        {
            Dino dino = _SpawnDino.SpawnDino();
            dino._inputSystem = _inputSystem;
            dino._Child = _ChildView;
            dino._BaseToyView.mediator = this;
            dino.SetValue();
            _HintController._toys.Add(dino._BaseToyView);
            return dino;

        }
        private Horse SpawnHorse()
        {
            Horse horse = _SpawnHorse.SpawnHorse();
            horse._inputSystem = _inputSystem;
            horse._Child = _ChildView;
            horse._BaseToyView.mediator = this;
            horse.SetValue();
            _HintController._toys.Add(horse._BaseToyView);
            return horse;

        }
        private Robot SpawnRobot()
        {
            Robot robot = _SpawnRobot.SpawnRobot();
            robot._inputSystem = _inputSystem;
            robot._Child = _ChildView;
            robot._BaseToyView.mediator = this;
            robot.SetValue();
            _HintController._toys.Add(robot._BaseToyView);
            return robot;

        }
        public void MakeAllInteractable()
        {
            foreach (BaseToyView t in _HintController._toys)
            {
                t.MakeInteractable();
            }
        }

        public void MakeAllNonInteractable()
        {
            foreach (BaseToyView t in _HintController._toys)
            {
                t.MakeNonInteractable();
            }
        }
        public void MakeAllIndependetNonInteractable()
        {
            foreach (BaseToyView t in _HintController._toys)
            {
                if (t.TryGetComponent(out IndependentToyView view))
                t.MakeNonInteractable();
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            SpawnChild();
            SpawnBall();
            SpawnBeanbag();
            SpawnConsole();
            SpawnDino();
            SpawnHorse();
            SpawnRobot();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
