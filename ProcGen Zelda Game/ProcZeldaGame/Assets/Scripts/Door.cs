using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = FindObjectOfType<CameraMovement>().gameObject.GetComponent<Camera>();// Camera.main;

        if (camera = null)
        {
            camera = Camera.main;
            Debug.Log("No Camera");
        }
    }

    private void Update()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        camera.GetComponent<CameraMovement>().MoveUp();
        Debug.Log(collision.gameObject.name);
    }
}