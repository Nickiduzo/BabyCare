using UnityEngine;

public class Tool : MonoBehaviour
{
    private Collider2D collider2D;

    SpawnerGround sg;

    public GameObject prefObjekt;

    private Animator anim;

    private bool work, goBack = false;

    private Vector2 originalPosition, screenBounds;
    float speed = 5;
    public int wolckAwei = 0;

    GameObject Por;

    void Awake()
    {
        Tool[] controllers = FindObjectsOfType<Tool>();
        anim = GetComponent<Animator>();

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
        originalPosition = transform.position;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        sg = GameObject.FindGameObjectWithTag("SpawnerGround").GetComponent<SpawnerGround>();
        collider2D = GetComponent<Collider2D>();


    }


    void Update()
    {


        if (goBack) GoBack(); else if (work) Worck();

        //check for destruction
        if (sg.graund <= 0 || sg.vegetables > 0)
        {
            GoDestroy();
            sg.graund = 2;

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
        Debug.Log("Work");
    }

    //the method of destroying the tool after the work is done
    void GoDestroy()
    {
        Vector2 pos;
        pos = new Vector2(screenBounds.x + 3, transform.position.y);
        originalPosition = pos;
        work = false;

        collider2D.enabled = false;
        anim.SetBool("State", false);

        if (Vector2.Distance(transform.position, pos) >= 1) Destroy(gameObject, 3f);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rock") || other.CompareTag("Seedlings"))
        {
            work = true;
            goBack = false;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rock") || other.CompareTag("Seedlings"))
        {
            work = false;
            goBack = true;
        }

    }


}
