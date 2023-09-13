using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("dfdsfgds");

        Destroy(collision.gameObject);



    }
}
