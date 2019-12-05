using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float jumpHeight;
    bool _isFacingRight;
    [FormerlySerializedAs("Spacebar")] public KeyCode spacebar;
    [FormerlySerializedAs("L")] public KeyCode l;
    [FormerlySerializedAs("R")] public KeyCode r;
    public KeyCode shootKey;
    public Transform groundCheck;
    public float groundCheckRadius;

    public LayerMask whatisGround;
    private bool _grounded;
    public bool attacking = false;
    public int attackPower;

    //Animation
    private Animator _animator;
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attacking = Animator.StringToHash("Attacking");

    // Use this for initialization
    void Start () {
        _isFacingRight = true;
        _animator = GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(spacebar) && _grounded){
            Jump();
        }

        if (Input.GetKey(l))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
          
            if (_isFacingRight)
            {
                Flip();
                _isFacingRight = false;
            }
        }

        if (Input.GetKey(r))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
         
            if (!_isFacingRight)
            {
                Flip();
                _isFacingRight = true;
            }
        }

        if (Input.GetKey(shootKey))
        {
            attacking = true;
            _animator.SetBool(Attacking, true);
        }
        else
        {
            attacking = false;
            _animator.SetBool(Attacking, false);
        }
        
        _animator.SetBool(Grounded, _grounded);
        _animator.SetFloat(Speed, Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    void FixedUpdate()
    {
        _grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatisGround);
    }

    void Flip(){
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
       
    }
}