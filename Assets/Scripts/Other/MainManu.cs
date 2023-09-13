using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartScene()
    {
        // calls Destroy on all active GameObjects should be sufficient

        GameObject[] GameObjects = FindObjectsOfType<GameObject>();

        for (int i = 0; i < GameObjects.Length; i++)
            Destroy(GameObjects[i]);

        SceneManager.LoadScene(0);
    }
}
