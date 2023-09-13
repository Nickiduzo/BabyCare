using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Camera Effects
/// </summary>
public class CameraEffects : MonoBehaviour
{
    public UnityEvent<float> OnShake { get; private set; } = new();

    /// <summary>
    /// Amount of Shake
    /// </summary>
    [SerializeField] Vector3 amount = new Vector3(1f, 1f, 0);

    /// <summary>
    /// Duration of Shake
    /// </summary>
    [SerializeField] float _duration = 1;

    /// <summary>
    /// Shake Speed
    /// </summary>
    [SerializeField] float _speed = 10;

    /// <summary>
    /// Amount over Lifetime [0,1]
    /// </summary>
    [SerializeField] AnimationCurve _curve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    /// <summary>
    /// Set it to true: The camera position is set in reference to the old position of the camera
    /// Set it to false: The camera position is set in absolute values or is fixed to an object
    /// </summary>
    [SerializeField] bool _deltaMovement = true;
    [SerializeField] Camera _camera;

    private float _time = 0;
    private Vector3 _lastPos;
    private Vector3 _nextPos;
    private float _lastFoV;
    private float _nextFoV;
    private bool _destroyAfterPlay;

    public static CameraEffects Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Do the shake
    /// </summary>
    public void ShakeOnce(float duration = 1f, float speed = 10f, Vector3? amount = null, Camera camera = null, bool deltaMovement = true, AnimationCurve curve = null)
    {
        //set data
        if(camera != null)
            _camera = camera;
        _duration = duration;
        _speed = speed;
        if (amount != null)
            amount = (Vector3)amount;
        if (curve != null)
            _curve = curve;
        _deltaMovement = deltaMovement;

        //one time
        Shake();
        OnShake?.Invoke(duration);
    }

    /// <summary>
    /// Do the shake
    /// </summary>
    public void Shake()
    {
        ResetCam();
        _time = _duration;
    }

    private void LateUpdate()
    {
        if (_time > 0)
        {
            //do something
            _time -= Time.deltaTime;
            if (_time > 0)
            {
                //next position based on perlin noise
                _nextPos = (Mathf.PerlinNoise(_time * _speed, _time * _speed * 2) - 0.5f) * amount.x * transform.right * _curve.Evaluate(1f - _time / _duration) +
                          (Mathf.PerlinNoise(_time * _speed * 2, _time * _speed) - 0.5f) * amount.y * transform.up * _curve.Evaluate(1f - _time / _duration);
                _nextFoV = (Mathf.PerlinNoise(_time * _speed * 2, _time * _speed * 2) - 0.5f) * amount.z * _curve.Evaluate(1f - _time / _duration);

                _camera.fieldOfView += (_nextFoV - _lastFoV);
                _camera.transform.Translate(_deltaMovement ? (_nextPos - _lastPos) : _nextPos);

                _lastPos = _nextPos;
                _lastFoV = _nextFoV;
            }
            else
            {
                //last frame
                ResetCam();
                if (_destroyAfterPlay)
                    Destroy(this);
            }
        }
    }

    private void ResetCam()
    {
        //reset the last delta
        _camera.transform.Translate(_deltaMovement ? -_lastPos : Vector3.zero);
        _camera.fieldOfView -= _lastFoV;

        //clear values
        _lastPos = _nextPos = Vector3.zero;
        _lastFoV = _nextFoV = 0f;
    }
}
