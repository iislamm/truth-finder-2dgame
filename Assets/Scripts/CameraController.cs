using UnityEngine;

public class CameraController : MonoBehaviour {

	private Transform _target;
	public float cameraSpeed;

	public float minX, maxX, minY, maxY;
	private float _clampX, _clampY;


	// Use this for initialization
	void Start ()
	{
		_target = FindObjectOfType<PlayerController>().transform;
	}

	void FixedUpdate()
	{

		Vector2 newCompose = Vector2.Lerp(transform.position, _target.position, cameraSpeed * Time.deltaTime);
		float clampX = Mathf.Clamp(newCompose.x, minX, maxX);
		float clampY = Mathf.Clamp(newCompose.y, minY, maxY);

		transform.position = new Vector3(clampX, clampY, -100f);

	}
}