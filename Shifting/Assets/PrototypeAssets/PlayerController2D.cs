using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 velocity = new Vector3(0, rb.velocity.y, 0);
        velocity.x = h * speed;
        if (Input.GetButtonDown("Jump")) {
            velocity.y = 5;
        }
        velocity = transform.rotation * velocity;

        rb.velocity = velocity;
    }
}
