using UnityEngine;

public class PuzzleParticleController : MonoBehaviour
{
    #region Singleton

    public static PuzzleParticleController Instance { get; private set; }

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

    [SerializeField] private ParticleSystem _successParticleSystemPrefab;
    [SerializeField] private ParticleSystem _collectedParticleSystemPrefab;

    public void PlaySuccess(Vector3 position)
    {
        Instantiate(_successParticleSystemPrefab, position, Quaternion.identity).Play();
    }

    public void PlayCollectedPuzzle()
    {
        Instantiate(_collectedParticleSystemPrefab, Vector3.zero, Quaternion.identity).Play();
    }
}