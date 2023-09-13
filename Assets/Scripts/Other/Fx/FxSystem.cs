using System;
using System.Collections.Generic;
using UnityEngine;

public class FxSystem : MonoBehaviour
{
    [SerializeField] private Particle[] _particles;
    private Dictionary<string, ParticleSystem> _particleSystems = new Dictionary<string, ParticleSystem>();

    #region Singleton

    public static FxSystem Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    #endregion Singleton

    public void PlayEffect(string name, Vector3 position, Transform parent = null)
    {
        Particle currentParticle = GetParticleByName(name);
        ParticleSystem effectGO = Instantiate(currentParticle.Effect, position, Quaternion.identity);
        if (effectGO != null)
        {
            if (parent != null)
            {
                SetParent(effectGO, parent);
            }

            _particleSystems[name] = effectGO;
        }
    }

    public void StopEffect(string name)
    {
        if (_particleSystems.ContainsKey(name))
        {
            ParticleSystem particleSystem = _particleSystems[name];
            particleSystem.Stop();
            _particleSystems.Remove(name);
        }
    }

    /// <summary>
    /// u can call method by index when u know index of element, and dont want to checking name of FX by string
    /// </summary>
    /// <param name="index"></param>
    /// <param name="position"></param>
    public void PlayEffect(int index, Vector3 position, Transform parent = null)
    {
        Particle particleSystemEffect = GetParticleByIndex(index);
        ParticleSystem particleSystemGO = Instantiate(particleSystemEffect.Effect, position, Quaternion.identity);
        if (particleSystemGO != null)
        {
            if (parent != null)
            {
                SetParent(particleSystemGO, parent);
            }
            _particleSystems[GetNameByIndex(index)] = particleSystemGO;
        }
    }

    private void SetParent(ParticleSystem child, Transform parent)
        => child.gameObject.transform.parent = parent;

    public int GetCountParticles()
        => _particleSystems.Count;

    private Particle GetParticleByName(string name)
            => Array.Find(_particles, particle => particle.Name == name);

    private Particle GetParticleByIndex(int index)
        => _particles[index];

    private string GetNameByIndex(int index)
        => _particles[index].Name;
}
