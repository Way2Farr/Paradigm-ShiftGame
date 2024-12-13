using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    [SerializeField] private Slider slider;

    void Start() { slider.value = 0.5f; }

    void Update() { AudioSettings.setVolume(slider.value); }
}
