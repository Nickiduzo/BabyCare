using TMPro;
using UI;
using UnityEngine;

public class PointCounter : HudElement
{
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private ScoreCount count;

    private static int counter;

    private new void Start()
    {
        base.Start();
        Appear();
        counter = 0;
    }
    private void FixedUpdate()
    {
        mainText.text = counter.ToString();
    }
    public static void IncreasePoint() => counter++;
    public static int TakeDataScore()
    {
        return counter;
    }
}
