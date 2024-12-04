using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{

    //Different Item IDs
    public int ID;

    //Quantity of the item
    public int quantity;

    public TextMeshProUGUI quantityText;

    public bool Clicked = false;

    private LevelEditor editor;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        quantityText.text = quantity.ToString();
        editor = GameObject.FindGameObjectWithTag("LevelEditor").GetComponent<LevelEditor>();
        mainCamera = Camera.main;
        
    }

    //Button Clicked
    public void clickState(){
        if(quantity > 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); 
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
            Instantiate(editor.itemGhost[ID], raycastHit.point, Quaternion.identity);

            //TO DO: Find Wall parent to 2D Wall Parent

            Clicked = true;
            quantity--;
            quantityText.text = quantity.ToString();
            editor.CurrentButtonPressed = ID;
         }


        }
    }

}
