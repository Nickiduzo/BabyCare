using UnityEngine;

namespace Quest
{
    public class QuestActor : MonoBehaviour
    {
        [SerializeField] private ShopperSpawner _shopperSpawner;
        [SerializeField] private Transform _shopperDestination;
        [SerializeField] private QuestLevelConfig _config;


        public void StartQuest()
        {
            Shopper shopper = GetShopper();
            MoveToDestinationAndSpawn(shopper);
        }

        // Moves shopper to destination and create random task
        private void MoveToDestinationAndSpawn(Shopper shopper)
            => shopper.MoveToAndSpawn(_shopperDestination.position);

        // invoke SpawnQuestShopper in ShopperSpawner
        private Shopper GetShopper()
            => _shopperSpawner.SpawnQuestShopper(_config);
    }
}