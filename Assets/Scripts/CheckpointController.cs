using UnityEngine;

public class CheckpointController : MonoBehaviour {
    public int enemiesCount = 5;
    public bool isLast = false;

    void OnTriggerEnter2D(Collider2D other){

        if (other.CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().NewCheckPoint(gameObject);
        }
    }
}