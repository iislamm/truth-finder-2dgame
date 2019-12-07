using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public int health = 24;
    public int lives = 3;

    private float _flickerTime = 0f;
    public float flickerDuration = 0.1f;
    private SpriteRenderer _sprite;
    public bool isImmune = false;
    private float _immunityTime = 0f;
    public float immunityDuration = 0.1f;
    private int _points = 0;
    // Use this for initialization
    void Start () {
        _sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }
	
    // Update is called once per frame
    void Update () {
        if(this.isImmune == true)
        {
            SpriteFlicker();
            _immunityTime += Time.deltaTime;
            if(_immunityTime >= immunityDuration)
            {
                this.isImmune = false;
                this._sprite.enabled = true;
            }
        }
    }

    void SpriteFlicker()
    {
        if(this._flickerTime < this.flickerDuration)
        {
            this._flickerTime = this._flickerTime + Time.deltaTime;
        }else if(this._flickerTime >= this.flickerDuration)
        {
            _sprite.enabled = !(_sprite.enabled);
            this._flickerTime = 0;
        }
    }

    void PlayHitReaction()
    {
        this.isImmune = true;
        this._immunityTime = 0f;
    }
    public void TakeDamage(int damage)
    {
        if(isImmune == false)
        {
            health -= damage;
            if (health < 0)
                health = 0;
            if(lives > 0 && health == 0)
            {
                FindObjectOfType<LevelManager>().RespawnPlayer();
                health = 6;
                lives--;
            }else if (lives == 0 && health == 0)
            {
                Debug.Log("GameOver");
                Destroy(this.gameObject);
            }
        }
        PlayHitReaction();
    }

    public void AddPoints(int value)
    {
        _points += value;
    }
}