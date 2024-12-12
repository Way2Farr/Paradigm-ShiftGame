using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerCamera : MonoBehaviour
{
    private Canvas canvas;

    [SerializeField]
    private GameObject pointerPrefab;
    [SerializeField]
    private GameObject shutterPrefab;

    [SerializeField]
    private Transform playerTransform; // set this to be the 3d character
    private PlayerMovement3D playerMovement3D;
    private Vector3 offset;
    private Vector3 heightOffset; // only used for first person, so camera feels at head height
    private bool isFirstPerson;
    private GameObject pointer;
    private GameObject shutter;
    public Transform player2DTransform; // dont touch this in editor
    void Start()
    {
        canvas = Component.FindFirstObjectByType<Canvas>(); // pretty rushed implementation, assumes that there is an Canvas in the scene
        Debug.Assert(canvas != null, "Missing canvas in scene");
        pointer = Instantiate(pointerPrefab);
        pointer.transform.SetParent(canvas.transform, false); // the false is so unity doesn't move it elsewhere when parenting
        shutter = Instantiate(shutterPrefab);
        shutter.transform.SetParent(canvas.transform, false);

        playerMovement3D = playerTransform.GetComponent<PlayerMovement3D>();
        offset = new Vector3(0, 1, -5);
        heightOffset = new Vector3(0, 2, 0); // walking too close to wall makes you see through it
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

        if(player2DTransform == null && Input.GetKeyDown(KeyCode.C)) { // need to be outside of wall to change perspective
            ToggleFirstPerson();
        }

        if (isFirstPerson && Input.GetMouseButtonDown(0)) {
            SnapPhoto();
        }
    }


    void LateUpdate()
    {
        if (player2DTransform == null) {
            Quaternion lookRotation = Quaternion.Euler(playerMovement3D.LookRotation);
            RaycastHit hitInfo;
            bool isCameraBlocked = Physics.Raycast(playerTransform.position, lookRotation * offset, out hitInfo, offset.magnitude);
            
            if (isFirstPerson) {
                transform.position = playerTransform.position + heightOffset;
            }
            else {
                transform.position = isCameraBlocked ? hitInfo.point : playerTransform.position + lookRotation * offset;
            }
            
            transform.rotation = lookRotation;
        }
        else {
            transform.position = player2DTransform.position + player2DTransform.rotation * offset;
            transform.rotation = player2DTransform.rotation;
            Debug.Log(player2DTransform.position);
        }
    }

    void ToggleFirstPerson() {
        // // use boolean to determine which perspective we want, update state accordingly
        if (isFirstPerson) { // going to 3rd person
            pointer.SetActive(false);
            shutter.SetActive(false);
            foreach(ShutterClose s in shutter.GetComponentsInChildren<ShutterClose>()) {
                s.Reset();
            }
        }
        else { // going to 1st person
            pointer.SetActive(true);
            shutter.SetActive(true);
        }

        isFirstPerson = !isFirstPerson;   
    }

    void SnapPhoto() {
        Ray polaroid = new Ray(transform.position, transform.forward);
        RaycastHit photo;

        if(Physics.Raycast(polaroid, out photo, 100)){
            if(photo.collider.CompareTag("GameItem")){
                foreach(ShutterClose s in shutter.GetComponentsInChildren<ShutterClose>()) {
                    s.takePhoto();
                }
                Destroy(photo.collider.gameObject);
                Debug.Log("here");
            }
            else{
                Debug.Log(photo.collider.gameObject);
            }
        }
        else {
            Debug.Log("nothing found");
        }
    }
}
