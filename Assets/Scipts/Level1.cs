using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    Vector2 world1Level2 = new Vector2(-30f, -205f);
    Vector2 newSpawn = new Vector2(-30f, -205f);

    public PlayerScript playerScript;
    public BoxCollider2D goal;


    private void OnTriggerEnter2D(Collider2D collider)
    {

        if(collider.gameObject.tag == "player")
        {
            playerScript.myRigidbody.transform.position = world1Level2;
            playerScript.numberOfHearts = 5;
            playerScript.spawn = newSpawn;
        }
    }
}
