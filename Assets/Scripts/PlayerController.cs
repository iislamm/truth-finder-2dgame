using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float jumpHeight;
    public bool isFacingRight;
    public bool hasShootAttack = false;
    public KeyCode spacebar;
    public KeyCode l;
    public KeyCode r;
    public KeyCode normalAttackKey;
    public KeyCode shootAttackKey;
    public KeyCode ultimateAttackKey;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool _grounded;
    public bool attacking = false;
    public int attackPower;
    private Rigidbody2D _rigidbody2D;
    private int _ultimateSkillTimer;
    public GameObject ultimateBullet;
    public GameObject normalBullet;
    public Transform firePoint;
    private int _attackTimout;
    
    //Animation
    private Animator _animator;
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attacking = Animator.StringToHash("Attacking");

    // Use this for initialization
    private void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        _animator = GetComponent<Animator>();
        _ultimateSkillTimer = 1000;
    }
	
    // Update is called once per frame
    private void Update ()
    {
        _ultimateSkillTimer--;
        if (Input.GetKeyDown(spacebar) && _grounded){
            Jump();
        }

        if (Input.GetKey(l))
        {
            _rigidbody2D.velocity = new Vector2(-moveSpeed, _rigidbody2D.velocity.y);
          
            if (isFacingRight)
            {
                Flip();
                isFacingRight = false;
            }
        }

        if (Input.GetKey(r))
        {
            _rigidbody2D.velocity = new Vector2(moveSpeed, _rigidbody2D.velocity.y);
         
            if (!isFacingRight)
            {
                Flip();
                isFacingRight = true;
            }
        }

        if (Input.GetKeyDown(normalAttackKey))
        {
            _attackTimout = 15;
            attacking = true;
            _animator.SetBool(Attacking, true);
        }
        else
        {
            if (_attackTimout <= 0)
            {
                attacking = false;
                _animator.SetBool(Attacking, false);
            }
            else
            {
                _attackTimout--;
            }
        }

        if (Input.GetKeyDown(ultimateAttackKey))
        {
            UltimateAttack();
        }

        if (Input.GetKeyDown(shootAttackKey))
        {
            ShootAttack();
        }
        
        _animator.SetBool(Grounded, _grounded);
        _animator.SetFloat(Speed, Mathf.Abs(_rigidbody2D.velocity.x));
    }

    private void UltimateAttack()
    {
        if (_ultimateSkillTimer <= 0)
        {
            _ultimateSkillTimer = 1000;
            Instantiate(ultimateBullet, firePoint.position, firePoint.rotation);
        }
        
    }

    private void ShootAttack()
    {
        if (hasShootAttack)
        {
            Instantiate(normalBullet, firePoint.position, firePoint.rotation);
        }
    }

    private void FixedUpdate()
    {
        _grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        var transform1 = transform;
        var localScale = transform1.localScale;
        localScale = new Vector3(-(localScale.x), localScale.y, localScale.z);
        transform1.localScale = localScale;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpHeight);
    }
}