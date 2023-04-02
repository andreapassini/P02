using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform lookAt;
    
    void LateUpdate()
    {
        if (lookAt)
        {
            transform.LookAt(lookAt.position);
        }
    }
}
