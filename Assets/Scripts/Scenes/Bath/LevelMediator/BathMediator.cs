using UnityEngine;

namespace Bath
{
    public class BathMediator : MonoBehaviour
    {
        [SerializeField] private BabySpawner _babySpawner;
        [SerializeField] private PistolSpawner _pistolSpawner;
        [SerializeField] private SubSpawner _subSpawner;
        [SerializeField] private SpongeSpawner _spongeSpawner;
        [SerializeField] private DuckSpawner _duckSpawner;
        [SerializeField] private StickSpawner _stickSpawner;
        [SerializeField] private TubeSpawner _tubeSpawner;
        [SerializeField] private BubbleSpawner _bubbleSpawner;

        //Start on first frame and spawn prefabs
        private void Start()
        {
            Baby baby = SpawnBaby();
            SpawnPistol(baby);
            SpawnSub(baby);
            SpawnSponge(baby);
            SpawnDuck();
            SpawnStick();
            SpawnTube(baby);
        }

        private Baby SpawnBaby()
        {
            Baby baby = _babySpawner.SpawnBaby();
            return baby;
        }

        private void SpawnPistol(Baby baby)
        {
            Toy pistol = _pistolSpawner.SpawnPistol();
            pistol.OnGiveBabyAToy += baby.PlayWithPistol;
        }

        private void SpawnSub(Baby baby)
        {
            Toy sub = _subSpawner.SpawnSub();
            sub.OnGiveBabyAToy += baby.PlayWithSubmarine;
        }

        private void SpawnSponge(Baby baby)
        {
            Sponge sponge = _spongeSpawner.SpawnSponge(_bubbleSpawner);
            sponge.OnDrugStart += baby.StartSpongeBathing;
            sponge.OnBubbleBaby += baby.SpongeBathing;
            sponge.OnDrugEnded += baby.StopSpongeBathing;
        }

        private void SpawnDuck()
        {
            Duck duck = _duckSpawner.SpawnDuck();
        }

        private void SpawnStick()
        {
            BubbleStick stick = _stickSpawner.SpawnStick();
        }

        //Spawn Tude and subscribe on event
        private void SpawnTube(Baby baby)
        {
            Tube tube = _tubeSpawner.SpawnTube();
            tube.OnWashingBaby += _bubbleSpawner.DestroyBubbles;
            tube.OnWashingBaby += baby.Bathing;

            tube.OnStopWashingBaby += baby.StopBathing;
            tube.OnStopWashingBaby += _bubbleSpawner.StopDestroyingBubbles;
        }
    }
}