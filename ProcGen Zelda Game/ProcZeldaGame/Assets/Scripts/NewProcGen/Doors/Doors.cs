using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    protected GameObject PlayerPoint;

    protected Camera camera;

    protected GameObject Hero;

  

    [SerializeField]
    protected float Time = 20f;

    [SerializeField]
    protected Vector2 distanceBetweenDoors;

    [SerializeField]
    protected Vector2 distanceBetweenRooms;

    [SerializeField]
    protected Vector2 distanceBetweenPlayerPoints;

    protected float ValueX;

    protected float ValueY;

    void Start()
    {
        distanceBetweenRooms = new Vector2(208f, 104);

        distanceBetweenDoors = new Vector2( 100 , 55);

        distanceBetweenPlayerPoints = new Vector2(8, 4);

    }

    private void Update()
    {

        camera = Camera.main;

        Hero = FindObjectOfType<Hero>().gameObject;

        PlayerPoint = GameObject.FindGameObjectWithTag("PlayerPoint");

        DestroyIfWall();
    }

    protected void MoveUp()
    {
        //Moves the camera
        Vector3 tempPos = camera.transform.position;
        ValueY = tempPos.y += distanceBetweenRooms.y;
        Mathf.Lerp(camera.transform.position.y, ValueY, Time);
        tempPos.z = -20f;
        camera.transform.position = tempPos;

        //Moves the hero
        Vector3 tempPosPC = Hero.transform.position;
        tempPosPC.y += distanceBetweenDoors.y;
        Hero.transform.position = tempPosPC;

        //Move the PlayerPoint
        Vector3 tempPosPP = PlayerPoint.transform.position;
        tempPosPP.y += distanceBetweenPlayerPoints.y;
        PlayerPoint.transform.position = tempPosPP;
    }

    protected void MoveDown()
    {
        //Moves the camera down
        Vector3 tempPos = camera.transform.position;
        ValueY = tempPos.y -= distanceBetweenRooms.y;
        Mathf.Lerp(camera.transform.position.y,ValueY, Time);
        tempPos.z = -20f;
        camera.transform.position = tempPos;

        //Moves the hero down
        Vector3 tempPosPC = Hero.transform.position;
        tempPosPC.y -= distanceBetweenDoors.y;
        Hero.transform.position = tempPosPC;

        //Move the PlayerPoint
        Vector3 tempPosPP = PlayerPoint.transform.position;
        tempPosPP.y -= distanceBetweenPlayerPoints.y;
        PlayerPoint.transform.position = tempPosPP;
    }

    protected void MoveLeft()
    {
        //Moves the camera Left
        Vector3 tempPos = camera.transform.position;
        ValueX = tempPos.x -= distanceBetweenRooms.x;
        Mathf.Lerp(camera.transform.position.x, ValueX, Time);
        tempPos.z = -20f;
        camera.transform.position = tempPos;

        //Moves the hero left
        Vector3 tempPosPC = Hero.transform.position;
        tempPosPC.x -= distanceBetweenDoors.x;
        Hero.transform.position = tempPosPC;

        //Move the PlayerPoint
        Vector3 tempPosPP = PlayerPoint.transform.position;
        tempPosPP.x -= distanceBetweenPlayerPoints.x;
        PlayerPoint.transform.position = tempPosPP;
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


        //Move the PlayerPoint
        Vector3 tempPosPP = PlayerPoint.transform.position;
        tempPosPP.x += distanceBetweenPlayerPoints.x;
        PlayerPoint.transform.position = tempPosPP;
    }

    protected void DestroyIfWall()
    {
        GameObject[] doorWall;

        doorWall = GameObject.FindGameObjectsWithTag("DoorWall");

        foreach (GameObject wall in doorWall)
        {
            if (wall.transform.position.x == transform.position.x && wall.transform.position.y == transform.position.y)
            {
                Destroy(this.gameObject);
            }
        }


    }
}