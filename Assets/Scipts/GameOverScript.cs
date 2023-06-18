using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{   
    public Image GameOverBackground;
    public Text GameOverText;
    public Button GameOverButton;

    public PlayerScript playerScript;

    public void GameOver(){
        gameObject.SetActive(true);
    }

    public void Restart(){
        gameObject.SetActive(false);
        playerScript.numberOfHearts = 5;
        playerScript.myRigidbody.transform.position = playerScript.spawn;
        playerScript.playerSpriteRenderer.enabled = true;
        playerScript.playerBoxCollider2D.enabled = true;
    }
}

