using System.Diagnostics;
using UnityEngine;

public class StarsEffect : MonoBehaviour
{
    [SerializeField] private Color[] particleColors;
    
    private void Start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        particleSystem.startColor = particleColors[Random.Range(0,particleColors.Length)];
    }
}
