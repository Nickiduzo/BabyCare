using DG.Tweening;
using Quest;
using Scene;
using UnityEngine;

namespace Finish
{
    public class FinishActor : MonoBehaviour
    {
        [SerializeField] private ShopperSpawner _shopperSpawner;
        [SerializeField] private Transform _shoperDestination;
        [SerializeField] private QuestActor _questActor;
        [SerializeField] private QuestLevelConfig _config;

        private Shopper _finishShopper;
        private const string previousScene = "previousScene";
        private const float START_INTERNAL_TIME = 4.6f;
        private const float LEVEL_NOT_COMPLIED_INTERNAL_TIME = 0f;


        // invoke ShopperSpawner, depending on which scene we got to "QuestScene" from
        private void Start()
        {
            string prevScene = PlayerPrefs.GetString(previousScene, "Carrot");
            if (prevScene == "MainScene")
            {
                LoadQuestShopper();
                return;
            }

            float internalTime = SceneLoader.IsLevelComplied ? START_INTERNAL_TIME : LEVEL_NOT_COMPLIED_INTERNAL_TIME;
            InitFinishShopper(internalTime);
        }

        // if we got to "QuestScene" from other scenes except for MainScene
        public void InitFinishShopper(float intervalTimeCount)
        {
            if (_finishShopper != null)
                return;

            _finishShopper = CreateFinishShopper(_config);

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(intervalTimeCount);
            sequence.Append(_finishShopper.MoveTo(_shoperDestination.position));
            sequence.AppendCallback(LoadQuestShopper);
        }

        // spawn shopper without task
        private Shopper CreateFinishShopper(QuestLevelConfig _config)
        {
            Shopper shopper = _shopperSpawner.SpawnFinishShopper(_config);
            return shopper;
        }
        
        // if we got to "QuestScene" from "MainScene", spawn shopper with task
        private void LoadQuestShopper()
        {
            _questActor.StartQuest();
            Destroy(_finishShopper?.gameObject);
            _finishShopper = null;
        }
    }
}