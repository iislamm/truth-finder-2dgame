using System;
using UnityEngine;

public class AutoGunController : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletGameObject;
    public GameObject checkPoint;
    private float _timer = 100f;
    private LevelManager _levelManager;
    private PlayerController _player;
    private bool _isFacingRight;

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _player = FindObjectOfType<PlayerController>();
        _isFacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        _timer--;
        if (_levelManager.currentCheckpoint == checkPoint && _timer <= 0)
        {
            Shoot();
            _timer = 100f;
        }

        if (_player.transform.position.x > transform.position.x && !_isFacingRight
            || _player.transform.position.x < transform.position.x && _isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale = new Vector3(-(localScale.x), localScale.y, localScale.z);
            transform1.localScale = localScale;
        }
    }
    void Shoot()
    {
        Instantiate(bulletGameObject, firePoint.position, firePoint.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
            Destroy(gameObject);
    }
}