using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObstacleController : MonoBehaviour
{
    private bool _isUp = false;
    public int upTimer = 200;
    public int downTimer = 300;
    public int power = 4;
    private Renderer _renderer;
    private BoxCollider2D _boxCollider2D;
    private float _sizeX; 
    private float _sizeY; 
    
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<Renderer>();
        var bounds = _boxCollider2D.bounds;
        _sizeX = bounds.size.x;
        _sizeY = bounds.size.y;
        var transformPosition = transform.position;
        transformPosition.z = -200;
        transform.position = transformPosition;
    }
    
    void Update()
    {
        if (_isUp)
            upTimer--;
        else
            downTimer--;

        if (upTimer <= 0)
        {
            upTimer = 50;
            _isUp = false;
            var transform1 = transform;
            var currentPosition = transform1.position;
            currentPosition.y -= _sizeY;
            currentPosition.z = -200;
            transform1.position = currentPosition;
        }
        else if (downTimer <= 0)
        {
            downTimer = 100;
            _isUp = true;
            var currentPosition = transform.position;
            currentPosition.y += _sizeY;
            transform.position = currentPosition;
            currentPosition.z = 10;
            transform.position = currentPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStats>().TakeDamage(power);
            var playerPosition = FindObjectOfType<PlayerController>().transform.position; 
            playerPosition.x -= _sizeX;
            FindObjectOfType<PlayerController>().transform.position = playerPosition;
        }
    }
}
