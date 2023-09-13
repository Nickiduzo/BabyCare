using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;
using UsefulComponents;

public class BodyController : MonoBehaviour, ITouchSystem
{ 
    public event Action<Vector3> OnTapped;

    private float timeToWait = 20;
    private BodyTouch bodyTouch;

    private Animator animator;
    private SoundSystem soundSystem;
    private bool hintSys = true;
    [SerializeField] private List<Transform> hintpoints;
    private void Start()
    {
        bodyTouch = GetComponent<BodyTouch>();
        soundSystem = SoundSystemUser.Instance;
        animator = GetComponent<Animator>();
        OnTapped += DisableHint;
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = CalculateTouchPosition();
            OnTapped?.Invoke(touchPosition);
        }

        if (timeToWait <= 0) ChooseWaitAnimation();
        else timeToWait -= Time.deltaTime;
    }

    public Vector2 CalculateTouchPosition()
    {
        Vector2 touchPosition = Input.mousePosition;

        return touchPosition;
    }

    private void ChooseWaitAnimation()
    {
        System.Random rand = new System.Random();

        if (!bodyTouch.amimationPlay())
        {
            switch (rand.Next(0, 3))
            {
                case 0:
                    PlayWaveAnimation();
                    timeToWait = 20;
                    break;
                case 2:
                    PlayWaitAnimation();
                    timeToWait = 20;
                    break;
                case 1:
                    PlayCLoudAnimation();      
                    timeToWait = 20;
                    break;
            }

        }
        if (hintpoints.Count > 0 && hintSys)
        {
            DisableHint(default(Vector3));
            ActivateHint(hintpoints[rand.Next(0, hintpoints.Count)].position);

        }

    }

    private void PlayWaitAnimation()
    {
        soundSystem.PlaySound("Yawing");
        animator.SetBool("isWaitAnimation", true);
    }
    private void PlayWaveAnimation() => animator.SetBool("isMakeWave", true);

    private void PlayCLoudAnimation()
    {
        animator.SetBool("isTaskCloud", true);
    }

    private void ActivateHint(Vector3 endPosition)
    {
        HintSystem.Instance.ShowPointerHint(endPosition, endPosition, 1);

    }
    private void DisableHint(Vector3 v)
    {
        HintSystem.Instance.HidePointerHint();
        hintSys = false;
    }
}
