using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    public float speed = 20f;
    private PlayerController _player;
    private Rigidbody2D _rb;
    public int power = 2;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();
        if (!_player.isFacingRight)
            speed = -speed;
        _rb.velocity = new Vector2(speed, _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().DealDamage(power);
            Destroy(gameObject);
        }
    }
}
