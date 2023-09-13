using UnityEngine;

[CreateAssetMenu(fileName = "MusicLevel", menuName = "Configs/MusicLevel")]
public class MiniMusicConfig : ScriptableObject
{
    [SerializeField] private PianoPlayer pianoPlayer;
    [SerializeField] private BigCow bigCow;
    [SerializeField] private OrangeCat orangeCat;
    [SerializeField] private GreenDuck greenDuck;
    [SerializeField] private PinkPig pinkPig;
    [SerializeField] private WhiteSheep whiteSheep;
    [SerializeField] private Hourse hourse;
    [SerializeField] private YellowChicken yellowChicken;
    [SerializeField] private RedChicken redChicken;
    [SerializeField] private Puppy puppy;
    [SerializeField] private Cloud[] clouds;

    public BigCow BigCow => bigCow;
    public PianoPlayer PianoPlayer => pianoPlayer;
    public OrangeCat OrangeCat => orangeCat;
    public GreenDuck GreenDuck => greenDuck;
    public PinkPig PinkPig => pinkPig;
    public WhiteSheep WhiteSheep => whiteSheep;
    public Hourse Hourse => hourse;
    public YellowChicken YellowChicken => yellowChicken;
    public RedChicken RedChicken => redChicken;
    public Puppy Puppy => puppy;
    public Cloud[] Clouds => clouds;
} 
