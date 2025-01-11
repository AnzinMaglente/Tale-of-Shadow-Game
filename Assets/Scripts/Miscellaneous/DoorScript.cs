using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject player;
    public bool isAtDoor;
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAtDoor = true;
        }
        else
        {
            isAtDoor = false;
        }
    }
}
