using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Player;
    private Rigidbody2D Rb;
    private Vector2 Movement;
    public bool IsFacingRight=false;
    public float MaxSpeed=3f;
    public int Damage=6;
    public float Health;
    public float MaxHealth;
    public GameObject HealthBar;


    // Start is called before the first frame update
    void Start()
    {
        Rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Direction = Player.position - transform.position;
        // Mathf.Atan2 to convert the direction into degrees 
        // between 0 and vetor 2
        // Mathf.Rad2Deg to convert rad into degrees
        float Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Rb.rotation = Angle;
        //Direction.Normalize();
        //Movement = Direction;
    }
    public void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.localScale = new Vector3((transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //transform.Rotate(0f, 180f, 0f);
    }
    void FixedUpdate()
    {
        //moveCharacter(Movement);

        if (this.IsFacingRight == true)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(MaxSpeed, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-MaxSpeed, this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}