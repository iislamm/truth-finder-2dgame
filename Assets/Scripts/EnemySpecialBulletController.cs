using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecialBulletController : MonoBehaviour
{
    private PlayerController _player;
    private Rigidbody2D _rigidbody2D;
    public float speed = 50f;
    public int power = 12;
    
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_player == null)
            Destroy(gameObject);
        _rigidbody2D.velocity = (_player.transform.position - transform.position).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(power);
            Destroy(gameObject);
        }
    }
}
