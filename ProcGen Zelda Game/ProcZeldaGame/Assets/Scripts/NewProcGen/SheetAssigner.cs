using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetAssigner : MonoBehaviour {
    
    [SerializeField]
     private  Texture2D[] sheetsNormal; //Array of 2D textures for the "Normal rooms"

    [SerializeField]
    private Texture2D[] bossRooms; //Array of 2D textures for the "BossRooms"

    [SerializeField]
    private Texture2D[] EntranceRooms; //Array of 2D textures for the "Entrance Rooms"

    [SerializeField]
    private GameObject RoomObj; //Reference to the room root prefab (which has the script RoomInstance on)

    public Vector2 roomDimensions = new Vector2(16 * 17, 16 * 9); //States the room dimensions, it can be changed in the IDE but it is set to 272 x 144 (Number of tiles, multiplied by how many game units for tile)

    public Vector2 gutterSize = new Vector2(16*9, 16*4);  //How much space there is between the rooms

    #region Assign Method, for each room created it assign a random texture from which to read the correct objects to spawn
    public void Assign(Room[,] rooms) //Take the 2D array room from the lvl gen 
    {
        foreach (Room room in rooms)
        {
            //skip point where there is no room
            if (room == null)
            {
                continue;
            }
            else if (room.type == 1)
            {
                //pick a random index for the text array
                int index = Mathf.RoundToInt(Random.value * (sheetsNormal.Length - 1));
                //find position to place room
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>(); //Spawn the room at given position and access the RoomInstance Script
                myRoom.Setup(sheetsNormal[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight); //Calls the setup method from RoomInstance
            }
            else if (room.type == 0)
            {
                //pick a random index for the array
                int index = Mathf.RoundToInt(Random.value * (EntranceRooms.Length - 1));
                //find position to place room
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
                myRoom.Setup(EntranceRooms[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
            }
            else if (room.type == 2)
            {
                //pick a random index for the array
                int index = Mathf.RoundToInt(Random.value * (bossRooms.Length - 1));
                //find position to place room
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
                myRoom.Setup(bossRooms[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
            }
        }
    }
    #endregion
}
