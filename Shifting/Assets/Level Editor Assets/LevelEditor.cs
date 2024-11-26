using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public ItemController[] itemButtons;
    public GameObject[] itemPrefabs;

    public GameObject[] itemGhost;

    public int CurrentButtonPressed;

    private void Update() {
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 worldPos = (Camera.main.ScreenToWorldPoint(screenPos));

        if(Input.GetMouseButtonDown(0) && itemButtons[CurrentButtonPressed].Clicked){
            itemButtons[CurrentButtonPressed].Clicked = false;
            Instantiate(itemPrefabs[CurrentButtonPressed], new Vector3(worldPos.x, worldPos.y, 0), Quaternion.identity);

            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
        }
    }
    
}

