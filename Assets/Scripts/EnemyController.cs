using System;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public bool isFacingRight = false;
    public float speed = 3f;
    public int power;
    public float health = 2;
    public EnemyType enemyType;
    private PlayerController _player;
    public bool attacking;
    public int pointsValue;
    private Rigidbody2D _rigidbody2D;
    

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();
        attacking = false;
        pointsValue = GetPointsValue();            
    }

    private void Update()
    {
        if ((transform.position.x > _player.transform.position.x && isFacingRight)
                || (transform.position.x < _player.transform.position.x && !isFacingRight))
        {
            Flip();
        }
        
    }

    void FixedUpdate()
    {
        if(isFacingRight)
        {
            _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(-speed, _rigidbody2D.velocity.y);
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
            FindObjectOfType<PlayerStats>().TakeDamage(power);
            Flip();
        }
    }
    public void DealDamage(float d)
    {
        health -= d;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            if (enemyType == EnemyType.Thug)
            {
                FindObjectOfType<LevelManager>().EnemyDown(gameObject);
            }
            else
            {
                FindObjectOfType<LevelManager>().LastEnemyDown();
            }
            Destroy(gameObject);
        }
    }
    
    private int GetPointsValue()
    {
        switch (enemyType)
        {
            case EnemyType.Thug:
                return 10;
            case EnemyType.GangHead:
                return 20;
            case EnemyType.Boss:
                return 50;
            default:
                return 10;
        }
    }
}