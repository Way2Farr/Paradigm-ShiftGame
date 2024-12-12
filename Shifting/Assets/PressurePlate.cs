using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour
{
    private bool moveBack;
    private MeshRenderer meshRenderer;
    private Vector3 originalPosition;
       private Rigidbody rb;

    public Gate gate;

    [SerializeField] private float pressedDistance = 0.1f;

    private void Start()
    {
        originalPosition = transform.position;
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionStay(Collision other)
    {
        {
            if (transform.position.y > originalPosition.y - pressedDistance)
            {
                rb.MovePosition(transform.position + new Vector3(0, -0.01f, 0));
            }
            moveBack = false;
        }
       
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player") || other.collider.CompareTag("GameItem"))
        {
            other.transform.parent = transform;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

            if (gate != null)
            {
            gate.OpenGate(); // Call the method to open the gate
            }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player") || other.collider.CompareTag("GameItem"))
        {
            moveBack = true;
            other.transform.parent = null;
            GetComponent<MeshRenderer>().material.color = Color.green;

        }

    }

    private void Update()
    {
                    if (transform.childCount == 0)
            {
                moveBack = true;
            }
        if (moveBack)
        {
            if(transform.position.y < originalPosition.y) 
            {
                transform.Translate(0, 0.01f, 0);

            }
            else
            {
                moveBack = false;
            }
        }
    }
}