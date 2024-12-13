using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerController2D : MonoBehaviour
{

    // Animator Variables --
    public Animator animator;

    public SpriteRenderer spriteRenderer;

    // layers
    [SerializeField] private LayerMask ground;

    // components
    private Rigidbody _rigidbody;
    private Collider _collider;
    public Transform axis; // we move based on our parent's rotation, which follows the wall's axis

    private float xMovement;
    private float _currentVelocity;
    private float _yVelocity;
    [SerializeField] float gravity = 10f;
    private float modifier = 1f;
    //private bool Grounded;

    [SerializeField] float groundFriction = 0.01f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float gravityMultiplier = 3.0f;
    [SerializeField] float groundSpeedMultiplier = 1.2f;
    // velocity functions
    [SerializeField] private float speed = 30f;

    // fields for ground check
    public bool grounded = false;
    public float groundedCheckDistance;
    private float bufferCheckDistance = 0.1f;

    // bools for movement
    public bool hasDoubleJump = true;
    public bool movingInAir = false;

    // Awake
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // apply gravity 
        _rigidbody.velocity += Vector3.down * gravity * gravityMultiplier * Time.deltaTime;
        //IsGrounded();
        updateVelocity();
        _rigidbody.velocity = axis.rotation * new Vector3(_currentVelocity, _rigidbody.velocity.y);


    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        xMovement = ctx.ReadValue<float>();

        float speedToAdd = xMovement * speed * modifier;

        if (ctx.performed || IsGrounded())
        {
            if (ctx.ReadValue<float>() > 0.5)
            {
                // Debug.Log("Move left");
                _currentVelocity = speedToAdd * groundSpeedMultiplier;
            }
            else if (ctx.ReadValue<float>() < -0.5)
            {
                // Debug.Log("Move right");
                _currentVelocity = speedToAdd * groundSpeedMultiplier;
            }
        }
        else if (ctx.performed && !IsGrounded())
        {
            if (_currentVelocity == 0)
            {
                _currentVelocity = speedToAdd;
            }
            else if (xMovement < 0 && _currentVelocity > 0 || xMovement > 0 && _currentVelocity < 0)
            {
                movingInAir = true;
            }

        }
        else if (ctx.canceled)
        {
            movingInAir = false;
        }

        animator.SetFloat("moveX", Math.Abs(xMovement));

    }

    public void OnJump(InputAction.CallbackContext ctx)
    {

        float speedToAdd = xMovement * speed * modifier;

        //_rigidbody.velocity = new Vector2(_currentVelocity, 0);
        if (ctx.performed && IsGrounded())
        {
            // Debug.Log("Jump");
            //_currentVelocity = speedToAdd;

            Vector3 jump = new Vector3(0, 1, 0);
            _rigidbody.velocity = new Vector2(_currentVelocity, 0);
            _rigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
        else if (ctx.performed && !IsGrounded() && hasDoubleJump)
        {
            if (xMovement > 0)
            {
                _currentVelocity = speedToAdd;
            }
            else if (xMovement < 0)
            {
                _currentVelocity = speedToAdd;
            }

            Vector3 jump = new Vector3(0, 1, 0);
            _rigidbody.velocity = new Vector2(_currentVelocity, 0);
            _rigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
            hasDoubleJump = false;

        }

        animator.SetFloat("moveY", _rigidbody.velocity.y);
    }

    bool IsGrounded()
    {

        Vector3 extents = _collider.bounds.extents;
        float GROUNDED_MARGIN = 0.1f;
        extents.y = GROUNDED_MARGIN;
        return Physics.BoxCast(transform.position, extents, Vector3.down, transform.rotation, _collider.bounds.extents.y);

    }

    private void CheckIfGrounded()
    {
        RaycastHit[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = transform.position;
        hits = Physics.RaycastAll(positionToCheck, new Vector2(0, -1), 0.000001f);

        //if a collider was hit, we are grounded
        if (hits.Length > 0)
        {
            // Debug.Log("Is on ground");
            //grounded = true;
        }
        else
        {
            //grounded = false;
        }
    }

    void updateVelocity()
    {

        grounded = IsGrounded();

      

        animator.SetBool("isGrounded", grounded);
        // apply ground friction
        if (IsGrounded() && Mathf.Abs(xMovement) < 0.3f)
        {
            // Debug.Log(_currentVelocity);
            _currentVelocity = 0;
            if (Mathf.Abs(_currentVelocity) < 3f)
            {
                //animator.SetBool(IsRunning, false);
                _currentVelocity = 0;
            }
        }
        SpriteDirection(xMovement);

        // refresh double jump
        if (IsGrounded())
        {
            hasDoubleJump = true;

     
        }
        else
        {
        }

        Debug.Log(_currentVelocity);
       
    }
         void SpriteDirection(float xMovement) {
        if (spriteRenderer != null)
        {
            if (xMovement < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (xMovement > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
     }
}
