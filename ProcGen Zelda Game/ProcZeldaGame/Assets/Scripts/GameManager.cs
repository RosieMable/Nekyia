using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //#region Singleton
    //public static GameManager sGM; //Allows access to singleton
    //                               //Being static means you can acess without knowing instance
    //private void Awake()            //Runs before Start
    //{
    //    if (sGM == null)            //Has it been set up before?
    //    {
    //        sGM = this;             //No, its the first Time creation of Game Manager, so store our instance
    //        DontDestroyOnLoad(gameObject); //Persist, now it will survive scene reloads
    //    }
    //    else if (sGM != this) //If we get called again, then destroy new version and keep old one
    //    {
    //        Destroy(gameObject); //Kill any subsequent one
    //    }
    //}
    //#endregion

    [SerializeField]
    private GameObject PlayerPrefab;
    private GameObject PlayerGO;

    [SerializeField]
    private GameObject ExitDoorPrefab;

    [SerializeField]
    private Transform BossSpawnerPos;

    [SerializeField]
    private Vector3 ExitDoorPos;

    private int Rooms;

    private bool ExitSpawned = false;

    // Use this for initialization
    void Start()
    {
        Invoke("SpawnPlayer", 0);
    }

    private void FixedUpdate()
    {
        BossSpawnerPos = FindObjectOfType<BossSpawner>().transform;

        ExitDoorPos = new Vector3(BossSpawnerPos.position.x, BossSpawnerPos.position.y, -5);
    }

    void SpawnPlayer()
    {
        PlayerGO = Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
    }

    public void SpawnExitDoor()
    {
        if (ExitSpawned == false)
        {
            Instantiate(ExitDoorPrefab, ExitDoorPos, Quaternion.identity).transform.parent = BossSpawnerPos; ;
            ExitSpawned = true; //Set the bool to true, so that there is only one exit
        }
    }

    public void DisplayGameOver()
    {
        
        SceneManager.LoadScene(4); //Loads Game over scene
    }
}
