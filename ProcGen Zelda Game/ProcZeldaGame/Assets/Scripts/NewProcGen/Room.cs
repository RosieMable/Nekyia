using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class that stores all the attributes of a room and it is used in the LevelGeneration script to have a 2D array (grid) of Rooms, 
// so that is easier to manipulate the data needed

public class Room  {

    public Vector2 gridPos;  //To indicate the position of the room inside of the grid

    public int type; //To indicate what type the room is, it is going to be manipulated later on in the lvl gen script

    public bool doorTop, doorBot, doorLeft, doorRight; //bools to indicate what doors the room has

    #region Constructor of Room
    // A constructor is a special method of the class which gets automatically invoked whenever an instance of the class is created.
    public Room(Vector2 _gridPos, int _type) 
    {
        gridPos = _gridPos;
        type = _type;
    }
    #endregion
}
