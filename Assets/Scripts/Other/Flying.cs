using UnityEngine;

public class Flying : MonoBehaviour
{
    SpawnerGround gc;

    public GameObject prefObjekt;

    private SpriteRenderer renderer;

    [SerializeField] Transform center;
    [SerializeField] private float radius = 1f, speed = 1;

    TouchPhase touchPhase = TouchPhase.Ended;
    private Vector2 originalPosition;
    private bool angry, goBack, chill = false;

    float positionX, positionY, angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        GetComponent<Rigidbody2D>();

        Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        originalPosition = transform.position;

        gc = GameObject.FindGameObjectWithTag("SpawnerGround").GetComponent<SpawnerGround>();
        gc.bugs++;

        GetComponent<Collider2D>();

        prefObjekt = GameObject.FindGameObjectWithTag("Vegetables");
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase)
        {
            angry = false;
            goBack = true;
            chill = false;
        }


        //Checking the approach to the object of aggressio
        if (!goBack)
        {
            if (Vector2.Distance(transform.position, prefObjekt.transform.position) >= 5)
            {
                angry = true;
                goBack = false;
                chill = false;
            }

            if (Vector2.Distance(transform.position, prefObjekt.transform.position) <= 1)
            {
                chill = true;
                angry = false;
                goBack = false;
            }
        }

        if (chill) Chill();
        else if (angry) Angry();
        else if (goBack) GoBack();
    }

    void Chill()
    {
        //Appointment for polotu navkolo apple
        center = prefObjekt.transform;
        positionX = center.position.x + Mathf.Cos(angle) * radius;
        positionY = center.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector2(positionX, positionY);
        angle = angle + Time.deltaTime * speed;

        if (angle >= 360f)
        {
            renderer.flipX = false;
            angle = 0f;
        }
        else
            renderer.flipX = true;
    }

    void Angry()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, prefObjekt.transform.position, speed * Time.deltaTime);
        // transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        Debug.Log("Angry");
    }

    void GoBack()
    {
        Destroy(gameObject, 5f);
        transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
        Debug.Log("GoBack");
    }


    //call the method to activate the milk in the jar

    void OnMouseDown()
    {
        // Destroy(gameObject, 3f);   
        angry = false;
        goBack = true;
        chill = false;
    }
}