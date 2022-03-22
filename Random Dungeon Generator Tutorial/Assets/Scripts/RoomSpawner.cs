using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 = need bottom door
    // 2 = need top door
    // 3 = need left door
    // 4 = need right door

    private RoomTemplates templates;
    private int rand;
    private bool alreadySpawnedRoom = false;

    public float roomSpawnDelay;

    //called first
    private void Start()
    {
        //get access to arrays in 'RoomTemplates' script:
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>(); //only tag with 'Rooms' is the 'RoomTemplates' game obj, the gotten component is the script attached to obj?

        //roomSpawnDelay = 2; //default value of '0.1f'
        roomSpawnDelay = 1f; //if 'roomSpawnDelay' is 1 or greater, a closed room doesn't spawn on top of the starting room

        Invoke("Spawn", roomSpawnDelay);
    }

    private void Update()
    {
        //delete all spawnpoints when boss spawns:
        if(templates.spawnedBoss == true)
        {
            Destroy(gameObject);
        }
    }

    private void Spawn()
    {
        //no rooms left:
        if( templates.maxRoomCount <= 0 && alreadySpawnedRoom == false)
        {
            //assumes first entry in each array is a 1 door room:
            switch (openingDirection)
            {
                case 1:  //spawn room with ONLY BOTTOM door
                    Instantiate(templates.bottomRooms[0], transform.position, templates.bottomRooms[0].transform.rotation);
                    break;
                case 2: //spawn room with ONLY TOP door
                    Instantiate(templates.topRooms[0], transform.position, templates.topRooms[0].transform.rotation);
                    break;
                case 3: //spawn room with ONLY LEFT door
                    Instantiate(templates.leftRooms[0], transform.position, templates.leftRooms[0].transform.rotation);
                    break;
                case 4: //spawn room with ONLY RIGHT door
                    Instantiate(templates.rightRooms[0], transform.position, templates.rightRooms[0].transform.rotation);
                    break;
                default:
                    break;
            }
        }

        //rooms left:
        if( alreadySpawnedRoom == false && templates.maxRoomCount > 0) //spawns only 1 room per spawnpnt
        {
            switch (openingDirection)
            {
                case 1: //spawn room with BOTTOM door
                    templates.maxRoomCount -= 1;
                    rand = Random.Range(0, templates.bottomRooms.Length); //subtract 1 from length since start at zero and this is used for index? (nvm, max is EXCLUSIVE)
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation); // random bottom room, spawns at curr spawnpoint's location, spawns with default rotation (could use 'Quaternion.identity' for no rotation?)
                    break;
                case 2: //spawn room with TOP door
                    templates.maxRoomCount -= 1;
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    break;
                case 3: //spawn room with LEFT door
                    templates.maxRoomCount -= 1;
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    break;
                case 4: //spawn room with RIGHT door
                    templates.maxRoomCount -= 1;
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    break;

                default: //doesn't make new room by default
                    break;
            }
            print("Room Count: " + templates.maxRoomCount);
        }

        alreadySpawnedRoom = true;

    }

    //everytime spawnpoint collides w/ anything: (happens before new room created) (both spawnpoints destroyed?)
    private void OnTriggerEnter(Collider collision)
    {
        //collision w/ other spawnpoint:
        if( collision.CompareTag("SpawnPoint") ) // && collision.GetComponent<RoomSpawner>().alreadySpawnedRoom == true) //if collision w/ spawnpoint and collided w/ spawnpoint already spawned a room
        {
            if(collision.GetComponent<RoomSpawner>().alreadySpawnedRoom == false && alreadySpawnedRoom == false) // if both collided spawnpnts haven't spawned a room
            {
                // spawns secret room to block off openings:
                Instantiate(templates.closedRoom, transform.position, templates.closedRoom.transform.rotation);

                templates.maxRoomCount -= 1;

                print("Room Count: " + templates.maxRoomCount);

                print("A secret room should spawn");

                Destroy(gameObject);

            }

            alreadySpawnedRoom = true; //marks as true whenever a collision tween spawn points so won't spawn another room

            /*
            if (openingDirection != 0) //doesn't destroy spawnpoints that don't spawn rooms so built rooms aren't built over
            {
                Destroy(gameObject); //so curr spawnpoint doesn't spawn another room
            }
            */
        }
    }

    /*
    //calls once every frame
    private void Update()
    {
        switch (openingDirection)
        {
            case 1: //spawn room with BOTTOM door
                rand = Random.Range(0, templates.bottomRooms.Length); //subtract 1 from length since start at zero and this is used for index? (nvm, max is EXCLUSIVE)
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation); // random bottom room, spawns at curr spawnpoint's location, spawns with default rotation (could use 'Quaternion.identity' for no rotation?)
                break;
            case 2: //spawn room with TOP door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                break;
            case 3: //spawn room with LEFT door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                break;
            case 4: //spawn room with RIGHT door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                break;

            default:
                break;
        }

    }
    */
}
