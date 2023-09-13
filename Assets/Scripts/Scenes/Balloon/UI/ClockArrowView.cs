using UnityEngine;

public class ClockArrowView : MonoBehaviour
{
    private void Update() => SpinArrow();
    private void SpinArrow() => gameObject.transform.Rotate(Vector3.forward * -160 * Time.deltaTime);  
    
}
