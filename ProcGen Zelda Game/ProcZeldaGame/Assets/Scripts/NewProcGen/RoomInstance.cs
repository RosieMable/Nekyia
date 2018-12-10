using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour {

    //Reference to the template data that will be randomly assigned by the sheet assigner
    public Texture2D tex;

    [HideInInspector]
    public Vector2 gridPos; //Grid position that corrisponds to the one that the lvl gen has made 

    public int type; // 0: normal, 1: entrance

    public bool doorTop, doorBot, doorLeft, doorRight; //Reference to the door bools in the levelGeneration script, so that the corrisponding door can be spawned correctly

    public GameObject doorU, doorD, doorL, doorR; //Prefabs of the different types of doors (it is connected to how the player, camera and PP move)


    public GameObject doorWall; //When the doors bools are false, a wall is spawned instead, so that there are no open spaces in the room


    [SerializeField]
    private ColorToGameObject[] mappings; //Reference to the color dictionary, so that the roomInstance knows what object to assign to what colour

    private float tileSize = 16; //Reference to the tilesize of the tileset

    Vector2 roomSizeInTiles = new Vector2(9, 17); //How big te room is in tiles' length

    #region Setup - Method that will be used in the sheet assigner to define the data of each room
    public void Setup(Texture2D _tex, Vector2 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight)
    {
        tex = _tex;
        gridPos = _gridPos;
        type = _type;
        doorTop = _doorTop;
        doorBot = _doorBot;
        doorLeft = _doorLeft;
        doorRight = _doorRight;
        MakeDoors();
        GenerateRoomTiles();
    }
    #endregion

    #region Make Doors - Method that works out where the doors should be spawned
    private void MakeDoors()
    {
 
        //top door, get position then spawn
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, -5) + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
        PlaceDoor(spawnPos, doorTop, doorU);
        //bottom door
        spawnPos = new Vector3(transform.position.x, transform.position.y, -5) + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
        PlaceDoor(spawnPos, doorBot, doorD);
        //right door
        spawnPos = new Vector3(transform.position.x, transform.position.y, -5) + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
        PlaceDoor(spawnPos, doorRight, doorR);
        //left door
        spawnPos = new Vector3(transform.position.x, transform.position.y, -5) + Vector3.left * (roomSizeInTiles.x * tileSize) - Vector3.left * (tileSize);
        PlaceDoor(spawnPos, doorLeft, doorL);
    }
    #endregion

    #region Place Doors - Method that figures out what type of door it needs to be spawned
    //The method takes the position of the door, the reference if the door is there or not and a reference to which door to spawn
    void PlaceDoor(Vector3 spawnPos, bool door, GameObject doorSpawn)
    {

        if (door)
        {
            Instantiate(doorSpawn, spawnPos, Quaternion.identity).transform.parent = transform; //If door is true, then spawns the correct door...
        }
        else
        {
            Instantiate(doorWall, spawnPos, Quaternion.identity).transform.parent = transform; //...Otherwise spawns a wall
        }
        //All the objects are children of the roomroot, so that the hierarchy seems neater
    }
    #endregion

    #region Generate Tiles - Method that builds up the room by each pixel of the texture
    void GenerateRoomTiles()
    {   //loop through every pixel of the texture
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            { //On each pixel, this function is called...
                GenerateTiles(x, y);
            }
        }
    }
    #endregion

    #region Generate Tiles - Method that works out for each pixel of the texture what object should be spawned
    void GenerateTiles(int x, int y)
    { //take the pixel at x and y position
        Color pixelColor = tex.GetPixel(x, y);

        //skip clear spaces in texture (transparent pixels)
        if (pixelColor.a == 0)
        {
            return;
        }
        //find the color to math the pixel
        foreach (ColorToGameObject mapping in mappings)
        {
            //If found a match...
            if (mapping.color.Equals(pixelColor))
            {
                Vector3 spawnPos = positionFromTileGrid(x, y); //...figure out where it should be spawned
                Instantiate(mapping.prefab, spawnPos, Quaternion.identity).transform.parent = this.transform; //instantiate the object as a child of the room root
            }
        }
    }
    #endregion

    #region Position from Tile Grid - Method that works out the pixel coordinates as coordinates for the scene
    //translate the pixel coordinate to a position in our scene
    private Vector3 positionFromTileGrid(int x, int y)
    {

        Vector3 ret;
        //find difference between the corner of the texture and the center of this object 
        Vector3 offset = new Vector3((-roomSizeInTiles.x + 1) * tileSize, (roomSizeInTiles.y / 4) * tileSize - (tileSize / 4), -5);
        //find scaled up position at the offset
        ret = new Vector3(tileSize * (float)x, -tileSize * (float)y, -5) + offset + transform.position;
        return ret;
    }
    #endregion

}
