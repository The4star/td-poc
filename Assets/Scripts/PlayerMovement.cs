using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float characterSpeed = 2f;
    public bool isShooting = false;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _movement;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");           
    }

    private void FixedUpdate()
    {
       UpdateAnimationAndMove();
    }

    void moveCharacter()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement.normalized * characterSpeed * Time.fixedDeltaTime);
    }

    void UpdateAnimationAndMove()
    {
        if (_movement != Vector2.zero)
        {
            moveCharacter();
            _animator.SetFloat("moveX", _movement.x);
            _animator.SetFloat("moveY", _movement.y);
            _animator.SetBool("moving", true);
        }
        else
        {
            _animator.SetBool("moving", false);
        }
    }
}
