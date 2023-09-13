using UnityEngine;

namespace Bath
{

    public class Foam : MonoBehaviour
    {
        [SerializeField] Animator _animator;

        public void StartFoaming()
        {
            gameObject.SetActive(false);
            _animator.SetTrigger("Foam");
            Invoke("DeactivateFoam", 0.8f);
        }
        private void DeactivateFoam()
        {
            gameObject.SetActive(false);
        }
    }
}
