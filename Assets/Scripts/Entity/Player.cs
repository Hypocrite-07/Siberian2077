using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private float _speed;
    public float speedPlus = 1;
    //    private Rigidbody2D _rigidbody2D;
    //    private Transform _transform;
    //    private Animator _animator;
    private bool _isRight;
    public bool isDream = true;
    public bool isTalk = false;
    public bool isHasOtvertka = false;
    public bool canGoToFinal = false;
    [SerializeField] private Sprite _sprite;

    void Awake()
    {
        //_rigidbody2D = GetComponent<Rigidbody2D>();
        //_animator = GetComponent<Animator>();
        Instance = this;
    }

    public void Redisign()
    {
        GetComponent<SpriteRenderer>().sprite = _sprite;
    }

    void Start()
    {
        
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (WorldController.IsFinalScene)
            if (_isRight)
                Flip(_isRight);
            return;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-Vector2.right * ((_speed * speedPlus) * 2) * Time.deltaTime);
                Flip(false);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * ((_speed * speedPlus) * 2) * Time.deltaTime);
                Flip(true);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector2.right * (_speed * speedPlus) * Time.deltaTime);
            Flip(false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * (_speed * speedPlus) * Time.deltaTime);
            Flip(true);
        }
    }

    void Flip(bool right)
    {
        if (speedPlus != 0)
        {
            if (_isRight == right)
            {
                _isRight = !right;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
    }

    public static Player Instance
    {
        get;
        set;
    }
}
