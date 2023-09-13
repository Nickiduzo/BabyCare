using UnityEngine;
using System.Linq;
using UI;

public class BalloonMediator : MonoBehaviour
{
    [SerializeField] private ActorUI actorUiBalloon;
    [SerializeField] private BalloonSpawner ballSpawner;
    [SerializeField] private CloudSpawner cloudSpawner;
    [SerializeField] private ClockArrow timer;
    private Balloon[] balloons;
    private Cloud[] clouds;

    private bool gameOver;
    // Start on first frame, initialize balloons, inititalize winPanel
    private void Start()
    {
        balloons = SpawnBalloon();
        GetSubscribe(balloons);
        clouds = SpawnCloud();
        timer.OnWin += StopGame;
        actorUiBalloon.InitWinInvoker(timer);
    }
    // Work each frame, has check on stop game or no, also check on respawn balloons
    private void Update()
    {
        if (gameOver)
        {
            ballSpawner.StopBalloon(balloons);
        }
        else 
        {
            ballSpawner.RespawnBalloons(balloons);
        }
        cloudSpawner.RespawnClouds(clouds);
    }
    private Balloon[] SpawnBalloon()
    {
        Balloon[] balls = new Balloon[20];
        balls = ballSpawner.SpawnBalloons(balls);
        return balls;
    }
    private Cloud[] SpawnCloud()
    {
        Cloud[] clouds = new Cloud[6];
        clouds = cloudSpawner.SpawnCloud(clouds);
        return clouds;
    }
    // Take Subscribe of method to find each balloon has active/unactive
    private void GetSubscribe(Balloon[] balloons)
    {
        for (int i = 0; i < balloons.Length; i++)
        {
            balloons[i].OnTapped += FindOffBalloon;
        }
    }
    // Search balloon with not active in hierarchy
    private void FindOffBalloon()
    {
        var disabledObject = balloons.Select((obj, index) => new { Object = obj, Index = index }).FirstOrDefault(item => item.Object == null || !item.Object.gameObject.activeSelf);
        RespawnBalloon(disabledObject.Index);
    }
    // Respawn balloon by index
    private void RespawnBalloon(int index)
    {
        ballSpawner.RespawnBalloon(balloons,index);
    }

    private void StopGame()
    {
        gameOver = true;
    }
}
