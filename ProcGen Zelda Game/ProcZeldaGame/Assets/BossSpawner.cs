using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] bossPool;

    [SerializeField]
    private GameObject randomBoss;

    public bool Dead = false;



    private Vector3 SpawnPos;

    private void Start()
    {
        SpawnPos = new Vector3(transform.position.x, transform.position.y, -5); //Correct spawning position, because of the Z value

        randomBoss = bossPool[Random.Range(0, bossPool.Length)]; //Chooses a random boss from the array

        Instantiate(randomBoss, SpawnPos, Quaternion.identity).transform.parent = transform; //Spawn the boss as a child of the spawner

        randomBoss.SetActive(true);
    }

}
