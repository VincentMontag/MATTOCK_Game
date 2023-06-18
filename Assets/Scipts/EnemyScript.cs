using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D enemyRigidbody;
    public BoxCollider2D enemyBoxCollider;
    public CapsuleCollider2D enemyCapsuleCollider;
    public SpriteRenderer enemySpriteRenderer;

    public float enemySpeed = 2.5f;
    public int health = 2;

    public PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        Physics2D.IgnoreCollision(playerScript.GetComponent<BoxCollider2D>(), enemyCapsuleCollider);
        Physics2D.IgnoreCollision(playerScript.playerCapsuleCollider, enemyCapsuleCollider);
        Physics2D.IgnoreCollision(playerScript.playerCapsuleCollider, enemyBoxCollider);
    }

    // Update is called once per frame
    void Update()
    {
     

            if(EnemyFacesLeft())
            {
                enemyRigidbody.velocity = new Vector2(enemySpeed, 0f);
            } 
            else
            {
                enemyRigidbody.velocity = new Vector2(-enemySpeed, 0f);
            }

        if(health <= 0){
            Destroy(gameObject);
        }
    }

    private bool EnemyFacesLeft()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidbody.velocity.x)), transform.localScale.y);
    }
}
