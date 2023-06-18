using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{   
    public int currentHealth;
    public int maxHealth;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public PlayerScript playerScript;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerScript.numberOfHearts;
        maxHealth = 5;

        for (int i = 0; i < hearts.Length; i++)
        {
            //makes hearts red if you have that much currentHealth
            if (i < currentHealth){
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            // Creates the heartsprites for the maxhealth
            if(i < maxHealth){
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }  
    }
}
