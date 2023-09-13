using UnityEngine;

public class FunnelTool : MonoBehaviour
{
    private Animator anim;
    private Collider2D collider2D;

    SpawnerGround gc;

    public GameObject prefObjekt;

    private bool worck, goBack = false;

    private Vector2 originalPosition, screenBounds;
    float speed = 5;
    public int wolckAwei = 0;
    void Awake()
    {
        FunnelTool[] controllers = FindObjectsOfType<FunnelTool>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        originalPosition = transform.position;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        gc = GameObject.FindGameObjectWithTag("SpawnerGround").GetComponent<SpawnerGround>();
        collider2D = GetComponent<Collider2D>();
    }


    void Update()
    {



        if (goBack) GoBack(); else if (worck) Worck();

        //Destruction of the object after completing the task

        if (gc.vegetables > 9)
        {
            Vector2 pos;
            pos = new Vector2(screenBounds.x + 3, transform.position.y);
            originalPosition = pos;
            collider2D.enabled = false;

            if (Vector2.Distance(transform.position, pos) >= 1) Destroy(gameObject, 3f);
        }



    }

    //Moving 
    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
        anim.SetBool("State", false);

    }

    void Worck()
    {

        anim.SetBool("State", true);
        //  Debug.Log("Worck");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rock") || other.CompareTag("Seedlings"))
        {
            worck = true;
            goBack = false;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rock") || other.CompareTag("Seedlings"))
        {
            worck = false;
            goBack = true;
        }

    }

}
