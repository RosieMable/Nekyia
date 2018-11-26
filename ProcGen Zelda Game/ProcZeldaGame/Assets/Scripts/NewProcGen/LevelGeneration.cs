using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    //Define the game world size
    [SerializeField]
    private Vector2 worldSize = new Vector2(4, 4);

    //Define 2D array to store rooms
    Room[,] rooms;

    //Define a list of Vector2 for the taken positions. Using a list, because it is easier to manipulate later on
    private List<Vector2> takenPositions = new List<Vector2>();


    private int gridSizeX, gridSizeY;

    
    public int numberOfRooms = 20;

    public GameObject roomWhiteObj;

    public Transform mapRoot;

    private void Start()
    {
        //Function to be certain that there aren't more rooms then what the grid can fit in
        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }

        //To be sure that te gridSizeX and Y value are exactly the same as the world size x and y
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);

        CreateRooms(); //lays out the actual map
        SetRoomDoors(); //assigns the doors where rooms would connect
        DrawMap(); //instantiates objects to make up a map
        GetComponent<SheetAssigner>().Assign(rooms); //passes room info to another script which handles generatating the level geometry

    }

    void CreateRooms()
    {
        //Setup of rooms
        rooms = new Room[gridSizeX * 2, gridSizeY * 2]; //Creates as many rooms as the world grid
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1); // Sets up where the roomsgrid will start (at the center of the scene) and the type of the room (it's 1 because is the starting room)
        takenPositions.Insert(0, Vector2.zero); //Insert the first value (a vector2 zero) as the first element of the takenposition array
        Vector2 checkPos = Vector2.zero; //Defines a local variable that will be later on manipulated


        // Magic numbers ???
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        //Add rooms
        //Loop that will run once for each room that we are going to make
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            //A bit of math to define the percentage of the odds of branching out rooms or not
            //The further we get into the loop, the less likely the rooms will branch out
            float randomPerc = ((float)i / ((float)numberOfRooms - 1)); //How many rooms are left to create?
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

            //Grab new position
            checkPos = NewPosition();

            //Test new position 
            //To make the map shape more interesting
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;

                do
                {   //For the times the rooms want to branch out we keep trying to find a new position...
                    checkPos = SelectiveNewPosition();
                    iterations++;

                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100); // ...That has only one neighbor that connects to it
                if (iterations >= 50)
                {
                    print("ERROR: could not create with fewer neighbors than: " + NumberOfNeighbors(checkPos, takenPositions));
                }
            }

            //Finalize position
            //Add the rooms to the array of taken positions
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0);
            takenPositions.Insert(0, checkPos);
        }

    }

    //By grabbing the position of an already existing room, we are defining where the next one should be
    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            //Grabbing a random position from the takenPositions list
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;

            //Randomly selecting whether the next room goes up or down (UpDown) or left or right (positive)
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }

            //The loop will keep going through the loop until the position is not already taken and making sure that it is within our rooms grid boundaries
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);

        return checkingPos;
    }



    Vector2 SelectiveNewPosition()
    { // method differs from the above in the two commented ways
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                //instead of getting a room to find an adject empty space, we start with one that only 
                //has one neighbor. This will make it more likely that it returns a room that branches out
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if (inc >= 100)
        { // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
            print("Error: could not find position with only one neighbor");
        }
        return checkingPos;
    }


    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        //We take an int set to zero and increment it each time we are checking the positions
        int ret = 0;

        if (usedPositions.Contains(checkingPos + Vector2.right))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }

        return ret;
    }


    void SetRoomDoors()
    {
        //The following two for loops allow us to check every position inside of the array...
        for (int x = 0; x < ((gridSizeX * 2)); x++)
        {
            for (int y = 0; y < ((gridSizeY * 2)); y++)
            {
                if (rooms[x, y] == null)
                {
                    continue; //If there is no room, the loop will continue, otherwise we will check each cardinal point inside of the grid to see if there are rooms
                }
                Vector2 gridPosition = new Vector2(x, y);
                if (y - 1 < 0)
                { //check above
                    rooms[x, y].doorBot = false;
                }
                else
                {
                    rooms[x, y].doorBot = (rooms[x, y - 1] != null);
                }
                if (y + 1 >= gridSizeY * 2)
                { //check bellow
                    rooms[x, y].doorTop = false;
                }
                else
                {
                    rooms[x, y].doorTop = (rooms[x, y + 1] != null);
                }
                if (x - 1 < 0)
                { //check left
                    rooms[x, y].doorLeft = false;
                }
                else
                {
                    rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
                }
                if (x + 1 >= gridSizeX * 2)
                { //check right
                    rooms[x, y].doorRight = false;
                }
                else
                {
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null);
                }
            }
        }
    }

    void DrawMap()
    {
        //Loop through each room inside of the rooms array
        foreach (Room room in rooms)
        {
            if (room == null)
            {
                continue; //skip where there is no room
            }
            //Grab each rooms position and multiply it by the size of the sprite
            Vector2 drawPos = room.gridPos;
            drawPos.x *= 16;//aspect ratio of map sprite
            drawPos.y *= 8;

            //create map obj and assign its variables to the room that it represents
            MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();
            mapper.type = room.type;
            mapper.up = room.doorTop;
            mapper.down = room.doorBot;
            mapper.right = room.doorRight;
            mapper.left = room.doorLeft;
            mapper.gameObject.transform.parent = mapRoot;
        }
    }


}
    
