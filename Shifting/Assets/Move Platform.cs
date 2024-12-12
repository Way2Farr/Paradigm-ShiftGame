using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    public float speed = 1f;
    Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveCoroutine());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(5);
        

        float step = speed * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

}
}
