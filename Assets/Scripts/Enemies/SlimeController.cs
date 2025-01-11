using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public PlayerHealthController playerHealthController;

    public GameObject player;
    private Rigidbody2D _rb;
    private Animator _anim;
    [SerializeField]
    private GameObject _pointA;
    [SerializeField]
    private GameObject _pointB;
    private Transform _currentPoint;
    [SerializeField]
    private float _patrolSpeed;

    public int damage = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _currentPoint = _pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerHealthController = player.GetComponent<PlayerHealthController>();
            }
        }

        _anim.SetFloat("xVelocity", Mathf.Abs(_rb.velocity.x));

        Vector2 point = _currentPoint.position - transform.position;

        if(_currentPoint == _pointB.transform)
        {
            _rb.velocity = new Vector2(_patrolSpeed, 0);
        }
        else
        {
            _rb.velocity = new Vector2(-_patrolSpeed, 0);
        }

        if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5 && _currentPoint == _pointB.transform)
        {
            flip();
            _currentPoint = _pointA.transform;
        }
        if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5 && _currentPoint == _pointA.transform)
        {
            flip();
            _currentPoint = _pointB.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool facingRight = (transform.position.x > _rb.transform.position.x);
            playerHealthController.TakeDamage(damage);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(_pointB.transform.position, 0.5f);
    }

    private void flip()
    {
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
