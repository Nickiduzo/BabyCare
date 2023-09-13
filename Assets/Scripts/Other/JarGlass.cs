using System.Collections.Generic;
using UnityEngine;

public class JarGlass : MonoBehaviour
{
    public float speed = 1;
    private Vector2 originalPosition;

    bool goForward, goBack = false;

    private Vector2 screenBounds;
    private float posStateX;

    SpawnerGround sg;

    int wormsCount, count;

    List<Worms> caughtWorms;

    Collider2D myCollider;

    void Awake()
    {
        JarGlass[] controllers = FindObjectsOfType<JarGlass>();

        myCollider = GetComponent<Collider2D>();
        caughtWorms = new List<Worms>();

        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        posStateX = -screenBounds.x + 3;
        originalPosition = transform.position;
        sg = GameObject.FindGameObjectWithTag("SpawnerGround").GetComponent<SpawnerGround>();
        count = sg.worms;
    }
    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        if (transform.position.x > posStateX) goForward = true;
        if (count < wormsCount)
        {
            goBack = true;
            goForward = false;
        }

        if (goForward) GoForward(); else if (goBack) GoBack();

    }

    void GoForward()
    {
        speed = 0;

    }

    void GoBack()
    {
        speed = -2;

        Debug.Log("GoBack");

        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Worms")
        {
            wormsCount++;
            Worms worms = collision.gameObject.GetComponent<Worms>();
            caughtWorms.Add(worms);
            collision.transform.SetParent(transform);
            collision.transform.position = gameObject.transform.position;
            collision.transform.rotation = transform.rotation;
            //collision.transform.localScale = Vector3.one;

        }
    }

}
