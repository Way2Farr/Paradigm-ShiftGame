using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public ItemController[] itemButtons;
    public GameObject[] itemPrefabs;
    public GameObject[] itemGhost;
    [SerializeField] private Camera mainCamera;
    public int CurrentButtonPressed;

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (Input.GetMouseButtonDown(0) && itemButtons[CurrentButtonPressed].Clicked)
            {
                itemButtons[CurrentButtonPressed].Clicked = false;
                GameObject newItem = Instantiate(itemPrefabs[CurrentButtonPressed], raycastHit.point, Quaternion.identity);

                // Ensure the new item does not have a script that destroys it on collision
                Collider newItemCollider = newItem.GetComponent<Collider>();
                if (newItemCollider != null)
                {
                    newItemCollider.isTrigger = false;
                }

                Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
            }
        }
    }
}