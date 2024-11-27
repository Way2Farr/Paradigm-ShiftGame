using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class boxItem : MonoBehaviour
{

    public int ID;
    private LevelEditor editor;
    // Start is called before the first frame update
    void Start()
    {
       editor = GameObject.FindGameObjectsWithTag("LevelEditor")[0].GetComponent<LevelEditor>();
        
    }

    private void OnMouseOver() {

        if(Input.GetMouseButtonDown(1)){
            Destroy(this.gameObject);
            editor.itemButtons[ID].quantity++;
            editor.itemButtons[ID].quantityText.text = editor.itemButtons[ID].quantity.ToString();
        }
    }


}
