using Animal;
using Quest;
using UnityEngine;

public class AnimalSelector : MonoBehaviour
{
    [SerializeField] private AnimalType[] availableAnimalTypes;
    [SerializeField] private GameObject[] _animalsPrefubs;
    [SerializeField] private Transform _animalContainer;
    
    private int _indexAnimalType;

    // create random animal and set it to config
    public void SetRandomAnimal(QuestLevelConfig _config)
    {
        Debug.Log("SetRandomAnimal");
        GetRandomIndex();
        _config.animalType = GetRandomIndex();
        Instantiate(_animalsPrefubs[_indexAnimalType], _animalContainer);
        Debug.Log("_config.animalType " + _config.animalType);
    }

    // create type of animal from config
    public void GetAnimalFromConfig(QuestLevelConfig config)
    {
        Debug.Log("SetAnimalFromConfig");
        Instantiate(_animalsPrefubs[config.animalType], _animalContainer);
    }

    // create random index for animal type
    private int GetRandomIndex()
        => _indexAnimalType = Random.Range(0, availableAnimalTypes.Length);
}
