using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3D : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private PlayerMovement3D playerMovement3D;
    private Vector3 offset;
    private Vector3 velocity; // used for smoothly moving the camera with Vector3.SmoothDamp()
    void Start()
    {
        playerMovement3D = playerTransform.GetComponent<PlayerMovement3D>();
        offset = new Vector3(0, 1, -5);
        velocity = Vector3.zero;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Quaternion lookRotation = Quaternion.Euler(playerMovement3D.LookRotation);
        RaycastHit hitInfo;
        bool isCameraBlocked = Physics.Raycast(playerTransform.position, lookRotation * offset, out hitInfo, offset.magnitude);

        transform.position = isCameraBlocked ? hitInfo.point : playerTransform.position + lookRotation * offset;
        transform.rotation = lookRotation;
    }
}
