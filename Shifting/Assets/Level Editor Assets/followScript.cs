using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Camera mainCamera;
    public Camera firstPerson;
    public GameObject pointer;

    void Start()
    {
        gameObject.SetActive(true);
        firstPerson.gameObject.SetActive(false);
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera not found!");
            }
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                transform.position = raycastHit.point;
            }
        }
    }
}