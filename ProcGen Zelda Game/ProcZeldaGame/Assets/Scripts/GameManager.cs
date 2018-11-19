using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; set; }


    [SerializeField]
    private GameObject Player;

    private int Rooms;

    // Use this for initialization
    void Start () {
        Invoke("SpawnStuff", 5);
       
	}
	

    void SpawnStuff()
    {
        Instantiate(Player, transform.position, Quaternion.identity); 
    }
}
