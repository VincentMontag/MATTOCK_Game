using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{

    private float rotateSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        // transform.Rotate(Vector3.up * Time.deltaTime, xxx);
        //to rotate smootly a bit on each frame
        while (true) 
        {
            //transform.Rotate => transform function from Rotate class
            //transform.up => along y axis
            //Time.deltaTime => how much time since last frame
            //360 * rotateSpeed * Time.deltaTime => how much i need to rotate each frame
            transform.Rotate(transform.up * rotateSpeed);
            //GameObject.FindGameObjectWithTag("SilverCoin").transform.Rotate(transform.up * rotateSpeed);
            //GameObject.FindGameObjectWithTag("GoldCoin").transform.Rotate(transform.up * rotateSpeed);
            // to avoid eternal loop
            yield return 0;
        }
    }
}
