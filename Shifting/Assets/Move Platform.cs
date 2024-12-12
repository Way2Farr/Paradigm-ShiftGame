using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] float offsetX = 12f;
    [SerializeField] float offsetY = 0f;
    [SerializeField] float offsetZ = 0f;

    bool positionReached = false;
    public float speed = 1000f;
    Vector3 originalPosition;
    Vector3 newPosition;
    bool callPause = true;
    bool doWait = false;
    bool flagTest = true;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        newPosition = originalPosition + offset;
        //StartCoroutine(MoveCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("start moving right");


        float step = speed * Time.deltaTime;

        
        {
            
        }
        if (!positionReached && flagTest)
        {
  
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            //doWait = true;

            
        }

        if (transform.position == newPosition && !positionReached)
        {
            positionReached = true;
            callPause = true;
            StartCoroutine(MoveCoroutine());
        }
        

        //yield return new WaitForSeconds(5);
        //Debug.Log("start moving left");
        if (positionReached && flagTest)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, step);
            doWait = true;
        }

        if (transform.position == originalPosition && positionReached)
        {
            positionReached = false;
            callPause = true;
            StartCoroutine(MoveCoroutine());
        }
        //yield return new WaitForSeconds(5);
    }

    IEnumerator MoveCoroutine ()
    {
        //Debug.Log("Wait");
        flagTest = false;
        yield return new WaitForSeconds(1);
        callPause = false;
        doWait = false;
        flagTest = true;
        //Debug.Log("");
    }

}
