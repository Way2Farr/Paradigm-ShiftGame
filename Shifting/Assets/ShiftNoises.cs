using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftNoises : MonoBehaviour
{

    
    private AudioSource ShiftNoise;
    public AudioClip noise1;
    public AudioClip noise2;
    public AudioClip noise3;
    public AudioClip peelingNoise;
    // Start is called before the first frame update
    void Start()
    {
        ShiftNoise = GetComponent<AudioSource>();
    }

    public void shiftInNoise()
    {
        AudioClip chosenNoise = (Random.Range(0, 3) == 0) ? noise1 : (Random.Range(0, 2) == 0 ? noise2 : noise3);
        ShiftNoise.PlayOneShot(chosenNoise);
    }

    public void shiftOutNoise(){
        ShiftNoise.PlayOneShot(peelingNoise);
    }
}
