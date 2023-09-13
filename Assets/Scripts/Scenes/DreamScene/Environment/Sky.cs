using UnityEngine;

namespace Dream
{
    public class Sky : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _stars;

        //Playing Particle System of stars during the night time.
        private void Start()
        {
            _stars.Play();
        }

        //Stop particle playing, when Sunrise
        public void Sunrise()
        {
            _stars.Stop();
        }
    }
}

