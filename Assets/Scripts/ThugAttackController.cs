using System;
using UnityEngine;

public class ThugAttackController : MonoBehaviour
{
    private float _timer;
    private float _onAttackTimer;
    private EnemyController _enemy;
    private PlayerController _player;

    private Animator _animator;
    private float _distanceToPlayer;

    private static readonly int Attacking = Animator.StringToHash("Attacking");

    // Start is called before the first frame update
    void Start()
    {
        _timer = 100f;
        _enemy = GetComponentInParent<EnemyController>();
        _onAttackTimer = 0;
        _animator = GetComponentInParent<Animator>();
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        _distanceToPlayer = Mathf.Abs(_player.transform.position.x - _enemy.transform.position.x);
        if (_onAttackTimer > 0)
        {
            _onAttackTimer--;
        }
        else
        {
            _timer--;
        }
        if (_timer <= 0 && !_enemy.attacking && _distanceToPlayer < 2)
        {
            _enemy.attacking = true;
            _animator.SetBool(Attacking, true);
            _timer = 100f;
            _onAttackTimer = 15f;
        }
        else if (_onAttackTimer <= 0)
        {
            _enemy.attacking = false;
            _animator.SetBool(Attacking, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_enemy.attacking && other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStats>().TakeDamage(_enemy.power);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_enemy.attacking && other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStats>().TakeDamage(_enemy.power);
        }
    }
}