using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpNoises : MonoBehaviour
{

    public AudioClip jump;
    public AudioClip land;
    public AudioSource jumpNoiseMaker;
    public PlayerMovement3D player;
    // Start is called before the first frame update
    void Start()
    {
        jumpNoiseMaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")){
            jumpNoiseMaker.PlayOneShot(jump);
            while(!(player.IsGrounded())){

            }
            jumpNoiseMaker.PlayOneShot(land);
        }
    }
}
