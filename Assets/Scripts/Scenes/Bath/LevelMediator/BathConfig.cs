using UnityEngine;

namespace Bath
{
    [CreateAssetMenu(fileName = "BathLevel", menuName = "Configs/BathLevel")]
    public class BathConfig : ScriptableObject
    {
        [SerializeField] private Baby _baby;
        [SerializeField] private Toy _pistol;
        [SerializeField] private Toy _sub;
        [SerializeField] private Sponge _sponge;
        [SerializeField] private Duck _duck;
        [SerializeField] private BubbleStick _stick;
        [SerializeField] private Tube _tube;

        public Baby Baby => _baby;
        public Toy Pistol => _pistol;
        public Toy Sub => _sub;
        public Sponge Sponge => _sponge;
        public Duck Duck => _duck;
        public BubbleStick Stick => _stick;
        public Tube Tube => _tube;
    }
}
