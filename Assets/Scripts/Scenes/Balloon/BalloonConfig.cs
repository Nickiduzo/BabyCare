using UnityEngine;

[CreateAssetMenu(fileName = "BalloonLevel", menuName = "Configs/BalloonLevel")]
public class BalloonConfig : ScriptableObject
{
    [SerializeField] private Balloon[] balloon;
    [SerializeField] private Cloud[] clouds;

    public Balloon[] Balloon => balloon;
    public Cloud[] Clouds => clouds;
}
