using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField]
    private GameObject PlayerPrefab;
    private GameObject PlayerGO;

    private int Rooms;

    // Use this for initialization
    void Start()
    {
        Invoke("SpawnPlayer", 0);

        if (PlayerGO == null)
        {

        }
    }

    void SpawnPlayer()
    {
        PlayerGO = Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
    }
}
