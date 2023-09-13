using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulComponents;
using Random = UnityEngine.Random;

public class HintController : MonoBehaviour
{
    public event Action OnHint;

    [SerializeField] public List<BaseToyView> _toys;
    [SerializeField] public ChildView _child;
    [SerializeField, Range(5f, 20f)] float _timeToWaitForHint = 10f;

    public bool IsActive { get; set; } = false;

    #region Singleton
    public static HintController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }
    #endregion Singleton

    private void Start()
    {
        StartCoroutine(HintWaiter());
    }

    /// <summary>
    /// Selects a toy and waits a serialized time before the Hint appears
    /// </summary>
    IEnumerator HintWaiter()
    {
        while (true)
        {
            if (IsActive)
            {
                yield return null;
                continue;
            }

            float remainingTime = _timeToWaitForHint;

            while (remainingTime > 0)
            {
                yield return null;

                if (ToyService.IsPlaying)
                {
                    remainingTime = _timeToWaitForHint; // Reset the remaining time
                }
                else
                {
                    remainingTime -= Time.deltaTime; // Decrease the remaining time
                }
            }

            // At this point, the waiting period has elapsed
            if (!ToyService.IsPlaying)
            {
                var toy = _toys[Random.Range(0, _toys.Count)];

                if (toy.KindOfToy == Toys.Ball || toy.KindOfToy == Toys.Beanbag)
                    HintSystem.Instance.ShowPointerHint(toy.transform.position, _child.transform.position);
                else
                    HintSystem.Instance.ShowPointerHint(toy.transform.position);

                toy.ActivateHint();
                OnHint.Invoke();
            }
        }
    }
}
