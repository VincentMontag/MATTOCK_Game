using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutoriallevel : MonoBehaviour
{
    Vector2 world1Level1 = new Vector2(-34f, -129f);
    Vector2 newSpawn = new Vector2(-34f, -129f);

    public PlayerScript playerScript;
    public BoxCollider2D goal;


    private void OnTriggerEnter2D(Collider2D collider)
    {

        if(collider.gameObject.tag == "player")
        {
            playerScript.myRigidbody.transform.position = world1Level1;
            playerScript.numberOfHearts = 5;
            playerScript.spawn = newSpawn;
        }
    }
}
