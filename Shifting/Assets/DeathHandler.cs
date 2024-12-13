using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField]
    private Transform player2D;
    [SerializeField]
    private Transform player3D;
    [SerializeField]
    private Transform respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static bool playerDead = false;
    void OnCollisionStay(Collision collision)
    {
        //Debug.Log("Collision detected");
        ContactPoint contact = collision.GetContact(0);
        if (collision.gameObject.tag == "Death")
        {
            //Debug.Log("Collision detected");
            playerDead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDead)
        {
            playerDead = false;

            this.transform.position = respawnPoint.position;
        }
    }
}
