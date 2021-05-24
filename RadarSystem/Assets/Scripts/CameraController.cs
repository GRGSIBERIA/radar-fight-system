using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector2 prev;

    Transform ts;

    // Start is called before the first frame update
    void Start()
    {
        prev = Input.mousePosition;
        ts = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cur = Input.mousePosition;

        if (Application.isEditor)
        {
            Debug.Log("enter");
            if (Input.GetMouseButtonDown(0))
            {
                var velocity = (cur - prev) * Time.deltaTime;
                var displacement = new Vector3(velocity.x, 0f, velocity.y);
                ts.position += displacement;
            }
        }
        else
        {

        }

        prev = cur;
    }
}
