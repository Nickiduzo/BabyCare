using UnityEngine;

[CreateAssetMenu(fileName = "SpaceLevel", menuName = "Configs/SpaceLevel")]
public class SpaceLevelConfig : ScriptableObject
{
    [SerializeField] StarView _starPrefab;

    public StarView Star => _starPrefab;
}
