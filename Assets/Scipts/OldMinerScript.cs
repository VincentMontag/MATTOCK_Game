using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMinerScript : MonoBehaviour
{

    public BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
