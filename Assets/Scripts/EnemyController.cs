using System;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public bool isFacingRight = false;
    public float speed = 3f;
    public int damage;
    public float health = 2;
    public float maxHealth;
    public EnemyType enemyType;
    private PlayerController _player;
    public bool attacking;
    protected int pointsValue;
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        attacking = false;
    }

    private void Update()
    {
        if (_player != null)
        {
            if ((transform.position.x > _player.transform.position.x && isFacingRight)
                || (transform.position.x < _player.transform.position.x && !isFacingRight))
            {
                Flip();
            }
        }
    }

    void FixedUpdate()
    {
        if(isFacingRight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        var transform1 = transform;
        var localScale = transform1.localScale;
        localScale = new Vector3(-(localScale.x), localScale.y, localScale.z);
        transform1.localScale = localScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
            Flip();
        }
    }
    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
    }
    void CheckDeath()
    {
        if (health <= 0)
        {
            FindObjectOfType<LevelManager>().ThugDown(gameObject);
            Destroy(gameObject);
        }
    }
    

    public int GetPointsValue()
    {
        return pointsValue;
    }
    
}