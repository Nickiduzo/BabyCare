using UnityEngine;

public class SaveBabyChoice : MonoBehaviour
{
    private const string BabyKey = "Baby";

    public void Save(string baby)
    { 
        PlayerPrefs.SetString(BabyKey, baby);
    }
}

