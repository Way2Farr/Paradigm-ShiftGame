using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{

public void OpenGate() {
        // Open the gate
     Destroy(this.gameObject);
    }

}

