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

    private AudioSource shiftNoise;
    public AudioClip noise1;
    public AudioClip noise2;
    public AudioClip noise3;
    public AudioClip peelNoise;

    void Start() {
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
        GameObject shiftNoiseMaker = new GameObject("AudioPlayer");
        shiftNoise = shiftNoiseMaker.AddComponent<AudioSource>();

    }

    // Changed to public static to reference in PlayerCamera3D
    public static bool insideWall;
    void OnCollisionStay(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        if (contact.otherCollider.transform == player3D && Input.GetButton("Shift")) {
            AudioClip chosenSound = (Random.Range(0, 3) == 0) ? noise1 : 
            (Random.Range(0, 3) == 1) ? noise2 : 
            noise3;
            shiftNoise.PlayOneShot(chosenSound);

            Vector3 position = contact.point;
            position.y = contact.otherCollider.transform.position.y;
            player2D.position = position;
            player2D.localPosition = new Vector3(player2D.localPosition.x, player2D.localPosition.y, -0.001f);
            player2D.gameObject.SetActive(true);
            player3D.gameObject.SetActive(false);
            insideWall = true;
            playerCamera.player2DTransform = player2D;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(2) && insideWall) {
            shiftNoise.PlayOneShot(peelNoise);
            player3D.position = player2D.position;
            player2D.gameObject.SetActive(false);
            player3D.gameObject.SetActive(true);
            insideWall = false;
            playerCamera.player2DTransform = null;
        }
    }
}
