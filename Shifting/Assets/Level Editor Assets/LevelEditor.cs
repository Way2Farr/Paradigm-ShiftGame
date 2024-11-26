using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public ItemController[] itemButtons;
    public GameObject[] itemPrefabs;

    public GameObject[] itemGhost;

    [SerializeField] private Camera mainCamera;



    public int CurrentButtonPressed;


    private void Update() {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); 
        if (Physics.Raycast(ray, out RaycastHit raycastHit)){
            transform.position = raycastHit.point;
        }

        if(Input.GetMouseButtonDown(0) && itemButtons[CurrentButtonPressed].Clicked){
            itemButtons[CurrentButtonPressed].Clicked = false;
            Instantiate(itemPrefabs[CurrentButtonPressed], new Vector3(raycastHit.point.x, raycastHit.point.y, raycastHit.point.z), Quaternion.identity);

            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
        }
    }
    
}

