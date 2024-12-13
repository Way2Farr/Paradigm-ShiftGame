using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class firstPerson : MonoBehaviour
{

    // Update is called once per frame

    private AudioListener audioListener;

    public float mouseSensitivity = 80f;
    public Transform playerBody;
    private float xRotation = 0f;
    public Camera thirdPerson;
    public Transform thirdPersonOrientation;
    public GameObject pointer;
    public float rayDistance = 100f;

    public shutterClose shutter1;
    public shutterClose shutter2;

    [SerializeField] private UnityEvent photoIncrement;

    void Start(){
        audioListener = GetComponent<AudioListener>();
    }

    void Update()
    {


        //Toggle to First Person
        if (Input.GetKeyDown(KeyCode.C))
        {
            audioListener.enabled = !audioListener.enabled;
            thirdPerson.gameObject.SetActive(!thirdPerson.gameObject.activeSelf);
            gameObject.SetActive(!gameObject.activeSelf);
            pointer.gameObject.SetActive(!pointer.gameObject.activeSelf);
        }

        //Polaroid Function
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf == true){
            Ray polaroid = new Ray(transform.position, transform.forward);
            RaycastHit photo;

            if(Physics.Raycast(polaroid, out photo, rayDistance)){
                if(photo.collider.CompareTag("GameItem")){
                    shutter1.takePhoto();
                    shutter2.takePhoto();
                    Destroy(photo.collider.gameObject);

                    photoIncrement.Invoke();
                
                }
            }
        }

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update the vertical rotation (looking up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -100f, 100f); // Prevent over-rotation

        // Apply vertical rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Align 3rd Person with firstPerson
        thirdPersonOrientation.rotation = transform.rotation;
    }
}
