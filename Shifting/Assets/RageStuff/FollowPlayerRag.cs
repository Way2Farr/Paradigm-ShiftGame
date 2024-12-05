using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform wall;
    public Camera firstPerson;
    public Transform firstPersonOrientation;
    [SerializeField]
    private GameObject playerObject;
    private Transform player;
    private PlayerController3D playerController;
    private Vector3 offset;
    
    void Start() {
        //Sets beginning positions
        player = playerObject.transform;
        playerController = playerObject.GetComponent<PlayerController3D>();
        offset = new Vector3(0, 1, -5);
        gameObject.SetActive(true);
        firstPerson.gameObject.SetActive(false);
    }

    void Update()
    {

        //Toggle Cameras to first person
        if (Input.GetKeyDown(KeyCode.C))
        {
            gameObject.SetActive(!gameObject.activeSelf);
            firstPerson.gameObject.SetActive(!firstPerson.gameObject.activeSelf);
        }

        //Mimic script when not in wall
        if (wall == null) {
            Quaternion test = Quaternion.Euler(playerController.lookRotation);
            transform.position = player.transform.position + (test * offset);
            transform.rotation = test;
            firstPersonOrientation.rotation = transform.rotation;
            // Debug.DrawLine(player.transform.position, player.transform.position + (playerController.transform.rotation * offset), Color.red);
        }
        //Mimic script so that objects stayed mirrored in position to each other
        else {
            transform.position = wall.transform.position + (wall.rotation * offset);
            transform.rotation = Quaternion.LookRotation(wall.position - transform.position);
        }

    }
}
