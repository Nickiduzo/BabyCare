using UnityEngine;

namespace Quest
{
    public class ShopperSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _questShopperSpawnPoint;
        [SerializeField] private Transform _finishShopperSpawnPoint;
        [SerializeField] private Transform _basketSpawnPoint;
        [SerializeField] private Transform _basketDestinationPoint;
        [SerializeField] private Shopper _shopper;
        [SerializeField] private TaskSpawner _taskSpawner;
        [SerializeField] private ShopperController _shopperController;

        private const string Shopper = "shopper";
        private int shopperID;

        // uses when need to spawn shopper with task
        public Shopper SpawnQuestShopper(QuestLevelConfig _config)
        {
            shopperID = GetRandomShopper();
            PlayerPrefs.SetInt(Shopper, shopperID);

            Shopper shopper = SpawnShopper(_shopper, _questShopperSpawnPoint.position);
            _shopperController.SetShopper(shopper);
            shopper.Construct(_taskSpawner, shopperID);
            shopper.SetRandomAnimal(_config);
            return shopper;
        }

        // uses when need to spawn shopper without task and he will wait until all products are fully loaded in car
        public Shopper SpawnFinishShopper(QuestLevelConfig _config)
        {
            if (_shopperController.IsShopperExist)
                return _shopperController.GetShopper();

            shopperID = PlayerPrefs.GetInt(Shopper, 1);
            Shopper shopper = SpawnShopper(_shopper, _finishShopperSpawnPoint.position);
            _shopperController.SetShopper(shopper);
            shopper.SetSkin(shopperID);
            shopper.GetAnimal(_config);
            shopper.SetProduct(_config.sceneType, _basketSpawnPoint, _basketDestinationPoint);
            return shopper;
        }

        // create random car skin
        private int GetRandomShopper()
            => Random.Range(0, _shopper.SkinsCount);

        // create shopper
        private Shopper SpawnShopper(Shopper toSpawn, Vector3 position)
            => Instantiate(toSpawn, position, toSpawn.transform.rotation);

    }
}