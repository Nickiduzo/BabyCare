using UnityEngine;
using UI;

namespace Thimble
{
    public class ThimbleSceneMediator : MonoBehaviour
    {
        [SerializeField] private ThimbleCupSpawn _cupSpawner;
        [SerializeField] private MagicianSpawn _magicianSpawner;
        [SerializeField] private WinPannel _win;

        private void Start()
        {
            Magician magician = _magicianSpawner.SpawnMagician(_cupSpawner.SpawnCups());
            magician.OnEndGame += _win.Appear;
        }
    }
}