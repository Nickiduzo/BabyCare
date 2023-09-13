using System;
//using UnityEditor.PackageManager;
using UnityEngine;

public class AnimationChecker:MonoBehaviour
{
    #region Singleton

    public static AnimationChecker Instance { get; private set; }

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

    public bool IsAnimationOver(Animator animator, string clipName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        try
        {
            // Перевірка, чи кліп закінчився
            if (stateInfo.IsName(clipName) && stateInfo.normalizedTime >= 1.0f)
                return true;

            //return false;
        }
        catch (NullReferenceException e) {
            Debug.Log(e);
        }

        return false;
    }
}
