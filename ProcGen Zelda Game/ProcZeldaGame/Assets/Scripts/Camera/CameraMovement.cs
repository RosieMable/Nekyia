using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    public float smoothing;

    [SerializeField]
    public Vector3 maxPosition;

    [SerializeField]
    public Vector3 minPosition;


    [SerializeField]
    private Vector3 cameraChange;

    [SerializeField]
    private Vector3 playerChange;
	// Use this for initialization
	void Start () {
		
	}

    void LateUpdate()
    {

        target = GameObject.FindObjectOfType<Hero>().transform;

        Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);

        Vector3 camPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (transform.position != target.position)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, smoothing);

            camPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);

            camPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);

        }
    }


    public void MoveUp()
    {
        minPosition += cameraChange;
        maxPosition += cameraChange;
        target.transform.position += playerChange;
    }
}
