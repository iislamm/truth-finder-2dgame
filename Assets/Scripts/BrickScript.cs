using UnityEngine;

public class BrickScript : MonoBehaviour {
	private SpriteRenderer _sr;

	public Sprite explodedBlock;
	// Use this for initialization
	void Start () {
		_sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.CompareTag("Player") && other.GetContact(0).point.y < transform.position.y)
		{
			_sr.sprite = explodedBlock;

			Object.Destroy(gameObject, .2f);
		}
	}
}