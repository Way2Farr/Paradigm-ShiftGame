using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3D : MonoBehaviour
{
    public GameObject pointer;
    public Camera firstPerson;
    public Transform firstPersonOrientation;

    private AudioListener audioListener;
    
    [SerializeField]
    private Transform playerTransform;

    private PlayerMovement3D playerMovement3D;
    private Vector3 offset;
    private Vector3 velocity; // used for smoothly moving the camera with Vector3.SmoothDamp()


    void Start()
    {
        playerMovement3D = playerTransform.GetComponent<PlayerMovement3D>();
        offset = new Vector3(0, 1, -5);
        audioListener = GetComponent<AudioListener>();
        
    }

    // Added Cursor Unlocking Within Wall
    void Update() {

        firstPersonOrientation.rotation = transform.rotation;

        //Toggle Cameras to first person
        if (Input.GetKeyDown(KeyCode.C))
        {
            gameObject.SetActive(!gameObject.activeSelf);
            audioListener.enabled = !audioListener.enabled;
            firstPerson.gameObject.SetActive(!firstPerson.gameObject.activeSelf);
            pointer.gameObject.SetActive(!pointer.gameObject.activeSelf);
        }

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
        transform.rotation = lookRotation;
    }
}
