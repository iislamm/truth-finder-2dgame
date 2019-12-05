using UnityEngine;

public class CameraScript : MonoBehaviour {

	public Transform target;
	public float camspeed;

	public float minX, maxX, minY, maxY;
	float _clampX, _clampY;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		if(target != null)
		{
			Vector2 newCompose = Vector2.Lerp(transform.position, target.position, camspeed * Time.deltaTime);

			float clampX = Mathf.Clamp(newCompose.x, minX, maxX);
			float clampY = Mathf.Clamp(newCompose.y, minY, maxY);

			transform.position = new Vector3(clampX, clampY, -10f);

		}

        
	}
}