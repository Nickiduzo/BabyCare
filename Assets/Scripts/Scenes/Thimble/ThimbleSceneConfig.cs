using UnityEngine;

namespace Thimble
{
    [CreateAssetMenu(fileName = "ThimbleLevel", menuName = "Configs/ThimbleLevel")]
    public class ThimbleSceneConfig : ScriptableObject
    {
        [SerializeField] private ThimbleCup _thimblecup;
        [SerializeField] private Magician _magician;
        
        public ThimbleCup thimblecup => _thimblecup;
        public Magician magician => _magician;
    }
}
