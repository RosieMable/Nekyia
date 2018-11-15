using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsTemplates : MonoBehaviour {


    public GameObject[] bottomRooms;


    public GameObject[] topRooms;


    public GameObject[] leftRooms;

    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    private bool MhasBossSpawned;
    public GameObject boss;

    private void Start()
    {
        Invoke("AddRoom", 3f);
        Invoke("AddBoss", 3.5f);
    }

    void AddRoom()
    {
        GameObject[] TroomsInScene;

        TroomsInScene = GameObject.FindGameObjectsWithTag("Rooms");

        foreach (GameObject Troom in TroomsInScene)
        {
            rooms.Add(Troom);
            Destroy(rooms[0]);
        }
    }

    void AddBoss()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (i == rooms.Count - 1)
            {
                Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
            }
          
        }
    }
}
