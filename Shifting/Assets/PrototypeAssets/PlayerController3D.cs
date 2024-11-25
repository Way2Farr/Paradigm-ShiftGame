using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float mouseSensitivity;
    public Vector3 lookRotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lookRotation = Vector3.zero;
    }

    void Update()
    {
        float h = Input.GetAxis("Mouse X") * mouseSensitivity;
        float v = Input.GetAxis("Mouse Y") * mouseSensitivity;
        lookRotation += new Vector3(-v, h, 0);
        lookRotation.x = Mathf.Clamp(lookRotation.x, -80f, 80f);

        transform.rotation = Quaternion.Euler(0, lookRotation.y, 0);
        
        Vector3 velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        velocity = velocity.normalized * speed;
        velocity.y = rb.velocity.y;
        velocity = transform.rotation * velocity;
        if (Input.GetButtonDown("Jump")) {
            velocity.y = 5;
        }
        rb.velocity = velocity;
    }

}
