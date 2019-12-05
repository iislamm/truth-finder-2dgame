using UnityEngine;

public class AutoGun : MonoBehaviour
{

    public Transform firePoint;
    public KeyCode fire;
    public GameObject bulletPrefab;
    public GameObject checkPoint;
    private float _timer = 100f;
    // Update is called once per frame
    void Update()
    {
        _timer--;
        if (FindObjectOfType<LevelManager>().currentCheckpoint == checkPoint && _timer <= 0)
        {

            Invoke(nameof(Shoot), 0.0f);
            _timer = 100f;

        }
        
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}