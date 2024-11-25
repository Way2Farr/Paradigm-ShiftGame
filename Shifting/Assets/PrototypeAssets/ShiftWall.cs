using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftWall : MonoBehaviour
{
    [SerializeField]
    private FollowPlayer cameraScript;
    [SerializeField]
    private Transform player2D;
    [SerializeField]
    private Transform player3D;
    private bool insideWall;
    void OnCollisionStay(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        if (contact.otherCollider.transform == player3D && Input.GetButton("Shift")) {
            Vector3 position = contact.point;
            position.y = contact.otherCollider.transform.position.y;
            player2D.position = position;
            player2D.localPosition = new Vector3(player2D.localPosition.x, player2D.localPosition.y, -0.001f);
            player2D.gameObject.SetActive(true);
            player3D.gameObject.SetActive(false);
            insideWall = true;
            cameraScript.wall = transform;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && insideWall) {
            player3D.position = player2D.position;
            player2D.gameObject.SetActive(false);
            player3D.gameObject.SetActive(true);
            insideWall = false;
            cameraScript.wall = null;
        }
    }
}
