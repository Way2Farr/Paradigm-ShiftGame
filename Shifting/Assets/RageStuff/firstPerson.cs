using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPerson : MonoBehaviour
{

    // Update is called once per frame

    public float mouseSensitivity = 80f;
    public Transform playerBody;
    private float xRotation = 0f;
    public Camera thirdPerson;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            thirdPerson.gameObject.SetActive(!thirdPerson.gameObject.activeSelf);
            gameObject.SetActive(!gameObject.activeSelf);
        }

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update the vertical rotation (looking up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // Prevent over-rotation

        // Apply vertical rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
