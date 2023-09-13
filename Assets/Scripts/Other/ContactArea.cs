using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ContactArea : MonoBehaviour
{
    [SerializeField] private bool DontDestroyOnLoad;

    public static ContactArea Instance { get; set; }

    private GameObject objectToDestroy;

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

        if (DontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void WaitingForEndLifecycle(GameObject objectToDestroy, float timeToDestruction)
    {
        this.objectToDestroy = objectToDestroy;
        if(timeToDestruction != 0)
        {
            StartCoroutine(CalculateLifetime(timeToDestruction));
        }
    }

    private IEnumerator CalculateLifetime(float timeToDestruction)
    {
        Debug.Log("CalculateLifetime");
        yield return new WaitForSeconds(timeToDestruction);
        Destroy(objectToDestroy);
    }

    public void DestroyNow()
    {
        Destroy(objectToDestroy);
    }
    
    // use Tweens below for move seeds to hole, invoke from seed script 
    public Tween MoveSeedToHole(Vector3 Point, Transform obj, float duration)
        => obj.DOMove(Point, duration);
    public Tween AdditionalMovementToHole(Vector3 Point, Transform obj, float duration)
        => obj.DOMove(Point, duration);
}
