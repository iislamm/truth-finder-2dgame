using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 20f;
    private PlayerController _player;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        if (_player != null)
        {
            rb.velocity = (_player.transform.position - transform.position).normalized * speed;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
            Destroy(gameObject);
    }
}