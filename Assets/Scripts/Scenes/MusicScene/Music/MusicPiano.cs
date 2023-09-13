using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;

public class Music : MonoBehaviour
{
    private SoundSystem _soundSystem;
    void Start()
    {
        _soundSystem = SoundSystemUser.Instance;
    }
    public void TakingSound()
    {
        _soundSystem.PlaySound("Taking");
    }
}
