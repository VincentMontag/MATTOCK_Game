using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public SpriteRenderer spriteRenderer;
    public AttackScript attackScript;

    private Vector3 rightVec = new Vector3(0.5f, -0.1f, 0.01f);
    private Vector3 leftVec = new Vector3(-0.5f, -0.1f, 0.01f);

    private float attackCooldown = 0.4f;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(attackScript.hasWeapon)
        {
            spriteRenderer.enabled = true;
            if(playerScript.dir >= 0)
            {
                spriteRenderer.flipX = false;
                gameObject.transform.localPosition = rightVec;
                if(Input.GetKeyDown(KeyCode.K) && playerScript.dir >= 0 && canAttack){
                    spriteRenderer.flipX = false;
                    StartCoroutine(AttackMovementRight());
                } 

            } else if(playerScript.dir < 0)
            {
                spriteRenderer.flipX = true;
                gameObject.transform.localPosition = leftVec;
                if(Input.GetKeyDown(KeyCode.K) && playerScript.dir < 0 && canAttack){
                    spriteRenderer.flipX = true;
                    StartCoroutine(AttackMovementLeft());
                }   
            }
        } 
        else
        {
            spriteRenderer.enabled = false;
        }
    }
    

    private IEnumerator AttackMovementRight()
    {
        canAttack = false;
        spriteRenderer.transform.localRotation = Quaternion.Euler(0,0,-50);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.transform.localRotation = Quaternion.Euler(0,0,-5);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator AttackMovementLeft()
    {
        canAttack = false;
        spriteRenderer.transform.localRotation = Quaternion.Euler(0,0,50);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.transform.localRotation = Quaternion.Euler(0,0,-5);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
