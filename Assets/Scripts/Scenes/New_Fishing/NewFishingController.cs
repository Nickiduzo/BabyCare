using System.Collections.Generic;
using UnityEngine;

namespace NewFishing
{
    [CreateAssetMenu(fileName = "NewFishingLevel", menuName = "Configs/NewFishingLevel")]
    public class NewFishingController : ScriptableObject
    {
        [SerializeField] private FishingRod _fishingRod;
        [SerializeField] private FishConstracror _fish;
        [SerializeField] private Bucket _bucket;
        [SerializeField] private FishSide _fishSide;
        public FishingRod fishingRod => _fishingRod;
        public FishConstracror fish => _fish;
        public Bucket bucket => _bucket;
        public FishSide fishSide => _fishSide;
    }
}
