using Sound;
using System.Collections;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private RandomSound sound;
    [SerializeField] private BalloonPool pool;
    [SerializeField] private ClockArrow timer;

    //Spawn balloons
    public Balloon[] SpawnBalloons(Balloon[] balloons)
    {
        for (int i = 0; i < balloons.Length; i++)
        {
            balloons[i] = pool.Pool[i].GetFreeElement();
            balloons[i].transform.position = GetRandomPosition();
            balloons[i].Construct(sound);
        }
        return balloons;
    }
    // Respawn one balloon by index
    public Balloon RespawnBalloon(Balloon[] balloons,int index)
    {
        Balloon balloon = balloons[index];
        balloon = pool.Pool[index].GetFreeElement();
        balloon.transform.position = GetRandomPosition();
        balloon.Construct(sound);
        return balloon;
    }
    //Respawn Balloons
    public Balloon[] RespawnBalloons(Balloon[] balloons)
    {
        for (int i = 0; i < balloons.Length; i++)
        {
            if (IsNotActive(balloons[i]))
            {
                balloons[i] = pool.Pool[i].GetFreeElement();
                balloons[i].transform.position = GetRandomPosition();
                balloons[i].Construct(sound);
            }
        }
        return balloons;
    }

    //Stop each balloon
    public void StopBalloon(Balloon[] mainBalloons)
    {
        StartCoroutine(DestroyDelay(mainBalloons));
    }
    //Destroy balloon after end, one-by-one
    private IEnumerator DestroyDelay(Balloon[] balloons)
    {
        foreach (Balloon balloon in balloons)
        {
            if (!balloon.gameObject.activeSelf)
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                balloon.DestroyBalloon();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    private Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(-11f, 11f), Random.Range(-24f, -5.5f));
    }
    private bool IsNotActive(Balloon balloon)
    {
        return !balloon.gameObject.activeSelf;
    }
}
