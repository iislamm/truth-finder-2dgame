using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangHeadAttackController : MonoBehaviour
{
    private float _attackTimer;
    private int _specialAttackTimer;
    private EnemyController _enemy;
    private PlayerController _player;
    private Animator _animator;
    public GameObject bulletGameObject;
    public GameObject specialBulletGameObject;

    private void Start()
    {
        _attackTimer = 100f;
        _enemy = GetComponentInParent<EnemyController>();
        _animator = GetComponentInParent<Animator>();
        _player = FindObjectOfType<PlayerController>();
        _specialAttackTimer = 1000;
    }

    private void Update()
    {
        _attackTimer--;
        _specialAttackTimer--;
        if (_attackTimer <= 0)
        {
            Shoot();
            _attackTimer = 100f;
        }
    }

    private void Shoot()
    {
        var transform1 = transform;
        if (_specialAttackTimer <= 0)
        {
            _specialAttackTimer = 1000;
            Instantiate(specialBulletGameObject, transform1.position, transform1.rotation);
        }
        else
        {
            Instantiate(bulletGameObject, transform1.position, transform1.rotation);
        }
    }
}
