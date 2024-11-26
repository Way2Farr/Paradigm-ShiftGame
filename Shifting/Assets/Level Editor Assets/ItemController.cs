using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{

    public int ID;

    public int quantity;

    public TextMeshProUGUI quantityText;

    public bool Clicked = false;

    private LevelEditor editor;

    // Start is called before the first frame update
    void Start()
    {
        quantityText.text = quantity.ToString();
        editor = GameObject.FindGameObjectsWithTag("LevelEditor")[0].GetComponent<LevelEditor>();
        
    }

    public void clickState(){
        if(quantity > 0){

            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            Vector3 worldPos = (Camera.main.ScreenToWorldPoint(screenPos));
            Instantiate(editor.itemGhost[ID], new Vector3(worldPos.x, worldPos.y, 0), Quaternion.identity);

            Clicked = true;
            quantity--;
            quantityText.text = quantity.ToString();
            editor.CurrentButtonPressed = ID;

        }
    }

}
