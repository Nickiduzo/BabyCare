using Inputs;
using Quest;
using Scene;
using Sound;
using UnityEngine;

namespace Dream
{
    public class DreamSceneMediator : MonoBehaviour
    {
        [SerializeField] private BlanketSpawner _blanketSpawner;

        [SerializeField] private LampSpawner _lampSpawner;

        [SerializeField] private ToyWheelSpawner _toyWheelSpawner;

        [SerializeField] private BabySpawner _babySpawner;

        [SerializeField] private Sky _sky;
        [SerializeField] private TimeCycle _timeCycle;

        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private SceneLoader _sceneLoader;

        private void Start()
        {
            Baby baby = SpawnBaby();

            SpawnBlanket(baby);
            SpawnLamp(baby);
            SpawnToyWheel(baby);
        }

        //Spawns baby and returns baby. Sign baby's events. 
        private Baby SpawnBaby()
        {
            Baby baby = _babySpawner.SpawnBaby();

            baby.OnSunRise += _timeCycle.BabyAwake;
            baby.OnSunRise += _sky.Sunrise;
            baby.OnLvlEnd += LvlEnd;

            return baby;
        }

        //Create blanket and sign its events
        private void SpawnBlanket(Baby baby)
        {
            Blanket blanket = _blanketSpawner.SpawnBlanket();
            blanket.OnCoveredBaby += baby.ChangeBlanket;
            blanket.OnCoveredBaby += baby.CheckStates;
        }

        //Create lamp and sign its events
        private void SpawnLamp(Baby baby)
        {
            Lamp lamp = _lampSpawner.SpawnLamp();
            lamp.OnTurnLampForBaby += baby.ChangeLight;
            lamp.OnTurnLampForBaby += baby.CheckStates;
            lamp.OnTurningOffLight += _timeCycle.TurnOff;

            if (lamp.TryGetComponent(out SpriteRenderer spriteRendererOfLamp))
                _timeCycle.AddElementsForChangingColor(spriteRendererOfLamp);
        }

        //Create toy wheel and sign its events
        private void SpawnToyWheel(Baby baby)
        {
            ToyWheel toyWheel = _toyWheelSpawner.SpawnToyWheel();
            toyWheel.OnTurnOn += baby.ChangeToyWheel;
            toyWheel.OnTurnOn += baby.CheckStates;

            if (toyWheel.TryGetComponent(out SpriteRenderer spriteRendererOfTW))
                _timeCycle.AddElementsForChangingColor(spriteRendererOfTW);
        }

        //Loads Quest Scene
        private void LvlEnd()
        {
            _sceneLoader.LoadScene(SceneType.Quest);
        }
    }
}