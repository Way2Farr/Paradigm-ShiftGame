using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour {
    private static float globalVolume = 0.5f;
    public static AudioSettings instance;

    private void Awake() {
        // ensure only one instance exists
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else { Destroy(gameObject); }
    }

    // getters and setters
    public static float getVolume() { return globalVolume; }
    public static void setVolume(float newVolume) { globalVolume = Mathf.Clamp(newVolume, 0f, 1f); }
}
