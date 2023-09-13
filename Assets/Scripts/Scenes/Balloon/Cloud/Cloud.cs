using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float speedOfCloud = 1.3f;
    //Moving cloud
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speedOfCloud * Time.deltaTime);
        if (transform.position.x >= 10) gameObject.SetActive(false);
    }
}
