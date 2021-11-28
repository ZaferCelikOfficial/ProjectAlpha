using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouroutinesController : MonoBehaviour
{
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Movement", target);
        //StartCoroutine(Movement(target));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Movement(Transform target)
    {
        Debug.LogError("1");
        yield return new WaitForSeconds(2f);
        Debug.LogError("2");
    }
}
