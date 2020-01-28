using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject observation;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        cam.transform.position = Vector3.Slerp(cam.transform.position, 
            new Vector3( observation.transform.position.x - observation.transform.forward.x * 15,
            cam.transform.position.y, observation.transform.position.z - observation.transform.forward.z * 15), 1f * Time.deltaTime );
        cam.transform.LookAt(observation.transform);
    }

}
