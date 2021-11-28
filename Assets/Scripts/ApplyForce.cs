using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public static ApplyForce ApplyForceInstance;
    public float xForce,zForce= 50f;
    void Start()
    {
        
    }
    public void ForceLeft(GameObject food)
    {
        Debug.Log("ApplyingForcetoLeft");
        food.GetComponent<Rigidbody>().AddForce(new Vector3(-xForce,0));
    }
    public void ForceRight(GameObject food)
    {
        food.GetComponent<Rigidbody>().AddForce(new Vector3(xForce,0));
    }
    
}

