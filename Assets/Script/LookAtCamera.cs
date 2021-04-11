using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Vector3  camDirection;
   
    // Update is called once per frame
    void Update()
    {
        camDirection = Camera.main.transform.forward;
        camDirection.y = 0;     
        transform.rotation = Quaternion.LookRotation(camDirection);
    }
}
