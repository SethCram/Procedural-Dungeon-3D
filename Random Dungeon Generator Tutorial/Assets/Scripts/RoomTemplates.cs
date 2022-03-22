using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    //fill these with appropriate room prefabs:
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms; //dynamic list of gameobjects

    public int maxRoomCount = 20;

    public float waitTime; //time waited to spawn boss
    public bool spawnedBoss = false;
    public GameObject boss;

    private RoomSpawner roomSpawner; // ?

    private void Start()
    {
        roomSpawner = GetComponent<RoomSpawner>();

        //waitTime = maxRoomCount * 5; //default value of 2
        waitTime = maxRoomCount;

        print("waitTime: " + waitTime);
    }

    private void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false)
        {
            Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity); //adds boss in last spawned room w/ waitTime is up
            spawnedBoss = true;

            print("Boss spawned");
        }
        else if (waitTime > -1)
        {
            waitTime -= Time.deltaTime; //decrease waitTime per frame
        }
    }
}
