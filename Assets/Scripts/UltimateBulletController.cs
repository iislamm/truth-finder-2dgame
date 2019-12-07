using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateBulletController : MonoBehaviour
{
    private PlayerController _player;
    private Rigidbody2D _rigidbody2D;
    public float speed = 50f;
    public int power = 6;
    
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_player != null)
        {
            if (!_player.isFacingRight)
                speed = -speed;
            _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().DealDamage(power);
        }
    }
}
