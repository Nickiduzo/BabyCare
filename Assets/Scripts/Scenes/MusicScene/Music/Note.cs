using Sound;
using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    public delegate void ButtonPressedDelegate();
    public event ButtonPressedDelegate OnButtonPressed;

    private Animator animator;
    private SoundSystem soundSystem;

    private void Start()
    { 
        animator = GetComponent<Animator>();
        soundSystem = SoundSystemUser.Instance;
    }
    public void PlayAnimSound(string name)
    {
        animator.SetTrigger(name);
        soundSystem.PlaySound(name);

        OnButtonPressed?.Invoke();
    }
}
