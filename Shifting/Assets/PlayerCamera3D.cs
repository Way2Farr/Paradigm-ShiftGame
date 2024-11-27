using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3D : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private PlayerMovement3D playerMovement3D;
    private Vector3 offset;
    void Start()
    {
        playerMovement3D = playerTransform.GetComponent<PlayerMovement3D>();
        offset = new Vector3(0, 1, -5);
        
    }


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

        Quaternion lookRotation = Quaternion.Euler(playerMovement3D.LookRotation);
        RaycastHit hitInfo;
        bool isCameraBlocked = Physics.Raycast(playerTransform.position, lookRotation * offset, out hitInfo, offset.magnitude);

        transform.position = isCameraBlocked ? hitInfo.point : playerTransform.position + lookRotation * offset;
        // transform.position = playerTransform.position + lookRotation * offset;
        transform.rotation = lookRotation;
    }
}
