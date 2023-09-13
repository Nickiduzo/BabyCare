using System.Collections.Generic;
using UnityEngine;

namespace Mole.Config
{
    [CreateAssetMenu(fileName = "CarrotLevel", menuName = "Configs/CarrotLevel")]
    public class CarrotLevelConfig : ScriptableObject
    {
        [SerializeField] private BasketPrefab _basket;
        [SerializeField] private Carrot _carrot;
        [SerializeField] private List<BaseHoleable> _prefabs;
        [SerializeField] private FruitSplash _splash;
        [SerializeField] private WaterPump _waterPump;
        [SerializeField] private SeedPackage _seedPackage;
        [SerializeField] private Seed _seed;
        [SerializeField] private int _maxMoleToSpawn;
        [SerializeField] private int _maxSplashToSpawn;
        [SerializeField] private int _catchMoleToWin;

        public List<BaseHoleable> Prefabs => _prefabs;
        public FruitSplash Splash => _splash;
        public Carrot Carrot => _carrot;
        public WaterPump WaterPump => _waterPump;
        public SeedPackage SeedPackage => _seedPackage;
        public int MaxMoleToSpawn => _maxMoleToSpawn;
        public int MaxSplashToSpawn => _maxSplashToSpawn;
        public Seed Seed => _seed;
        public BasketPrefab BasketPrefab => _basket;

        public int CatchMoleToWin => _catchMoleToWin;
    }
}
