using Feeding;
using UnityEngine;

public class GetBaby : MonoBehaviour
{
//#region Singleton

    public static GetBaby Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

   // #endregion Singleton
    private const string BabyKey = "Baby";
    
    [SerializeField] private GameObject _babyPrefabs;
    private BabyChoiceController _babyCC;

    public GameObject GetChoosenBaby()
    {
        //string babyNationality = PlayerPrefs.GetString(BabyKey, "Baby1");

        //_babyCC = Resources.Load<BabyChoiceController>($"Babies/{babyNationality}");

        //Baby baby = _babyPrefabs.GetComponent<Baby>();
        //baby.SetBaby(_babyCC);

        return _babyPrefabs;
    }
}
