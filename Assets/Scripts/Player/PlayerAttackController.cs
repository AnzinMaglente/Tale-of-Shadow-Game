using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private bool _canAttack = true;
    [SerializeField]
    private float _attackTime;
    [SerializeField]
    private float _attackSpeed;
    [SerializeField]
    private float _attackCooldown;

    [SerializeField]
    private Transform _meleeAttactOrigin;
    [SerializeField]
    private GameObject _SlashPrefab;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void HorizontalSlash()
    {
        GameObject HorSlash = Instantiate(_SlashPrefab, _meleeAttactOrigin.position, _meleeAttactOrigin.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            HorizontalSlash();
        }
    }
}
