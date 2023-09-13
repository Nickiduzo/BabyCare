using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    [SerializeField] List<Sprite> _sprites;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] ParticleView _blowParticle;
    [SerializeField, Range(0f, 10f)] float _relativeSpeed;
    [SerializeField, Range(20f, 70f)] float _distanceToDestroy;

    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Count)];
    }
    private void Start()
    {
        transform.parent = null;

        _rb.DORotate(360f, 2f).SetRelative(true).SetLoops(-1).SetEase(Ease.Linear);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.position) > _distanceToDestroy)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(_blowParticle, transform.position, _blowParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        DOTween.Kill(_rb);
    }
}
