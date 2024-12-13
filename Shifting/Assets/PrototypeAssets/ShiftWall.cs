using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftWall : MonoBehaviour
{
    private PlayerCamera playerCamera;
    [SerializeField]
    private Transform player2D;
    [SerializeField]
    private Transform player3D;
    private PlayerController2D playerController2D;

    void Start() {
        playerCamera = Camera.main.GetComponent<PlayerCamera>();

        Debug.Assert(player3D != null, "Please assign the scene's 3D player to player3d");
        Debug.Assert(player2D != null, "Please assign the scene's 2D player to player2d");
        playerController2D = player2D.GetComponent<PlayerController2D>();
    }

    // Changed to public static to reference in PlayerCamera3D
    public static bool insideWall;
    void OnCollisionStay(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        if (contact.otherCollider.transform == player3D && Input.GetButton("Shift")) {
            //move the 2d player into this wall's parent
            player2D.SetParent(transform.parent.transform);
            Debug.Log(playerController2D);
            playerController2D.axis = transform.parent.transform;

            // set the position to be correct
            Vector3 position = contact.point;
            position.y = contact.otherCollider.transform.position.y;
            player2D.position = position; //move 2d player to the contact point;
            player2D.localPosition = new Vector3(player2D.localPosition.x, player2D.localPosition.y, -0.001f);
            //push it slightly in front of the wall

            player2D.rotation = transform.rotation;
            player2D.gameObject.SetActive(true);
            player3D.gameObject.SetActive(false);
            insideWall = true;
            playerCamera.player2DTransform = player2D;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(2) && insideWall) {
            player3D.position = player2D.position;
            player3D.rotation = player2D.rotation;

            player2D.gameObject.SetActive(false);
            player3D.gameObject.SetActive(true);
            insideWall = false;
            playerCamera.player2DTransform = null;
        }
    }
}
