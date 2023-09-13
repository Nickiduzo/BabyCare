using TMPro;
using UI;
using UnityEngine;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Counter : HudElement
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _score = 0;
    public int Score
    {
        get { return _score; }
        set
        {
            ChangeCount(value);
            ScoreService.Score = value;
            _score = value;
        }
    }

    private new void Start()
    {
        base.Start();
        Appear();
    }

    public void ChangeCount(int count)
    {
        _text.text = count.ToString();
    }
}
