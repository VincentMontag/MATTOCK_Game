using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{   
    // Player Variables
    public Rigidbody2D myRigidbody;
    public SpriteRenderer playerSpriteRenderer;
    public CapsuleCollider2D playerCapsuleCollider;
    public BoxCollider2D playerBoxCollider2D;
    public float speed = 5;
    public float dir;
    public float jumpheight = 7;
    public Vector2 spawn = new Vector2(-25, 7.5f);
    public int numberOfHearts = 5;
    public GameObject deathBox;
    [SerializeField] private LayerMask ground;

    //Animator Variables
    private Animator playerAnimator;

    private bool deathboxTouched = false;

    // Variables for Dashing
    private TrailRenderer playerTrailRenderer;
    [SerializeField] private float powerOfDash = 4.5f;
    [SerializeField] private float dashDuration = 0.2f;
    private Vector2 dashDirection;
    public bool isDashing;
    public bool canDash = true;
    Vector2 topLeft = new Vector2(1,1);
    Vector2 topRight = new Vector2(-1,1);

    // Variables to trigger GameOver
    public GameOverScript GameOverScript;

    // Variables for Coins
    public CoinScript coinScript;

    //Buying
    public OldMinerScript oldMinerScript;
    public DisplayBuy displayBuy;
    public AttackScript attackScript;
    public bool canBuy = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerBoxCollider2D = GetComponent<BoxCollider2D>();
        playerTrailRenderer = GetComponent<TrailRenderer>();
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        Physics2D.IgnoreLayerCollision(6, 7, false);
        coinScript = GameObject.FindGameObjectWithTag("NumberOfCoins").GetComponent<CoinScript>();
        Physics2D.IgnoreCollision(playerCapsuleCollider, oldMinerScript.boxCollider2D);
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        /* 
        numberOfHearts > 0 = Player is dead, so no
        input allowed
        */ 
        if(numberOfHearts > 0){

            dir = Input.GetAxis("Horizontal");
            if(dir >= 0)
            {
                 playerSpriteRenderer.flipX = false;
            } else{
                playerSpriteRenderer.flipX = true;
            }

            transform.Translate(Vector2.right * dir * speed * Time.deltaTime);

            //Animator should do running animations if direction (we are moving) is not 0
            if(dir != 0)
            {
                playerAnimator.SetBool("IsRunning", true);
            }
            else
            {
                playerAnimator.SetBool("IsRunning", false);
            }
            //Animator should do jumping animations if GroundCheck returns false (means we are in the air)
            if(GroundCheck() == false)
            {
                playerAnimator.SetBool("IsJumping", true);
            }
            else
            {
                playerAnimator.SetBool("IsJumping", false);
            }

            // Jumping
            if(Input.GetKeyDown(KeyCode.Space) && GroundCheck())
            {
                myRigidbody.AddForce(Vector2.up * jumpheight, ForceMode2D.Impulse);
            }

            //Buying
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(coinScript.coin >= 25 && canBuy)
                {
                    attackScript.hasWeapon = true;
                    coinScript.coin -= 25;
                }
            }

            //Dashing
            if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                Physics2D.IgnoreLayerCollision(6, 7, true);
                isDashing = true;
                canDash = false;
                playerTrailRenderer.emitting = true;
                dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                if(dashDirection == Vector2.zero)
                {
                    dashDirection = new Vector2(transform.localScale.x, 0);
                }

                // Coroutine is like Thread
                StartCoroutine(StopDashing());
            }

            if(isDashing)
            {
                myRigidbody.velocity = dashDirection.normalized * powerOfDash;
                return;
            } 
        } else if(numberOfHearts == 0)
        {
            GameOverScript.GameOver();
            playerBoxCollider2D.enabled = false;
            playerSpriteRenderer.enabled = false;
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashDuration);
        playerTrailRenderer.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        yield return new WaitForSeconds(1);
        canDash = true;
    }

    /*
    * checks if the player is on the ground to allow him to jump up and be able to dash
    * does not allow jumping after dashing since you are no longer on the ground anymore
    * can be use as a true/false check
    */
    private bool GroundCheck()
    {
        return Physics2D.BoxCast(playerCapsuleCollider.bounds.center, playerCapsuleCollider.bounds.size, 0f, Vector2.down, 0.1f, ground);
    }

    private IEnumerator DeathBoxTouched()
    {
        deathboxTouched = true;
        myRigidbody.transform.position = spawn + Vector2.up;
        yield return new WaitForSeconds(0.1f);
        numberOfHearts -= 1;
        deathboxTouched = false;
    }

    /*
    * If player touches border of level (called deathbox)
    * Player will respawn and loose one heart
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "deathbox" && deathboxTouched == false)
        {    
            StartCoroutine(DeathBoxTouched());    
        }

        // Bronze coin is worth 1
        if(collision.gameObject.tag == "BronzeCoin" && coinScript.coin < 99)
        {   
            coinScript.AddMoney(1);
            Destroy(collision.gameObject);
        }

        // Silver coin is worth 3
        if(collision.gameObject.tag == "SilverCoin" && coinScript.coin < 99)
        {   
            coinScript.AddMoney(3);
            Destroy(collision.gameObject);
        }

        // Gold coin is worth 5
        if(collision.gameObject.tag == "GoldCoin" && coinScript.coin < 99)
        {   
            coinScript.AddMoney(5);
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "oldMiner" && coinScript.coin >= 25)
        {
            displayBuy.isActive = true;
            displayBuy.DisplayText();
            if(coinScript.coin >= 25)
            {
                canBuy = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "oldMiner")
        {
            displayBuy.isActive = false;
            displayBuy.DisplayText();
            canBuy = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        If you walk into enemy, you'll get tp'd to spawn
        If you dash, you'll go through
        */
        if(collision.gameObject.tag == "enemy")
        {
            if(isDashing == false)
            {
                if(numberOfHearts >= 2)
                {
                myRigidbody.transform.position = spawn;
                numberOfHearts -= 1;
                } 
                else if(numberOfHearts == 1)
                {
                    numberOfHearts -= 1;
                }
            }
        }
    }
}