using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float speed = 20f;
    private PlayerController _player;
    private Rigidbody2D _rb;
    public int power = 2;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();
        if (_player == null)
            Destroy(gameObject);
        _rb.velocity = (_player.transform.position - transform.position).normalized * speed;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(power);
            Destroy(gameObject);
        }
    }
}