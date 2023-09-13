using UnityEngine;

public class SelfDestroying : MonoBehaviour
{
    [SerializeField] private float _delay;

    private void Awake()
      => Destroy(gameObject, _delay);
}