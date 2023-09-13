using UnityEngine;

public class MiniMusicMediator : MonoBehaviour
{
    [SerializeField] private PianoPlayerSpawner pianoPlayerSpawner;
    [SerializeField] private BigCowSpawner bigCowSpawner;
    [SerializeField] private OrangeCatSpawner orangeCatSpawner;
    [SerializeField] private GreenDuckSpawner greenDuckSpawner;
    [SerializeField] private PinkPigSpawner pinkPigSpawner;
    [SerializeField] private WhiteSheepSpawner whiteSheepSpawner;
    [SerializeField] private HourseSpawner hourseSpawner;
    [SerializeField] private YellowChickenSpawner yellowChickenSpawner;
    [SerializeField] private RedChickenSpawner redChickenSpawner;
    [SerializeField] private PuppySpawner puppySpawner;
    [SerializeField] private MusicCloudSpawner cloudSpawner;

    private Cloud[] clouds;
    private void Start()
    {
        PianoPlayer pianoPlayer = SpawnPianoPlayer();
        BigCow bigCow = SpawnCow();
        OrangeCat orangeCat = SpawnCat();
        GreenDuck greenDuck = SpawnDuck();
        PinkPig pinkPig = SpawnPig();
        WhiteSheep whiteSheep = SpawnSheep();
        Hourse hourse = SpawnHourse();
        YellowChicken yellowChicken = SpawnYellowChicken();
        RedChicken redChicken = SpawnRedChicken();
        Puppy puppy = SpawnPuppy();
        clouds = SpawnCloud();
    }
    private void Update()
    {
        cloudSpawner.RespawnClouds(clouds);
    }
    private Cloud[] SpawnCloud()
    {
        Cloud[] clouds = new Cloud[6];
        clouds = cloudSpawner.SpawnCloud(clouds);
        return clouds;
    }
    private PianoPlayer SpawnPianoPlayer()
    {
        PianoPlayer pianoPlayer = pianoPlayerSpawner.SpawnPlayer();
        return pianoPlayer;
    }
    private BigCow SpawnCow()
    {
        BigCow bigCow = bigCowSpawner.SpawnBigCow();
        return bigCow;
    }
    private OrangeCat SpawnCat()
    {
        OrangeCat orangeCat = orangeCatSpawner.SpawnCat();
        return orangeCat;
    }
    private GreenDuck SpawnDuck()
    {
        GreenDuck greenDuck = greenDuckSpawner.SpawnGreenDuck();
        return greenDuck;
    }
    private PinkPig SpawnPig()
    {
        PinkPig pinkPig = pinkPigSpawner.SpawnPinkPig();
        return pinkPig;
    }
    private WhiteSheep SpawnSheep()
    {
        WhiteSheep whiteSheep = whiteSheepSpawner.SpawnSheep();
        return whiteSheep;
    }
    private Hourse SpawnHourse()
    {
        Hourse hourse = hourseSpawner.SpawnHourse();
        return hourse;
    }
    private YellowChicken SpawnYellowChicken()
    {
        YellowChicken yellowChicken = yellowChickenSpawner.SpawnChicken();
        return yellowChicken;
    }
    private RedChicken SpawnRedChicken()
    {
        RedChicken redChicken = redChickenSpawner.SpawnRedChicken();
        return redChicken;
    }
    private Puppy SpawnPuppy()
    {
        Puppy puppy = puppySpawner.SpawnPuppy();
        return puppy;
    }
}
