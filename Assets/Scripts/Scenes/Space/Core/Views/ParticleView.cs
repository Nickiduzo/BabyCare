using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleView : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    public ParticleSystem ParticleSystem => _particleSystem;

    private void Update()
    {
        if(!_particleSystem.isPlaying)
            Destroy(gameObject);
    }
}
