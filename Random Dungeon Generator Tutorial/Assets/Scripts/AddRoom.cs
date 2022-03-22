using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//adds spawned room to rooms list in 'RoomTemplate' script
public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    // runs once for every room created
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        templates.rooms.Add(this.gameObject); //adds created room to 'RoomTemplates' list
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
