using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    private PlayerMovement3D playerMovement3D;
    private Vector3 offset;
    private Vector3 offset2D;
    public Transform player2DTransform;
    void Start()
    {
        playerMovement3D = playerTransform.GetComponent<PlayerMovement3D>();
        offset = new Vector3(0, 1, -5);
        offset2D = new Vector3(0, 1, -10);


    }

    // Added Cursor Unlocking Within Wall
    void Update() {

        if (ShiftWall.insideWall == true) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

     
    void LateUpdate()
    {
        if (player2DTransform == null) {
            Quaternion lookRotation = Quaternion.Euler(playerMovement3D.LookRotation);
            RaycastHit hitInfo;
            bool isCameraBlocked = Physics.Raycast(playerTransform.position, lookRotation * offset, out hitInfo, offset.magnitude);

            transform.position = isCameraBlocked ? hitInfo.point : playerTransform.position + lookRotation * offset;
            transform.rotation = lookRotation;
        }
        else {
            transform.position = player2DTransform.position + player2DTransform.rotation * offset2D;
            transform.rotation = player2DTransform.rotation;
            Debug.Log(player2DTransform.position);
        }
    }
}
