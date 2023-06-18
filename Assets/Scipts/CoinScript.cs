using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{   

    public int coin;
    public Text NumberOfCoins;

    public CircleCollider2D BronzeCoinCircleCollider;
    public CircleCollider2D SilverCoinCircleCollider;
    public CircleCollider2D GoldCoinCircleCollider;
    
    public PlayerScript playerScript;   

    // Start is called before the first frame update
    void Start()
    {
        BronzeCoinCircleCollider = GameObject.FindGameObjectWithTag("BronzeCoin").GetComponent<CircleCollider2D>();
        SilverCoinCircleCollider = GameObject.FindGameObjectWithTag("SilverCoin").GetComponent<CircleCollider2D>();
        GoldCoinCircleCollider = GameObject.FindGameObjectWithTag("GoldCoin").GetComponent<CircleCollider2D>();
        Physics2D.IgnoreCollision(playerScript.playerCapsuleCollider, BronzeCoinCircleCollider);
        Physics2D.IgnoreCollision(playerScript.playerCapsuleCollider, SilverCoinCircleCollider);
        Physics2D.IgnoreCollision(playerScript.playerCapsuleCollider, GoldCoinCircleCollider); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMoney(int WealthOfCoins)
    {
        for(int i = 0; i < WealthOfCoins; i++){
            coin++;
        }
        NumberOfCoins.text = coin.ToString();
        if(coin > 99){
            coin = 99;
        }
    }
}
