using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{

    // Animator Variables --

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    private bool movingBackwards;

    // Sound Variables --
    public AudioSource legs;

    // --
    public Vector3 LookRotation { get {return lookRotation;} }
    private readonly float ACCELERATION_FACTOR = 10;
    private readonly float GROUNDED_MARGIN = 0.1f;
    private readonly float MAX_LOOK_ANGLE = 70f;

    private Rigidbody rb;
    private Collider c;


    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jumpHeight;

    private Vector3 lookRotation;
    private Vector3 targetVelocity; // targetVelocity & currentVelocity only for X-Z axis movement
    private Vector3 currentVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<Collider>();
        legs = GetComponent<AudioSource>();

        lookRotation = transform.rotation.eulerAngles;
        targetVelocity = Vector3.zero;
        currentVelocity = Vector3.zero;
    }

    void Update()
    {
        // take mouse input and rotate character
    PlayerInput();
        // take input and update X-Z axis movement
    XZMovement();
        // take input and update Y axis movement
    YMovement();

    CheckSideways();

     // Check if the key is being held down
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            // If the sound is not already playing, start it
            if (!legs.isPlaying)
            {
                legs.Play();
            }
        }
        else
        {
            // If the key is not being held, stop the sound
            if (legs.isPlaying)
            {
                legs.Stop();
            }
        }
    }


    bool IsGrounded() {
        Vector3 extents = c.bounds.extents;
        extents.y = GROUNDED_MARGIN;
        return Physics.BoxCast(transform.position, extents, Vector3.down, transform.rotation, c.bounds.extents.y);
    }
    void PlayerInput(){
        float mouseInputX = Input.GetAxis("Mouse X");
        float mouseInputY = Input.GetAxis("Mouse Y");
        lookRotation += new Vector3(-mouseInputY, mouseInputX, 0);
        lookRotation.x = Mathf.Clamp(lookRotation.x, -MAX_LOOK_ANGLE, MAX_LOOK_ANGLE);

        rb.MoveRotation(Quaternion.Euler(0, lookRotation.y, 0));

    }
    void XZMovement() {
        float strafeInput = Input.GetAxisRaw("Horizontal");
        float forwardInput = Input.GetAxisRaw("Vertical");
        targetVelocity = new Vector3(strafeInput, 0, forwardInput).normalized * walkSpeed;
        targetVelocity = transform.rotation * targetVelocity;

        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, ACCELERATION_FACTOR * Time.deltaTime);


        animator.SetFloat("moveSpeed", currentVelocity.magnitude);

        SpriteDirection(strafeInput);
        MovingBackwards(forwardInput);

    }
    void YMovement() {

        bool jumpInput = Input.GetButtonDown("Jump");
        if (jumpInput && IsGrounded()) {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(2 * -Physics.gravity.y * jumpHeight), rb.velocity.z);
        }

        rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);

        animator.SetBool("isGrounded", IsGrounded());
    
    }

     void SpriteDirection(float strafeInput) {
        if (spriteRenderer != null)
        {
            if (strafeInput < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (strafeInput > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
     }

     void MovingBackwards(float forwardInput) {
        if(!movingBackwards && forwardInput < 0) {
            movingBackwards = true;
        } else if(movingBackwards && forwardInput > 0) {
            movingBackwards = false;
        }

        animator.SetBool("facingCamera", movingBackwards);
    }  

        void CheckSideways()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isSideways", true);
        }
        else
        {
            animator.SetBool("isSideways", false);
        }
    }
}

