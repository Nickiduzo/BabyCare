using UnityEngine;

public interface ILayoutElement
{
    void Activate();
    void Deactivate();
    void Translate(Vector3 motion);
}