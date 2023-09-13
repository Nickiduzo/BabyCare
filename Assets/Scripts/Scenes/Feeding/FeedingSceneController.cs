using UnityEngine;

namespace Feeding
{

    [CreateAssetMenu(fileName = "FeedingLevel", menuName = "Configs/FeedingLevel")]
    public class FeedingSceneController : ScriptableObject
    {
        //Feeding scene prefab container 
        [SerializeField] private Bottle _bottle;
        [SerializeField] private Spoon _spoon;
        [SerializeField] private Porridge _porridge;
        [SerializeField] private Cookies _cookie;
        [SerializeField] private Apple _apple;
        [SerializeField] private EatZone _eatZone;
        [SerializeField] private BabyFeeding _baby;
        [SerializeField] private Napkin _napkin;
        [SerializeField] private Blobs _blobs;



        public Bottle bottle => _bottle;
        public Apple apple => _apple;
        public Porridge porridge => _porridge;
        public Spoon spoon => _spoon;
        public Cookies cookie => _cookie;
        public EatZone eatZone => _eatZone;
        public BabyFeeding baby => _baby;
        public Napkin napkin => _napkin;
        public Blobs blobs => _blobs;

    }
}
