using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{

    // Animator Variables --

    enum PlayerState {Front, Back, Walking}

    PlayerState state;

    public bool stateComplete;

    public Animator animator;

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

        //Selects Animation State
        if(stateComplete) {
            SelectState();
        }

        UpdateState();
    }

    bool IsGrounded() {
        Vector3 extents = c.bounds.extents;
        extents.y = GROUNDED_MARGIN;
        return Physics.BoxCast(transform.position, extents, Vector3.down, transform.rotation, c.bounds.extents.y);
    }

    //Picks Animation based on player Input
    void SelectState() {
        stateComplete = false;
        if(IsGrounded() == true) {

            if() {
            state = PlayerState.Front;
            CheckFront();
            }
        else 
        {
            state = PlayerState.Back;
            CheckBack();

        }
    }
        else {
            state = PlayerState.Walking;
            CheckWalking();
        }
    }

    void UpdateState() {
        switch(state){
            case PlayerState.Front:
            UpdateFront();
            break;

            case PlayerState.Back:
            UpdateBack();
            break;


            case PlayerState.Walking:
            UpdateWalking();
            break;
        }
    }

    void StartFront(){
        animator.Play("CameronFront");
    }

    void StartBack(){
        animator.Play("CameronBack");
    }

    void StartWalking(){
        animator.Play("CameronWalking");
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

    }

    void YMovement() {

        bool jumpInput = Input.GetButtonDown("Jump");
        if (jumpInput && IsGrounded()) {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(2 * -Physics.gravity.y * jumpHeight), rb.velocity.z);
        }

        rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);
        
    }
}
