using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    protected Camera camera;

    protected Vector3 moveJump = Vector2.zero;
    protected float horMove, vertMove;

    [SerializeField]
    protected Vector2 distanceBetweenRooms;

    void Start()
    {
        SheetAssigner SA = FindObjectOfType<SheetAssigner>();
        distanceBetweenRooms = new Vector2(418f, 212f);
        moveJump = new Vector3(distanceBetweenRooms.x, distanceBetweenRooms.y, 0); //distance b/w rooms: to be used for movement
    }

    private void Update()
    {

        camera = Camera.main;

    }

    protected void MoveUp()
    {
        Vector3 tempPos = camera.transform.position;
        Vector3 UpMove = Vector3.up * moveJump.y;
        tempPos = UpMove;
        tempPos.z = -20f;
        camera.transform.position = tempPos;
    }

    protected void MoveDown()
    {
        Vector3 tempPos = camera.transform.position;
        Vector3 DownMove = Vector3.down * moveJump.y;
        tempPos += DownMove;
        tempPos.z = -20f;
        camera.transform.position = tempPos;
    }

    protected void MoveLeft()
    {
        Vector3 tempPos = camera.transform.position;
        Vector3 LeftMove = Vector3.right * -moveJump.x;
        tempPos += LeftMove;
        tempPos.z = -20f;
        camera.transform.position = tempPos;
    }

    protected void MoveRight()
    {
        Vector3 tempPos = camera.transform.position;
        Vector3 RightMove = Vector3.right * moveJump.x;
        tempPos += RightMove;
        tempPos.z = -20f;
        camera.transform.position = tempPos;
    }
}