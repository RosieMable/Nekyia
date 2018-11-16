using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour {

    public Texture2D map_tex;

    [HideInInspector]
    public Vector2 gridPos;

    public int type; // 0: normal, 1: entrance

    public bool doorTop, doorBot, doorLeft, doorRight;


    [SerializeField]
    private GameObject doorU, doorD, doorL, doorR, doorWall;


    [SerializeField]
    private ColorToGameObject[] mappings;

    float tileSize = 16;

    public Vector2 roomSizeInTiles = new Vector2(9, 17);

    public void Setup(Texture2D _tex, Vector2 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight)
    {
        map_tex = _tex;
        gridPos = _gridPos;
        type = _type;
        doorTop = _doorTop;
        doorBot = _doorBot;
        doorLeft = _doorLeft;
        doorRight = _doorRight;
        MakeDoors();
        GenerateRoomTiles();
    }


    void GenerateRoomTiles()
    {
        for (int x = 0; x < map_tex.width; x++)
        {
            for (int y = 0; y < map_tex.height; y++)
            {
                GenerateTiles(x, y);
            }
        }
    }

    void GenerateTiles(int x, int y)
    {
        Color pixelColor = map_tex.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            //The pixel is transparent, skip it
            return;
        }

        foreach (ColorToGameObject mapping in mappings)
        {
            if (mapping.color.Equals(pixelColor))
            {
                Vector3 spawnPos = positionFromTileGrid(x, y);
                Instantiate(mapping.prefab, spawnPos, Quaternion.identity).transform.parent = this.transform;
            }
            else
            {
                //print(mapping.color + ", " + pixelColor);
            }
        }
    }

    //translate the pixel coordinate to a position in our scene
    private Vector3 positionFromTileGrid(int x, int y)
    {

        //Since the position 0, 0 for our texture is in its corner, while the position 0, 0 for our RoomRoot is its center, we need to calculate the offset
        Vector3 ret;
        Vector3 offset = new Vector3((-roomSizeInTiles.x + 1) * tileSize,
                                        (roomSizeInTiles.y / 4) * tileSize - (tileSize / 4), 0);
        ret = new Vector3(tileSize * (float)x, -tileSize * (float)y, 0) + offset + transform.position;
        return ret;
    }


    private void MakeDoors()
    {
        Vector3 spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
        
        spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);

        spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x / 4 * tileSize) - Vector3.right * (tileSize / 4);

        spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x / 4 * tileSize) - Vector3.left * (tileSize / 4);
    }

    void PlaceDoor(Vector3 spawnPos, bool door, GameObject doorSpawn)
    {
        if (door)
        {
            Instantiate(doorSpawn, spawnPos, Quaternion.identity).transform.parent = transform;
        }
        else
        {
           Instantiate(doorWall, spawnPos, Quaternion.identity).transform.parent = transform;
        }
    }
}
