using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    protected Camera camera;

    protected GameObject Hero;

    protected Vector3 moveJumpDoor = Vector2.zero;
    protected Vector3 moveJump = Vector2.zero;
    protected float horMove, vertMove;

    [SerializeField]
    protected float Time = 20f;

    [SerializeField]
    protected Vector2 distanceBetweenDoors;

    [SerializeField]
    protected Vector2 distanceBetweenRooms;

    protected float ValueX;

    protected float ValueY;

    void Start()
    {
        distanceBetweenRooms = new Vector2(208f, 104);
        distanceBetweenDoors = new Vector2( 100 , 55);


        moveJump = new Vector3(distanceBetweenRooms.x, distanceBetweenRooms.y, -20); //distance b/w rooms: to be used for movement
        moveJumpDoor = new Vector3(distanceBetweenDoors.x, distanceBetweenDoors.y, -20);
    }

    private void Update()
    {

        camera = Camera.main;

        Hero = FindObjectOfType<Hero>().gameObject;

    }

    protected void MoveUp()
    {
        Vector3 tempPos = camera.transform.position;
        ValueY = tempPos.y += distanceBetweenRooms.y;
        Mathf.Lerp(camera.transform.position.y, ValueY, Time);
        tempPos.z = -20f;
        camera.transform.position = tempPos;


        Vector3 tempPosPC = Hero.transform.position;
        tempPosPC.y += distanceBetweenDoors.y;
        Hero.transform.position = tempPosPC;
    }

    protected void MoveDown()
    {
        Vector3 tempPos = camera.transform.position;
        ValueY = tempPos.y -= distanceBetweenRooms.y;
        Mathf.Lerp(camera.transform.position.y,ValueY, Time);
        tempPos.z = -20f;
        camera.transform.position = tempPos;

        Vector3 tempPosPC = Hero.transform.position;
        tempPosPC.y -= distanceBetweenDoors.y;
        Hero.transform.position = tempPosPC;
    }

    protected void MoveLeft()
    {
        Vector3 tempPos = camera.transform.position;
        ValueX = tempPos.x -= distanceBetweenRooms.x;
        Mathf.Lerp(camera.transform.position.x, ValueX, Time);
        tempPos.z = -20f;
        camera.transform.position = tempPos;

        Vector3 tempPosPC = Hero.transform.position;
        tempPosPC.x -= distanceBetweenDoors.x;
        Hero.transform.position = tempPosPC;
    }

    protected void MoveRight()
    {
        Vector3 tempPos = camera.transform.position;
        ValueX = tempPos.x += distanceBetweenRooms.x;
        Mathf.Lerp(camera.transform.position.x, ValueX, Time);
        tempPos.z = -20f;
        camera.transform.position = tempPos;

        Vector3 tempPosPC = Hero.transform.position;
        tempPosPC.x += distanceBetweenDoors.x;
        Hero.transform.position = tempPosPC;
    }
}