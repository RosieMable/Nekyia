using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

    /*
     * 1 == Top Direction
     * 2 == Bottom Direction
     * 3 == Right Direction
     * 4 == Left Direction
     * */
    [SerializeField]
    private int VopeningDirection;

    [SerializeField]
    private float waitTime = 4f;


    private bool MhasSpawned = false;

    private RoomsTemplates templates;

    private int VrandomNum;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = FindObjectOfType<RoomsTemplates>().GetComponent<RoomsTemplates>();
        Invoke("SpawnRoom", 0.25f);
    }


    void SpawnRoom()

    {
        if (MhasSpawned == false)
        {
            switch (VopeningDirection)
            {
                case 1:
                    //Need to spawn a room with a BOTTOM door.
                    VrandomNum = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[VrandomNum], transform.position, templates.bottomRooms[VrandomNum].transform.rotation);
                    break;

                case 2:
                    //Need to spawn a room with a TOP door.
                    VrandomNum = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[VrandomNum], transform.position, templates.topRooms[VrandomNum].transform.rotation);
                    break;

                case 3:
                    //Need to spawn a room with a LEFT door.
                    VrandomNum = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[VrandomNum], transform.position, templates.leftRooms[VrandomNum].transform.rotation);
                    break;

                case 4:
                    //Need to spawn a room with a RIGHT door.
                    VrandomNum = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[VrandomNum], transform.position, templates.rightRooms[VrandomNum].transform.rotation);
                    break;


                default:
                    Debug.Assert(VopeningDirection == 0 || VopeningDirection > 4, "The spawn point of the Room " + gameObject.GetComponentInParent<Transform>().gameObject.name + "hasn't specified an opening direction.");
                    break;
            }


        }

        MhasSpawned = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().MhasSpawned == false && MhasSpawned == false)
            {
                //Spawn walls blocking off any openings
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            //After instantiating the closed room, the bool needs to be true because otherwise it won't be there to collide with other spawn points that may appear and this will end with rooms spawning on top of each others
            MhasSpawned = true;
        }
    }
}
