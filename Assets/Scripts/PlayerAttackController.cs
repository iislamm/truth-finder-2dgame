using System;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private PlayerController _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AttackEnemy(other);
    }
    
    private void OnTriggerStay2D (Collider2D other)
    {
        AttackEnemy(other);
    }

    private void AttackEnemy(Collider2D enemy)
    {
        
        if (enemy.CompareTag("Enemy") && _player.attacking)
        {
            enemy.gameObject.GetComponent<EnemyController>().DealDamage(_player.attackPower);
        }
    }
}