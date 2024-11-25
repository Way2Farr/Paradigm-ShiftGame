using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform wall;
    [SerializeField]
    private GameObject playerObject;
    private Transform player;
    private PlayerController3D playerController;
    private Vector3 offset;
    
    void Start() {
        player = playerObject.transform;
        playerController = playerObject.GetComponent<PlayerController3D>();
        offset = new Vector3(0, 1, -5);
    }
    void Update()
    {
        if (wall == null) {
            Quaternion test = Quaternion.Euler(playerController.lookRotation);
            transform.position = player.transform.position + (test * offset);
            transform.rotation = test;
            // Debug.DrawLine(player.transform.position, player.transform.position + (playerController.transform.rotation * offset), Color.red);
        }
        else {
            transform.position = wall.transform.position + (wall.rotation * offset);
            transform.rotation = Quaternion.LookRotation(wall.position - transform.position);
        }

    }
}
