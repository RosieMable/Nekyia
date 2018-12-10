using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager sGM; //Allows access to singleton
                                   //Being static means you can acess without knowing instance
    private void Awake()            //Runs before Start
    {
        if (sGM == null)            //Has it been set up before?
        {
            sGM = this;             //No, its the first Time creation of Game Manager, so store our instance
            DontDestroyOnLoad(gameObject); //Persist, now it will survive scene reloads
        }
        else if (sGM != this) //If we get called again, then destroy new version and keep old one
        {
            Destroy(gameObject); //Kill any subsequent one
        }
    }
    #endregion

    [SerializeField]
    private GameObject PlayerPrefab;
    private GameObject PlayerGO;

    private int Rooms;

    // Use this for initialization
    void Start()
    {
        Invoke("SpawnPlayer", 0);

    }

    void SpawnPlayer()
    {
        PlayerGO = Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
    }
}
