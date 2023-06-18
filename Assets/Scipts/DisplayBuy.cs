using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBuy : MonoBehaviour
{
    public bool isActive = false;
    
    public void DisplayText()
    {
        if(isActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
