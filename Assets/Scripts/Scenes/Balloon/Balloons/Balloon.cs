using UnityEngine;
using Sound;
using System;
using System.Collections;

public class Balloon : MonoBehaviour
{
    [SerializeField] private GameObject particleEffect;
    public event Action OnTapped;

    private ISound sound;

    private float speedOfBalloon = 1.8f;
    private int counter = 0;
    private bool isIncrease = true;

    [Header("Uppear destroy value")]
    public int uppearPosition = 7;
    public void Construct(ISound sound)
    {
        this.sound = sound ?? throw new ArgumentNullException(nameof(sound));
    }
    // Controll moving and speed of balloon each frame
    private void Update()
    {
        counter = PointCounter.TakeDataScore();

        if (ShouldIncreaseSpeed())
        {
            IncreaseSpeed();
            isIncrease = false;
        }

        if (counter % 15 != 0) isIncrease = true; // each 15 balloons increase speed

        transform.Translate(Vector2.up * speedOfBalloon * Time.deltaTime);
        
        if (gameObject.transform.position.y >= uppearPosition) gameObject.SetActive(false);
    }
    //By click on mouse destroy balloon and increase point
    private void OnMouseDown()
    {
        PointCounter.IncreasePoint();
        DestroyBalloon();
        OnTapped?.Invoke();
    }
    //Destroy balloon
    public void DestroyBalloon()
    {
        SpawnEffect();
        gameObject.SetActive(false);
        sound.Play();
    }
    //Spawn destroy effect
    private void SpawnEffect()
    {
        Instantiate(particleEffect, transform.position, Quaternion.identity);
    }
    // Add 10% to speed of balloon
    private void IncreaseSpeed()
    {
        speedOfBalloon += speedOfBalloon * 0.1f;
    }
    // Check that destroyed 15 balloons to increase speed
    private bool ShouldIncreaseSpeed()
    {
        return counter != 0 && counter % 15 == 0 && isIncrease;
    }
}
