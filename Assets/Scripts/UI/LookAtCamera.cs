using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera camera;
    public Transform objectToLook;

    // Update is called once per frame
    void Update()
    {
        // Make object always face camera
        objectToLook.rotation = Quaternion.Slerp(objectToLook.rotation, camera.transform.rotation, 3f * Time.deltaTime);
    }
}
