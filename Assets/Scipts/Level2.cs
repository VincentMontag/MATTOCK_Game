using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{
    public PlayerScript playerScript;
    public BoxCollider2D goal;


    private void OnTriggerEnter2D(Collider2D collider)
    {

        if(collider.gameObject.tag == "player")
        {
            SceneManager.LoadScene("EndScreen");
        }
    }
}
