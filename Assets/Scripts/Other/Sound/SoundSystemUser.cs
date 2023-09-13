using UnityEngine;

namespace Sound
{
    public class SoundSystemUser : MonoBehaviour
    {
        [SerializeField] private SoundSystem _soundSystem;
        
        #region Singleton

        public static SoundSystem Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = _soundSystem;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }
            //DontDestroyOnLoad(gameObject);
        }

        #endregion Singleton
    }
}