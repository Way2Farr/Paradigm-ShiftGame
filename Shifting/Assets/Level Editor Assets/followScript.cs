using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followScript : MonoBehaviour
{

    void Update()
    {
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

        Vector3 worldPos = (Camera.main.ScreenToWorldPoint(screenPos));

        transform.position = worldPos;
        
    }
}
