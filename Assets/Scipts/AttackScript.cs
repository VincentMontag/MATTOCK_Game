using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public PlayerScript playerScript;

    private Vector3 rightVec = new Vector3(0.5f, -0.1f, 0.01f);
    private Vector3 leftVec = new Vector3(-0.5f, -0.1f, 0.01f);

    public Transform attackPosition;
    public LayerMask enemies;
    public float attackRange;
    public int damage;
    public bool hasWeapon = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasWeapon)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemies);
                for(int i = 0; i < enemiesToDamage.Length; i++){
                    enemiesToDamage[i].GetComponent<EnemyScript>().health -= damage;
                }
            }
        } 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if(playerScript.dir >= 0)
        {  
            attackPosition.localPosition = rightVec;
        } 
        else if(playerScript.dir < 0)
        {
            attackPosition.localPosition = leftVec;
        }
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
