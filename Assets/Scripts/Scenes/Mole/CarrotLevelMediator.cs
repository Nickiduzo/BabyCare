using Mole.Spawners;
using Mole.Config;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UI;
using Sound;
using UnityEngine;
using UsefulComponents;

namespace Mole
{
    public class CarrotLevelMediator : MonoBehaviour
    {
        [SerializeField] private CarrotLevelConfig _config;
        [SerializeField] private CarrotHolesContainer _holesContainer;
        [SerializeField] private MoleSpawnController _moleSpawn;
        [SerializeField] private CameraEffects _cameraEffects;
        [SerializeField] private List<EnvironmentAnimController> _environments;
        [SerializeField] private ActorUI _actorUI;
        [SerializeField] private TimerView _timer;
        [SerializeField] private WinPannel _winPannel;

        // initialize progress bar in [ActroUI], launch mole spawning, monitor whether all moles have been caught
        private void Start()
        {
            _actorUI.InitWinInvoker(_timer);
            _winPannel.OnAppear.AddListener(() => HummerSystem.Instance.IsValid = false);

            _moleSpawn.StartMoleSpawning();
            _moleSpawn.OnMoleCaught.AddListener((ICollectable item) => _actorUI.Hud.AddScore(item));
            _moleSpawn.OnBaloonCaught.AddListener((int time) => _actorUI.Hud.IncreaseTime(time));
            _moleSpawn.OnBombCaught.AddListener((int time) => _actorUI.Hud.DecreaseTime(time));

            foreach (var item in _environments)
                _cameraEffects.OnShake.AddListener(item.RotateForDuration);
        }

        private void DisableHint()
            => HintSystem.Instance.HidePointerHint();

        // check if all holes have been filled with water, if so, return "true"
        private bool IsHolesFull()
            => _holesContainer.HolesOnScene.All(hole => hole.IsFull);

        private void ActivateHint(Vector3 startPosition, Vector3 endPosition)
            => HintSystem.Instance.ShowPointerHint(startPosition, endPosition);
    }
}