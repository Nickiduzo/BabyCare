using UnityEngine;

namespace Dream
{
    [CreateAssetMenu(fileName = "DreamingLevel", menuName = "Configs/DreamingLevel")]
    public class DreamSceneConfig : ScriptableObject
    {
        [SerializeField] private Baby _baby;
        [SerializeField] private Blanket _blanket;
        [SerializeField] private Lamp _lamp;
        [SerializeField] private ToyWheel _toyWheel;

        public Baby Baby => _baby;
        public Lamp Lamp => _lamp;
        public Blanket Blanket => _blanket;
        public ToyWheel ToyWheel => _toyWheel;
    }
}
