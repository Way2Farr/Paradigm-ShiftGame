using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterClose : MonoBehaviour
{
    public float shutterSpeed = 2000f;

    public Transform shutterEnd;
    public AudioClip sound1;
    public AudioClip sound2;
    private AudioSource audioSource;
    private Vector3 originalPos;

    void Start(){
        originalPos = transform.position;
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null){
            Debug.LogError("No AudioSource component found on this object!");
        }
    }

    private void PlayRandomSound()
    {
        // Randomly choose between sound1 and sound2
        AudioClip randomSound = (Random.Range(0, 2) == 0) ? sound1 : sound2;
        audioSource.PlayOneShot(randomSound);  // Play the chosen sound
    }

    public void takePhoto()
    {
            PlayRandomSound();
            StartCoroutine(snapshot());
    }

    public IEnumerator snapshot(){
        while (Vector3.Distance(transform.position, shutterEnd.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, shutterEnd.position, shutterSpeed * Time.deltaTime);
            yield return null;
        }

        // Ensure final position is set
        transform.position = shutterEnd.position;

        // Move back to the original position
        while (Vector3.Distance(transform.position, originalPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, originalPos, shutterSpeed * Time.deltaTime);
            yield return null;
        }

        // Ensure final position is reset
        Reset();
    }

    public void Reset() {
        transform.position = originalPos;
    }
}